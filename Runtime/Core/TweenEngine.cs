using System;
using System.Collections.Generic;
using UnityEngine;

namespace Warlogic.Tweenkit
{
    public class TweenEngine : ITweenEngine
    {
        private readonly List<IPlayable> _activeTweens = new List<IPlayable>();
        private readonly ITweenTicker _ticker;
        private bool _disposed;

        public bool IsDisposed
        {
            get { return _disposed; }
        }

        public int ActiveTweenCount
        {
            get
            {
                ThrowIfDisposed();
                return _activeTweens.Count;
            }
        }

        public TweenEngine(ITweenTicker ticker)
        {
            _ticker = ticker;
            _ticker.Tick += OnTick;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _ticker.Tick -= OnTick;
            KillAll();
            _disposed = true;
        }

        public void Register(IPlayable playable)
        {
            ThrowIfDisposed();

            if (playable is ITween tween && tween.IsSequenced)
            {
                return;
            }

            if (_activeTweens.Contains(playable))
            {
                return;
            }

            _activeTweens.Add(playable);
            playable.Play();
        }

        public void Unregister(IPlayable playable)
        {
            ThrowIfDisposed();
            _activeTweens.Remove(playable);
        }

        public void KillAll()
        {
            if (_disposed)
            {
                _activeTweens.Clear();
                return;
            }

            for (int i = _activeTweens.Count - 1; i >= 0; i--)
            {
                _activeTweens[i].Kill();
            }

            _activeTweens.Clear();
        }

        private void OnTick(float deltaTime)
        {
            for (int i = _activeTweens.Count - 1; i >= 0; i--)
            {
                IPlayable playable = _activeTweens[i];

                if (playable is ITween tween && tween.IsSequenced)
                {
                    _activeTweens.RemoveAt(i);
                    continue;
                }

                if (playable.IsPaused)
                {
                    continue;
                }

                if (!playable.IsPlaying || playable.IsKilled || playable.IsComplete)
                {
                    _activeTweens.RemoveAt(i);
                    continue;
                }

                try
                {
                    playable.Tick(deltaTime);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Tween tick failed: {ex.GetType().Name}: {ex.Message}");
                }

                if (i >= _activeTweens.Count || _activeTweens[i] != playable)
                {
                    continue;
                }

                if (!playable.IsPlaying || playable.IsKilled || playable.IsComplete)
                {
                    _activeTweens.RemoveAt(i);
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(TweenEngine));
            }
        }
    }
}
