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
                Lerp = Mathf.LerpUnclamped,
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
                Lerp = Mathf.LerpUnclamped,
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
                Lerp = Mathf.LerpUnclamped,
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
                Lerp = Mathf.LerpUnclamped,
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
                Lerp = Vector2.LerpUnclamped,
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
                Lerp = Vector2.LerpUnclamped,
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
                Lerp = Vector2.LerpUnclamped,
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
                Lerp = Vector2.LerpUnclamped,
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
                Lerp = Mathf.LerpUnclamped,
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
                Lerp = Mathf.LerpUnclamped,
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
                Lerp = Vector2.LerpUnclamped,
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
                Lerp = Vector2.LerpUnclamped,
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

        public static Tweener<float> ToWidth(this VisualElement target, float to, float? from = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = to,
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.width = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentWidth(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByWidth(this VisualElement target, float add, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.width = value,
                GetFrom = () => GetCurrentWidth(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ToHeight(this VisualElement target, float to, float? from = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = to,
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.height = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentHeight(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByHeight(this VisualElement target, float add, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.height = value,
                GetFrom = () => GetCurrentHeight(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ToFlexGrow(this VisualElement target, float to, float? from = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = to,
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.flexGrow = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentFlexGrow(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByFlexGrow(this VisualElement target, float add, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.flexGrow = value,
                GetFrom = () => GetCurrentFlexGrow(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ToFlexShrink(this VisualElement target, float to, float? from = null, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                To = to,
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.flexShrink = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentFlexShrink(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<float> ByFlexShrink(this VisualElement target, float add, ITweenEngine engine = null)
        {
            Tweener<float> tween = new Tweener<float>
            {
                Lerp = Mathf.LerpUnclamped,
                Apply = value => target.style.flexShrink = value,
                GetFrom = () => GetCurrentFlexShrink(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector4> ToMargin(this VisualElement target, Vector4 to, Vector4? from = null, ITweenEngine engine = null)
        {
            Tweener<Vector4> tween = new Tweener<Vector4>
            {
                To = to,
                Lerp = Vector4.LerpUnclamped,
                Apply = value =>
                {
                    target.style.marginLeft = value.x;
                    target.style.marginTop = value.y;
                    target.style.marginRight = value.z;
                    target.style.marginBottom = value.w;
                }
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentMargin(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector4> ByMargin(this VisualElement target, Vector4 add, ITweenEngine engine = null)
        {
            Tweener<Vector4> tween = new Tweener<Vector4>
            {
                Lerp = Vector4.LerpUnclamped,
                Apply = value =>
                {
                    target.style.marginLeft = value.x;
                    target.style.marginTop = value.y;
                    target.style.marginRight = value.z;
                    target.style.marginBottom = value.w;
                },
                GetFrom = () => GetCurrentMargin(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector4> ToPadding(this VisualElement target, Vector4 to, Vector4? from = null, ITweenEngine engine = null)
        {
            Tweener<Vector4> tween = new Tweener<Vector4>
            {
                To = to,
                Lerp = Vector4.LerpUnclamped,
                Apply = value =>
                {
                    target.style.paddingLeft = value.x;
                    target.style.paddingTop = value.y;
                    target.style.paddingRight = value.z;
                    target.style.paddingBottom = value.w;
                }
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentPadding(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector4> ByPadding(this VisualElement target, Vector4 add, ITweenEngine engine = null)
        {
            Tweener<Vector4> tween = new Tweener<Vector4>
            {
                Lerp = Vector4.LerpUnclamped,
                Apply = value =>
                {
                    target.style.paddingLeft = value.x;
                    target.style.paddingTop = value.y;
                    target.style.paddingRight = value.z;
                    target.style.paddingBottom = value.w;
                },
                GetFrom = () => GetCurrentPadding(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ToTranslate(this VisualElement target, Vector2 to, Vector2? from = null, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                To = to,
                Lerp = Vector2.LerpUnclamped,
                Apply = value => target.style.translate = new Translate(value.x, value.y)
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentTranslate(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Vector2> ByTranslate(this VisualElement target, Vector2 add, ITweenEngine engine = null)
        {
            Tweener<Vector2> tween = new Tweener<Vector2>
            {
                Lerp = Vector2.LerpUnclamped,
                Apply = value => target.style.translate = new Translate(value.x, value.y),
                GetFrom = () => GetCurrentTranslate(target),
                GetTo = from => from + add
            };

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Color> ToBackgroundColor(this VisualElement target, Color to, Color? from = null, ITweenEngine engine = null)
        {
            Tweener<Color> tween = new Tweener<Color>
            {
                To = to,
                Lerp = Color.LerpUnclamped,
                Apply = value => target.style.backgroundColor = value
            };

            if (from.HasValue)
            {
                tween.From = from.Value;
            }
            else
            {
                tween.GetFrom = () => GetCurrentBackgroundColor(target);
            }

            SetupAndRegister(target, tween, engine);
            return tween;
        }

        public static Tweener<Color> ByBackgroundColor(this VisualElement target, Color add, ITweenEngine engine = null)
        {
            Tweener<Color> tween = new Tweener<Color>
            {
                Lerp = Color.LerpUnclamped,
                Apply = value => target.style.backgroundColor = value,
                GetFrom = () => GetCurrentBackgroundColor(target),
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

        private static float GetCurrentFlexGrow(VisualElement target)
        {
            if (target.style.flexGrow != StyleKeyword.Undefined)
            {
                return target.style.flexGrow.value;
            }

            return target.resolvedStyle.flexGrow;
        }

        private static float GetCurrentFlexShrink(VisualElement target)
        {
            if (target.style.flexShrink != StyleKeyword.Undefined)
            {
                return target.style.flexShrink.value;
            }

            return target.resolvedStyle.flexShrink;
        }

        private static Vector4 GetCurrentMargin(VisualElement target)
        {
            float left = target.style.marginLeft != StyleKeyword.Undefined
                ? target.style.marginLeft.value.value
                : target.resolvedStyle.marginLeft;
            float top = target.style.marginTop != StyleKeyword.Undefined
                ? target.style.marginTop.value.value
                : target.resolvedStyle.marginTop;
            float right = target.style.marginRight != StyleKeyword.Undefined
                ? target.style.marginRight.value.value
                : target.resolvedStyle.marginRight;
            float bottom = target.style.marginBottom != StyleKeyword.Undefined
                ? target.style.marginBottom.value.value
                : target.resolvedStyle.marginBottom;
            return new Vector4(left, top, right, bottom);
        }

        private static Vector4 GetCurrentPadding(VisualElement target)
        {
            float left = target.style.paddingLeft != StyleKeyword.Undefined
                ? target.style.paddingLeft.value.value
                : target.resolvedStyle.paddingLeft;
            float top = target.style.paddingTop != StyleKeyword.Undefined
                ? target.style.paddingTop.value.value
                : target.resolvedStyle.paddingTop;
            float right = target.style.paddingRight != StyleKeyword.Undefined
                ? target.style.paddingRight.value.value
                : target.resolvedStyle.paddingRight;
            float bottom = target.style.paddingBottom != StyleKeyword.Undefined
                ? target.style.paddingBottom.value.value
                : target.resolvedStyle.paddingBottom;
            return new Vector4(left, top, right, bottom);
        }

        private static Vector2 GetCurrentTranslate(VisualElement target)
        {
            if (target.style.translate != StyleKeyword.Undefined)
            {
                Translate t = target.style.translate.value;
                return new Vector2(t.x.value, t.y.value);
            }

            Translate rt = target.resolvedStyle.translate;
            return new Vector2(rt.x.value, rt.y.value);
        }

        private static Color GetCurrentBackgroundColor(VisualElement target)
        {
            if (target.style.backgroundColor != StyleKeyword.Undefined)
            {
                return target.style.backgroundColor.value;
            }

            return target.resolvedStyle.backgroundColor;
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
