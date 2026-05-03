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
    }
}
