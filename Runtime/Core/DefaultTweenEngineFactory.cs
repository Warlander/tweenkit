using UnityEngine;

namespace Warlogic.Tweenkit
{
    public class DefaultTweenEngineFactory
    {
        public ITweenEngine Create()
        {
            GameObject tickerObject = new GameObject("TweenkitTicker");
            GameObjectTweenTicker ticker = tickerObject.AddComponent<GameObjectTweenTicker>();
            TweenEngine engine = new TweenEngine(ticker);
            ticker.SetOnDestroyCallback(engine.Dispose);
            return engine;
        }
    }
}
