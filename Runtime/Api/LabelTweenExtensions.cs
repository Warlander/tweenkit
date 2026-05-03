using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit
{
    public static class LabelTweenExtensions
    {
        public static Tweener<string> ToTypewriter(this Label target, string fullText, ITweenEngine engine = null)
        {
            Tweener<string> tween = new Tweener<string>
            {
                From = string.Empty,
                To = fullText,
                Lerp = (from, to, t) =>
                {
                    if (string.IsNullOrEmpty(to))
                    {
                        return to;
                    }

                    int count = Mathf.FloorToInt(t * to.Length);
                    if (count >= to.Length)
                    {
                        return to;
                    }

                    return to.Substring(0, count);
                },
                Apply = text => target.text = text
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        private static void SetupAndRegister(Label target, Tween tween, ITweenEngine engine = null)
        {
            if (target.panel != null)
            {
                tween.SetAliveCondition(() => target.panel != null);
            }

            ITweenEngine targetEngine = engine ?? Tweenkit.Engine;
            if (targetEngine == null)
            {
                throw new InvalidOperationException("Tweenkit not initialized.");
            }

            targetEngine.Register(tween);
        }
    }
}
