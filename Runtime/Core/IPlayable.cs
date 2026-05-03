using System;

namespace Warlogic.Tweenkit
{
    public interface IPlayable
    {
        float Duration { get; }
        bool IsComplete { get; }
        bool IsKilled { get; }
        bool IsPlaying { get; }
        bool IsPaused { get; }
        int LoopCount { get; }
        LoopType LoopType { get; }

        event Action OnComplete;
        event Action OnUpdate;
        event Action OnKill;

        void Tick(float deltaTime);
        void Play();
        void Pause();
        void Kill();
        void Complete();
        void Reset();
        IPlayable SetLoops(int count, LoopType loopType);
    }
}
