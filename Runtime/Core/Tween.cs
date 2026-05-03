using System;
using UnityEngine;

namespace Warlogic.Tweenkit
{
    public class Tween : ITween
    {
        protected float _elapsed;
        protected float _delayElapsed;
        protected bool _isComplete;
        protected bool _isKilled;
        protected bool _isPlaying;
        protected bool _isPaused;
        protected Func<bool> _aliveCondition;
        protected UnityEngine.Object _target;

        protected Func<float, float> _ease = t => t;

        protected float _duration;
        protected float _delay;
        protected int _currentLoop;
        protected bool _isReversed;

        public virtual float Duration
        {
            get { return _duration; }
            set
            {
                ThrowIfSequenced();
                _duration = value;
            }
        }

        public virtual float Delay
        {
            get { return _delay; }
            set
            {
                ThrowIfSequenced();
                _delay = value;
            }
        }

        public bool IsSequenced { get; internal set; }

        public virtual bool IsComplete
        {
            get { return _isComplete; }
        }

        public virtual bool IsKilled
        {
            get { return _isKilled; }
        }

        public virtual bool IsPlaying
        {
            get { return _isPlaying; }
        }

        public virtual bool IsPaused
        {
            get { return _isPaused; }
        }

        public virtual int LoopCount
        {
            get { return _loopCount; }
            protected set { _loopCount = value; }
        }

        private int _loopCount = 1;

        public virtual LoopType LoopType
        {
            get { return _loopType; }
            protected set { _loopType = value; }
        }

        private LoopType _loopType = LoopType.Restart;

        public event Action OnComplete;
        public event Action OnUpdate;
        public event Action OnKill;

        protected void RaiseOnUpdate()
        {
            OnUpdate?.Invoke();
        }

        protected void RaiseOnComplete()
        {
            OnComplete?.Invoke();
        }

        protected void RaiseOnKill()
        {
            OnKill?.Invoke();
        }

        public Tween SetDuration(float duration)
        {
            ThrowIfSequenced();
            Duration = Mathf.Max(0f, duration);
            return this;
        }

        public Tween SetDelay(float delay)
        {
            ThrowIfSequenced();
            Delay = delay;
            return this;
        }

        public Tween SetEase(Ease ease)
        {
            ThrowIfSequenced();
            _ease = EasingFunctions.GetFunction(ease);
            return this;
        }

        public Tween SetEase(Func<float, float> ease)
        {
            ThrowIfSequenced();
            _ease = ease;
            return this;
        }

        public Tween SetEase(Ease inEase, Ease outEase)
        {
            ThrowIfSequenced();
            Func<float, float> inFunc = EasingFunctions.GetFunction(inEase);
            Func<float, float> outFunc = EasingFunctions.GetFunction(outEase);
            _ease = t =>
            {
                if (t < 0.5f)
                {
                    return inFunc(t * 2f) * 0.5f;
                }

                return outFunc((t - 0.5f) * 2f) * 0.5f + 0.5f;
            };
            return this;
        }

        public Tween SetEase(Func<float, float> inEase, Func<float, float> outEase)
        {
            ThrowIfSequenced();
            _ease = t =>
            {
                if (t < 0.5f)
                {
                    return inEase(t * 2f) * 0.5f;
                }

                return outEase((t - 0.5f) * 2f) * 0.5f + 0.5f;
            };
            return this;
        }

        public Tween SetAliveCondition(Func<bool> condition)
        {
            ThrowIfSequenced();
            _aliveCondition = condition;
            return this;
        }

        public Tween SetAliveCondition(UnityEngine.Object target)
        {
            ThrowIfSequenced();
            _target = target;
            _aliveCondition = () => _target != null;
            return this;
        }

        public virtual IPlayable SetLoops(int count, LoopType loopType)
        {
            ThrowIfSequenced();
            LoopCount = count;
            LoopType = loopType;
            return this;
        }

        public virtual void Play()
        {
            if (_isKilled || _isComplete)
            {
                throw new InvalidOperationException("Cannot play a tween that has been killed or completed.");
            }

            if (_isPaused)
            {
                _isPaused = false;
                return;
            }

            if (!_isPlaying)
            {
                Reset();
                _isPlaying = true;
            }
        }

        public virtual void Pause()
        {
            if (!_isPlaying || _isPaused)
            {
                return;
            }

            _isPaused = true;
        }

        public virtual void Kill()
        {
            if (_isKilled)
            {
                return;
            }

            _isKilled = true;
            _isPlaying = false;
            _isPaused = false;
            OnKill?.Invoke();
        }

        public virtual void Complete()
        {
            if (_isComplete)
            {
                return;
            }

            _elapsed = Duration;
            _isComplete = true;
            _isKilled = true;
            _isPlaying = false;
            _isPaused = false;
            ApplyValue(ComputeFinalT());
            OnUpdate?.Invoke();
            OnComplete?.Invoke();
        }

        public virtual void Tick(float deltaTime)
        {
            if (!_isPlaying || _isKilled || _isComplete)
            {
                return;
            }

            if (_aliveCondition != null && !_aliveCondition.Invoke())
            {
                Kill();
                return;
            }

            if (Delay > 0f && _delayElapsed < Delay)
            {
                _delayElapsed += deltaTime;
                if (_delayElapsed < Delay)
                {
                    return;
                }

                deltaTime = _delayElapsed - Delay;
                _delayElapsed = Delay;
            }

            _elapsed += deltaTime;

            if (Duration <= 0f)
            {
                _elapsed = 0f;
                ApplyValue(ComputeFinalT());
                OnUpdate?.Invoke();
                _isComplete = true;
                _isPlaying = false;
                OnComplete?.Invoke();
                return;
            }

            float rawT = Mathf.Clamp01(_elapsed / Duration);
            float easedT = _ease(rawT);
            if (_isReversed)
            {
                easedT = 1f - easedT;
            }

            ApplyValue(easedT);
            OnUpdate?.Invoke();

            if (_elapsed >= Duration)
            {
                _currentLoop++;
                int totalLoops = LoopCount < 0 ? int.MaxValue : LoopCount;
                if (_currentLoop >= totalLoops)
                {
                    _isComplete = true;
                    _isPlaying = false;
                    OnComplete?.Invoke();
                    return;
                }

                _elapsed = 0f;
                _delayElapsed = Delay;

                if (LoopType == LoopType.Yoyo)
                {
                    _isReversed = !_isReversed;
                }
            }
        }

        public virtual void Reset()
        {
            _elapsed = 0f;
            _delayElapsed = 0f;
            _isComplete = false;
            _isKilled = false;
            _isPlaying = false;
            _isPaused = false;
            _currentLoop = 0;
            _isReversed = false;
        }

        protected virtual void ApplyValue(float t)
        {
        }

        private float ComputeFinalT()
        {
            if (LoopType == LoopType.Restart || LoopCount < 0)
            {
                return 1f;
            }

            // Yoyo with finite loops.
            // After LoopCount-1 completions, _isReversed is toggled that many times.
            // Final iteration direction: reversed if (LoopCount - 1) is odd, i.e. LoopCount is even.
            bool finalReversed = (LoopCount - 1) % 2 == 1;
            float easedT = _ease(1f);
            return finalReversed ? 1f - easedT : easedT;
        }

        private void ThrowIfSequenced()
        {
            if (IsSequenced)
            {
                throw new InvalidOperationException("Cannot modify a tween that is part of a sequence.");
            }
        }
    }
}
