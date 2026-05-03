using System;

namespace Warlogic.Tweenkit
{
    public interface ITweenTicker
    {
        event Action<float> Tick;
    }
}
