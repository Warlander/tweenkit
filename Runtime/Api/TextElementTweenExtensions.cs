using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit
{
    public static class TextElementTweenExtensions
    {
        public static Tweener<Color> ToTextColor(this TextElement target, Color to, Color? from = null, ITweenEngine engine = null)
        {
            Tweener<Color> tween = new Tweener<Color>
            {
                To = to,
                Lerp = Color.LerpUnclamped,
                Apply = value => target.style.color = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentTextColor(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Color> ByTextColor(this TextElement target, Color add, ITweenEngine engine = null)
        {
            Tweener<Color> tween = new Tweener<Color>
            {
                Lerp = Color.LerpUnclamped,
                Apply = value => target.style.color = value,
                GetFrom = () => GetCurrentTextColor(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        private static Color GetCurrentTextColor(TextElement target)
        {
            return target.style.color != StyleKeyword.Undefined
                ? target.style.color.value
                : target.resolvedStyle.color;
        }

        private static void SetupAndRegister(TextElement target, Tween tween, ITweenEngine engine = null)
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
