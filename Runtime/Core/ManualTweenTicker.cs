using System;

namespace Warlogic.Tweenkit
{
    public class ManualTweenTicker : ITweenTicker
    {
        public event Action<float> Tick;

        public void TickManual(float deltaTime)
        {
            Tick?.Invoke(deltaTime);
        }
    }
}
