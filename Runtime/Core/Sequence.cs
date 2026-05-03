using System;
using System.Collections.Generic;
using UnityEngine;

namespace Warlogic.Tweenkit
{
    public class Sequence : IPlayable
    {
        private readonly List<SequenceItem> _items = new List<SequenceItem>();
        private float _insertionPoint;
        private float _lastStartTime;

        private float _elapsed;
        private float _duration;
        private bool _isComplete;
        private bool _isPlaying;
        private bool _isPaused;
        private bool _isKilled;
        private int _currentLoop;

        public float Duration
        {
            get { return _duration; }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
        }

        public bool IsKilled
        {
            get { return _isKilled; }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
        }

        public bool IsPaused
        {
            get { return _isPaused; }
        }

        public int LoopCount { get; private set; } = 1;

        public LoopType LoopType { get; private set; } = LoopType.Restart;

        public event Action OnComplete;
        public event Action OnUpdate;
        public event Action OnKill;

        public Sequence Append(IPlayable playable)
        {
            ThrowIfInvalidPlayable(playable);

            if (playable is ITween tween && tween.IsSequenced)
            {
                throw new InvalidOperationException("Tween is already part of a sequence.");
            }

            if (playable is Tween t)
            {
                t.IsSequenced = true;
            }

            SequenceItem item = new SequenceItem(_insertionPoint, playable.Duration, playable, null);
            _items.Add(item);
            _lastStartTime = item.StartTime;
            _insertionPoint += playable.Duration;
            _duration = Mathf.Max(_duration, _insertionPoint);
            return this;
        }

        public Sequence AppendInterval(float interval)
        {
            _insertionPoint += interval;
            _duration = Mathf.Max(_duration, _insertionPoint);
            return this;
        }

        public Sequence AppendCallback(Action callback)
        {
            SequenceItem item = new SequenceItem(_insertionPoint, 0f, null, callback);
            _items.Add(item);
            _lastStartTime = item.StartTime;
            return this;
        }

        public Sequence Join(IPlayable playable)
        {
            ThrowIfInvalidPlayable(playable);

            if (playable is ITween tween && tween.IsSequenced)
            {
                throw new InvalidOperationException("Tween is already part of a sequence.");
            }

            if (playable is Tween t)
            {
                t.IsSequenced = true;
            }

            SequenceItem item = new SequenceItem(_lastStartTime, playable.Duration, playable, null);
            _items.Add(item);
            _duration = Mathf.Max(_duration, item.StartTime + playable.Duration);
            return this;
        }

        public Sequence Insert(float atTime, IPlayable playable)
        {
            ThrowIfInvalidPlayable(playable);

            if (playable is ITween tween && tween.IsSequenced)
            {
                throw new InvalidOperationException("Tween is already part of a sequence.");
            }

            if (playable is Tween t)
            {
                t.IsSequenced = true;
            }

            SequenceItem item = new SequenceItem(atTime, playable.Duration, playable, null);
            _items.Add(item);
            _lastStartTime = item.StartTime;
            _duration = Mathf.Max(_duration, atTime + playable.Duration);
            return this;
        }

        public IPlayable SetLoops(int count, LoopType loopType)
        {
            LoopCount = count;
            LoopType = loopType;
            return this;
        }

        public void Play()
        {
            if (IsKilled || IsComplete)
            {
                throw new InvalidOperationException("Cannot play a sequence that has been killed or completed.");
            }

            if (_isPaused)
            {
                _isPaused = false;

                foreach (SequenceItem item in _items)
                {
                    if (item.Playable != null && item.Started)
                    {
                        item.Playable.Play();
                    }
                }

                return;
            }

            if (!_isPlaying)
            {
                Reset();
                _isPlaying = true;
            }
        }

        public void Pause()
        {
            if (!_isPlaying || _isPaused)
            {
                return;
            }

            _isPaused = true;

            foreach (SequenceItem item in _items)
            {
                if (item.Playable != null)
                {
                    item.Playable.Pause();
                }
            }
        }

        public void Kill()
        {
            if (_isKilled)
            {
                return;
            }

            foreach (SequenceItem item in _items)
            {
                if (item.Playable != null)
                {
                    item.Playable.Kill();
                }
            }

            _isKilled = true;
            _isPlaying = false;
            _isPaused = false;
            OnKill?.Invoke();
        }

        public void Complete()
        {
            if (_isComplete)
            {
                return;
            }

            _isComplete = true;
            _isKilled = true;
            _isPlaying = false;
            _isPaused = false;
            _elapsed = _duration;

            foreach (SequenceItem item in _items)
            {
                if (!item.Started)
                {
                    item.Started = true;
                    if (item.Callback != null)
                    {
                        item.Callback.Invoke();
                    }
                }

                if (item.Playable != null)
                {
                    item.Playable.Complete();
                }
            }

            OnUpdate?.Invoke();
            OnComplete?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if (!_isPlaying || _isKilled || _isComplete)
            {
                return;
            }

            float previousElapsed = _elapsed;
            _elapsed += deltaTime;

            foreach (SequenceItem item in _items)
            {
                if (!item.Started && _elapsed >= item.StartTime)
                {
                    item.Started = true;
                    if (item.Playable != null)
                    {
                        item.Playable.Play();
                    }
                    else if (item.Callback != null)
                    {
                        item.Callback.Invoke();
                    }
                }

                if (item.Playable != null && item.Playable.IsPlaying)
                {
                    float effectiveDelta = deltaTime;
                    if (previousElapsed < item.StartTime)
                    {
                        effectiveDelta = _elapsed - item.StartTime;
                    }

                    item.Playable.Tick(effectiveDelta);
                }
            }

            OnUpdate?.Invoke();

            if (_elapsed >= _duration)
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
                Reset();
                _isPlaying = true;

                // Note: Full backward playback for Sequence Yoyo would require
                // all child playables to support reverse ticking, which they do not.
                // For now, Yoyo on a Sequence behaves the same as Restart.
            }
        }

        public void Reset()
        {
            _elapsed = 0f;
            _isComplete = false;
            _isPlaying = false;
            _isPaused = false;
            _isKilled = false;
            _currentLoop = 0;

            foreach (SequenceItem item in _items)
            {
                item.Started = false;
                item.Playable?.Reset();
            }
        }

        private void ThrowIfInvalidPlayable(IPlayable playable)
        {
            if (playable.LoopCount != 1)
            {
                throw new InvalidOperationException("Cannot add a looping playable to a sequence.");
            }
        }
    }
}
