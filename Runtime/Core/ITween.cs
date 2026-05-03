using System;

namespace Warlogic.Tweenkit
{
    public interface ITween : IPlayable
    {
        bool IsSequenced { get; }
    }
}
