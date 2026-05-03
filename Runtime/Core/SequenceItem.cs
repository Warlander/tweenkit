using System;

namespace Warlogic.Tweenkit
{
    public class SequenceItem
    {
        public SequenceItem(float startTime, float duration, IPlayable playable, Action callback)
        {
            StartTime = startTime;
            Duration = duration;
            Playable = playable;
            Callback = callback;
        }

        public float StartTime { get; }
        public float Duration { get; }
        public IPlayable Playable { get; }
        public Action Callback { get; }
        public bool Started { get; set; }
    }
}
