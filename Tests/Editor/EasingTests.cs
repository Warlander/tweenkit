using System;
using NUnit.Framework;
using UnityEngine;

namespace Warlogic.Tweenkit.Tests
{
    public class EasingTests
    {
        [Test]
        public void GetFunction_InvalidEase_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            Ease invalidEase = (Ease)999;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => EasingFunctions.GetFunction(invalidEase));
        }

        [Test]
        public void Linear_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.Linear);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f));
            Assert.AreEqual(0.5f, ease(0.5f));
            Assert.AreEqual(1f, ease(1f));
        }

        [Test]
        public void InQuad_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InQuad);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.25f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutQuad_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutQuad);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.75f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutQuad_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutQuad);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InCubic_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InCubic);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.125f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutCubic_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutCubic);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.875f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutCubic_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutCubic);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InQuart_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InQuart);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.0625f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutQuart_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutQuart);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.9375f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutQuart_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutQuart);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InQuint_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InQuint);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.03125f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutQuint_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutQuint);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.96875f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutQuint_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutQuint);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InExpo_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InExpo);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.03125f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutExpo_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutExpo);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.96875f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutExpo_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutExpo);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InSine_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InSine);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.29289f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutSine_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutSine);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.70711f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutSine_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutSine);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InCirc_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InCirc);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.13397f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutCirc_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutCirc);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.86602f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutCirc_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutCirc);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void CustomEase_IsRespected()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = new Tweener<float>
            {
                From = 0f,
                To = 1f,
                Duration = 1f,
                Lerp = Mathf.Lerp,
                Apply = v => appliedValue = v
            };
            tween.SetEase(t => t * t * t);

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(0.125f, appliedValue, 0.001f);
        }

        [Test]
        public void SeparateEaseInOut_ProducesCorrectBlendedCurve()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = new Tweener<float>
            {
                From = 0f,
                To = 1f,
                Duration = 1f,
                Lerp = Mathf.Lerp,
                Apply = v => appliedValue = v
            };
            tween.SetEase(Ease.InQuad, Ease.OutQuad);

            // Act - at t=0.25, normalized t=0.25, ease-in on remapped 0.5 => 0.25 * 0.5 = 0.125
            tween.Play();
            tween.Tick(0.25f);

            // Assert
            Assert.AreEqual(0.125f, appliedValue, 0.001f);

            // Act - tick to t=0.75, normalized t=0.75, ease-out on remapped 0.5 => 0.75 * 0.5 + 0.5 = 0.875
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(0.875f, appliedValue, 0.001f);
        }

        [Test]
        public void InBack_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InBack);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(-0.0876975f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutBack_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutBack);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(1.0876975f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutBack_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutBack);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InElastic_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InElastic);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(-0.015625f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutElastic_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutElastic);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(1.015625f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutElastic_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutElastic);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InBounce_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InBounce);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.234375f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void OutBounce_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.OutBounce);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.765625f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }

        [Test]
        public void InOutBounce_ReturnsExpectedValues()
        {
            // Arrange
            Func<float, float> ease = EasingFunctions.GetFunction(Ease.InOutBounce);

            // Act & Assert
            Assert.AreEqual(0f, ease(0f), 0.0001f);
            Assert.AreEqual(0.5f, ease(0.5f), 0.0001f);
            Assert.AreEqual(1f, ease(1f), 0.0001f);
        }
    }
}
