using NUnit.Framework;
using UnityEngine;

namespace Warlogic.Tweenkit.Tests
{
    public class TweenTests
    {
        [Test]
        public void Delay_IsRespected()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDelay(0.5f);

            // Act
            tween.Play();
            tween.Tick(0.3f);

            // Assert
            Assert.IsFalse(tween.IsComplete);
            Assert.IsTrue(tween.IsPlaying);
        }

        [Test]
        public void Duration_IsRespected()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert
            Assert.IsFalse(tween.IsComplete);
        }

        [Test]
        public void Easing_IsApplied()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetEase(Ease.InQuad);
            tween.Apply = v => appliedValue = v;

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(0.25f, appliedValue, 0.001f);
        }

        [Test]
        public void OnComplete_FiresAtEnd()
        {
            // Arrange
            bool completeFired = false;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.OnComplete += () => completeFired = true;

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.IsTrue(completeFired);
        }

        [Test]
        public void OnUpdate_FiresDuringPlayback()
        {
            // Arrange
            int updateCount = 0;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.OnUpdate += () => updateCount++;

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, updateCount);
        }

        [Test]
        public void OnKill_FiresOnKill()
        {
            // Arrange
            bool killFired = false;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.OnKill += () => killFired = true;

            // Act
            tween.Play();
            tween.Kill();

            // Assert
            Assert.IsTrue(killFired);
        }

        [Test]
        public void Kill_DoesNotSetIsComplete()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.Play();

            // Act
            tween.Kill();

            // Assert
            Assert.IsTrue(tween.IsKilled);
            Assert.IsFalse(tween.IsComplete);
        }

        [Test]
        public void Complete_SetsIsKilled()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.Play();

            // Act
            tween.Complete();

            // Assert
            Assert.IsTrue(tween.IsComplete);
            Assert.IsTrue(tween.IsKilled);
        }

        [Test]
        public void Complete_SnapsToEnd()
        {
            // Arrange
            float finalValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.To = 10f;
            tween.Apply = v => finalValue = v;

            // Act
            tween.Play();
            tween.Complete();

            // Assert
            Assert.AreEqual(10f, finalValue);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void Complete_FiresOnUpdateBeforeOnComplete()
        {
            // Arrange
            int updateCount = 0;
            int completeCount = 0;
            int updateCountAtComplete = -1;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.OnUpdate += () =>
            {
                updateCount++;
            };
            tween.OnComplete += () =>
            {
                completeCount++;
                updateCountAtComplete = updateCount;
            };

            // Act
            tween.Play();
            tween.Complete();

            // Assert
            Assert.AreEqual(1, updateCount);
            Assert.AreEqual(1, completeCount);
            Assert.AreEqual(1, updateCountAtComplete);
        }

        [Test]
        public void ManualTicking_WorksIndependentlyOfEngine()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.To = 10f;
            tween.Apply = v => appliedValue = v;

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(10f, appliedValue);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void Play_AfterComplete_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.Play();
            tween.Tick(1f);
            Assert.IsTrue(tween.IsComplete);

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => tween.Play());
        }

        [Test]
        public void Play_AfterKill_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.Play();
            tween.Kill();
            Assert.IsTrue(tween.IsKilled);

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => tween.Play());
        }

        [Test]
        public void Pause_ManualTickStillAdvances()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.To = 10f;
            tween.Apply = v => appliedValue = v;
            tween.Play();
            tween.Tick(0.5f);
            Assert.AreEqual(5f, appliedValue);
            tween.Pause();

            // Act
            tween.Tick(0.25f);

            // Assert
            Assert.IsFalse(tween.IsComplete);
            Assert.AreEqual(7.5f, appliedValue);
        }

        [Test]
        public void Play_WhilePaused_ResumesWithoutReset()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.To = 10f;
            tween.Apply = v => appliedValue = v;
            tween.Play();
            tween.Tick(0.5f);
            Assert.AreEqual(5f, appliedValue);
            tween.Pause();

            // Act
            tween.Play();
            tween.Tick(0.25f);

            // Assert
            Assert.IsFalse(tween.IsComplete);
            Assert.AreEqual(7.5f, appliedValue);
        }

        private Tweener<float> CreateTween()
        {
            return new Tweener<float>
            {
                From = 0f,
                To = 1f,
                Duration = 1f,
                Lerp = Mathf.Lerp,
                Apply = _ => { }
            };
        }
    }
}
