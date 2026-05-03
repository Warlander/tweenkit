using System;
using UnityEngine;

namespace Warlogic.Tweenkit
{
    public class GameObjectTweenTicker : MonoBehaviour, ITweenTicker
    {
        public event Action<float> Tick;

        private Action _onDestroyCallback;

        public void SetOnDestroyCallback(Action callback)
        {
            _onDestroyCallback = callback;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            Tick?.Invoke(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _onDestroyCallback?.Invoke();
        }
    }
}
