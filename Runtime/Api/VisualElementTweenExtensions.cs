using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit
{
    public static class VisualElementTweenExtensions
    {
        public static Tweener<float> ToOpacity(this VisualElement target, float to, float? from = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = to,
                Lerp = Mathf.Lerp,
                Apply = value => target.style.opacity = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentOpacity(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByOpacity(this VisualElement target, float add, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.Lerp,
                Apply = value => target.style.opacity = value,
                GetFrom = () => GetCurrentOpacity(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ToScale(this VisualElement target, float to, float? from = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = to,
                Lerp = Mathf.Lerp,
                Apply = value => target.style.scale = new Scale(new Vector3(value, value, 1f))
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentScale(target).x;
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByScale(this VisualElement target, float add, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.Lerp,
                Apply = value => target.style.scale = new Scale(new Vector3(value, value, 1f)),
                GetFrom = () => GetCurrentScale(target).x,
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ToScale(this VisualElement target, Vector2 to, Vector2? from = null, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                To = to,
                Lerp = Vector2.Lerp,
                Apply = value => target.style.scale = new Scale(new Vector3(value.x, value.y, 1f))
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () =>
                {
                    Vector3 currentScale = GetCurrentScale(target);
                    return new Vector2(currentScale.x, currentScale.y);
                };
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ByScale(this VisualElement target, Vector2 add, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                Lerp = Vector2.Lerp,
                Apply = value => target.style.scale = new Scale(new Vector3(value.x, value.y, 1f)),
                GetFrom = () =>
                {
                    Vector3 currentScale = GetCurrentScale(target);
                    return new Vector2(currentScale.x, currentScale.y);
                },
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ToPosition(this VisualElement target, Vector2 to, Vector2? from = null, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                To = to,
                Lerp = Vector2.Lerp,
                Apply = value =>
                {
                    target.style.position = Position.Absolute;
                    target.style.left = value.x;
                    target.style.top = value.y;
                }
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => new Vector2(GetCurrentLeft(target), GetCurrentTop(target));
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ByPosition(this VisualElement target, Vector2 add, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                Lerp = Vector2.Lerp,
                Apply = value =>
                {
                    target.style.position = Position.Absolute;
                    target.style.left = value.x;
                    target.style.top = value.y;
                },
                GetFrom = () => new Vector2(GetCurrentLeft(target), GetCurrentTop(target)),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ToRotation(this VisualElement target, float toDegrees, float? fromDegrees = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = toDegrees,
                Lerp = Mathf.Lerp,
                Apply = value => target.style.rotate = new Rotate(value)
            };

            if (fromDegrees.HasValue)
            {
                tween.From = fromDegrees.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentRotate(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByRotation(this VisualElement target, float addDegrees, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.Lerp,
                Apply = value => target.style.rotate = new Rotate(value),
                GetFrom = () => GetCurrentRotate(target),
                GetTo = from => from + addDegrees
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ToSize(this VisualElement target, Vector2 to, Vector2? from = null, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                To = to,
                Lerp = Vector2.Lerp,
                Apply = value =>
                {
                    target.style.width = value.x;
                    target.style.height = value.y;
                }
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => new Vector2(GetCurrentWidth(target), GetCurrentHeight(target));
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> BySize(this VisualElement target, Vector2 add, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                Lerp = Vector2.Lerp,
                Apply = value =>
                {
                    target.style.width = value.x;
                    target.style.height = value.y;
                },
                GetFrom = () => new Vector2(GetCurrentWidth(target), GetCurrentHeight(target)),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        private static float GetCurrentLeft(VisualElement target)
        {
            if (target.style.left != StyleKeyword.Undefined && target.style.left.value.unit == LengthUnit.Pixel)
            {
                return target.style.left.value.value;
            }

            return target.resolvedStyle.left;
        }

        private static float GetCurrentTop(VisualElement target)
        {
            if (target.style.top != StyleKeyword.Undefined && target.style.top.value.unit == LengthUnit.Pixel)
            {
                return target.style.top.value.value;
            }

            return target.resolvedStyle.top;
        }

        private static float GetCurrentWidth(VisualElement target)
        {
            if (target.style.width != StyleKeyword.Undefined && target.style.width.value.unit == LengthUnit.Pixel)
            {
                return target.style.width.value.value;
            }

            return target.resolvedStyle.width;
        }

        private static float GetCurrentHeight(VisualElement target)
        {
            if (target.style.height != StyleKeyword.Undefined && target.style.height.value.unit == LengthUnit.Pixel)
            {
                return target.style.height.value.value;
            }

            return target.resolvedStyle.height;
        }

        private static float GetCurrentOpacity(VisualElement target)
        {
            float opacity = target.style.opacity != StyleKeyword.Undefined
                ? target.style.opacity.value
                : target.resolvedStyle.opacity;
            return float.IsNaN(opacity) ? 1f : opacity;
        }

        private static float GetCurrentRotate(VisualElement target)
        {
            return target.style.rotate != StyleKeyword.Undefined
                ? target.style.rotate.value.angle.value
                : target.resolvedStyle.rotate.angle.value;
        }

        private static Vector3 GetCurrentScale(VisualElement target)
        {
            return target.style.scale != StyleKeyword.Undefined
                ? target.style.scale.value.value
                : target.resolvedStyle.scale.value;
        }

        private static void SetupAndRegister(VisualElement target, Tween tween, ITweenEngine engine = null)
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
