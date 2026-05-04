using System;

namespace Warlogic.Tweenkit
{
    public interface ITweenEngine : IDisposable
    {
        void Register(IPlayable playable);
        void Unregister(IPlayable playable);
        void KillAll();
        int ActiveTweenCount { get; }
        bool IsDisposed { get; }
    }
}
