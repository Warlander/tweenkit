using System;

namespace Warlogic.Tweenkit
{
    public class Tweener<T> : Tween
    {
        public T From { get; set; }
        public T To { get; set; }
        public Func<T, T, float, T> Lerp { get; set; }
        public Action<T> Apply { get; set; }
        public Func<T> GetFrom { get; set; }
        public Func<T, T> GetTo { get; set; }

        private bool _evaluated;

        public override void Reset()
        {
            base.Reset();
            _evaluated = false;
        }

        protected override void ApplyValue(float t)
        {
            if (Lerp == null || Apply == null)
            {
                return;
            }

            if (!_evaluated)
            {
                _evaluated = true;
                if (GetFrom != null)
                {
                    From = GetFrom();
                }
                if (GetTo != null)
                {
                    To = GetTo(From);
                }
            }

            T value = Lerp(From, To, t);
            Apply(value);
        }
    }
}
