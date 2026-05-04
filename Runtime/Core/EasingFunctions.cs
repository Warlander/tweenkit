using System;
using UnityEngine;

namespace Warlogic.Tweenkit
{
    public static class EasingFunctions
    {
        public static Func<float, float> GetFunction(Ease ease)
        {
            switch (ease)
            {
                case Ease.Linear:
                    return Linear;
                case Ease.InSine:
                    return InSine;
                case Ease.OutSine:
                    return OutSine;
                case Ease.InOutSine:
                    return InOutSine;
                case Ease.InQuad:
                    return InQuad;
                case Ease.OutQuad:
                    return OutQuad;
                case Ease.InOutQuad:
                    return InOutQuad;
                case Ease.InCubic:
                    return InCubic;
                case Ease.OutCubic:
                    return OutCubic;
                case Ease.InOutCubic:
                    return InOutCubic;
                case Ease.InQuart:
                    return InQuart;
                case Ease.OutQuart:
                    return OutQuart;
                case Ease.InOutQuart:
                    return InOutQuart;
                case Ease.InQuint:
                    return InQuint;
                case Ease.OutQuint:
                    return OutQuint;
                case Ease.InOutQuint:
                    return InOutQuint;
                case Ease.InExpo:
                    return InExpo;
                case Ease.OutExpo:
                    return OutExpo;
                case Ease.InOutExpo:
                    return InOutExpo;
                case Ease.InCirc:
                    return InCirc;
                case Ease.OutCirc:
                    return OutCirc;
                case Ease.InOutCirc:
                    return InOutCirc;
                case Ease.InBack:
                    return InBack;
                case Ease.OutBack:
                    return OutBack;
                case Ease.InOutBack:
                    return InOutBack;
                case Ease.InElastic:
                    return InElastic;
                case Ease.OutElastic:
                    return OutElastic;
                case Ease.InOutElastic:
                    return InOutElastic;
                case Ease.InBounce:
                    return InBounce;
                case Ease.OutBounce:
                    return OutBounce;
                case Ease.InOutBounce:
                    return InOutBounce;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ease), ease, null);
            }
        }

        public static float Linear(float t)
        {
            return t;
        }

        public static float InSine(float t)
        {
            return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        }

        public static float OutSine(float t)
        {
            return Mathf.Sin(t * Mathf.PI * 0.5f);
        }

        public static float InOutSine(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1f) * 0.5f;
        }

        public static float InQuad(float t)
        {
            return t * t;
        }

        public static float OutQuad(float t)
        {
            return 1f - (1f - t) * (1f - t);
        }

        public static float InOutQuad(float t)
        {
            return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) * 0.5f;
        }

        public static float InCubic(float t)
        {
            return t * t * t;
        }

        public static float OutCubic(float t)
        {
            return 1f - Mathf.Pow(1f - t, 3f);
        }

        public static float InOutCubic(float t)
        {
            return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) * 0.5f;
        }

        public static float InQuart(float t)
        {
            return t * t * t * t;
        }

        public static float OutQuart(float t)
        {
            return 1f - Mathf.Pow(1f - t, 4f);
        }

        public static float InOutQuart(float t)
        {
            return t < 0.5f ? 8f * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 4f) * 0.5f;
        }

        public static float InQuint(float t)
        {
            return t * t * t * t * t;
        }

        public static float OutQuint(float t)
        {
            return 1f - Mathf.Pow(1f - t, 5f);
        }

        public static float InOutQuint(float t)
        {
            return t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5f) * 0.5f;
        }

        public static float InExpo(float t)
        {
            return t == 0f ? 0f : Mathf.Pow(2f, 10f * (t - 1f));
        }

        public static float OutExpo(float t)
        {
            return t == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * t);
        }

        public static float InOutExpo(float t)
        {
            if (t == 0f)
            {
                return 0f;
            }

            if (t == 1f)
            {
                return 1f;
            }

            return t < 0.5f ? Mathf.Pow(2f, 20f * t - 10f) * 0.5f : (2f - Mathf.Pow(2f, -20f * t + 10f)) * 0.5f;
        }

        public static float InCirc(float t)
        {
            return 1f - Mathf.Sqrt(1f - Mathf.Pow(t, 2f));
        }

        public static float OutCirc(float t)
        {
            return Mathf.Sqrt(1f - Mathf.Pow(t - 1f, 2f));
        }

        public static float InOutCirc(float t)
        {
            return t < 0.5f
                ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2f))) * 0.5f
                : (Mathf.Sqrt(1f - Mathf.Pow(-2f * t + 2f, 2f)) + 1f) * 0.5f;
        }

        public static float InBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            return c3 * t * t * t - c1 * t * t;
        }

        public static float OutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
        }

        public static float InOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;
            if (t < 0.5f)
            {
                return (Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) * 0.5f;
            }
            else
            {
                return (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) * 0.5f;
            }
        }

        public static float InElastic(float t)
        {
            const float c4 = (2f * Mathf.PI) / 3f;
            if (t == 0f)
            {
                return 0f;
            }
            if (t == 1f)
            {
                return 1f;
            }
            return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c4);
        }

        public static float OutElastic(float t)
        {
            const float c4 = (2f * Mathf.PI) / 3f;
            if (t == 0f)
            {
                return 0f;
            }
            if (t == 1f)
            {
                return 1f;
            }
            return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
        }

        public static float InOutElastic(float t)
        {
            const float c5 = (2f * Mathf.PI) / 4.5f;
            if (t == 0f)
            {
                return 0f;
            }
            if (t == 1f)
            {
                return 1f;
            }
            if (t < 0.5f)
            {
                return -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) * 0.5f;
            }
            else
            {
                return (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) * 0.5f + 1f;
            }
        }

        public static float InBounce(float t)
        {
            return 1f - OutBounce(1f - t);
        }

        public static float OutBounce(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;
            if (t < 1f / d1)
            {
                return n1 * t * t;
            }
            else if (t < 2f / d1)
            {
                float t2 = t - 1.5f / d1;
                return n1 * t2 * t2 + 0.75f;
            }
            else if (t < 2.5f / d1)
            {
                float t2 = t - 2.25f / d1;
                return n1 * t2 * t2 + 0.9375f;
            }
            else
            {
                float t2 = t - 2.625f / d1;
                return n1 * t2 * t2 + 0.984375f;
            }
        }

        public static float InOutBounce(float t)
        {
            if (t < 0.5f)
            {
                return (1f - OutBounce(1f - 2f * t)) * 0.5f;
            }
            else
            {
                return (1f + OutBounce(2f * t - 1f)) * 0.5f;
            }
        }
    }
}
