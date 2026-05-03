using NUnit.Framework;
using UnityEngine;

namespace Warlogic.Tweenkit.Tests
{
    public class LoopTests
    {
        [Test]
        public void Restart_PlaysCorrectNumberOfTimes()
        {
            // Arrange
            int completeCount = 0;
            int updateCount = 0;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(0.5f);
            tween.SetLoops(3, LoopType.Restart);
            tween.OnComplete += () => completeCount++;
            tween.OnUpdate += () => updateCount++;

            // Act
            tween.Play();
            tween.Tick(0.5f);
            tween.Tick(0.5f);
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, completeCount);
            Assert.AreEqual(3, updateCount);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void Yoyo_ReversesDirection()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Yoyo);
            tween.From = 0f;
            tween.To = 10f;
            tween.SetEase(Ease.InQuad);
            tween.Apply = v => appliedValue = v;

            // Act - forward halfway
            tween.Play();
            tween.Tick(0.5f);

            // Assert - forward InQuad at t=0.5: 0.25 * 10 = 2.5
            Assert.AreEqual(2.5f, appliedValue, 0.001f);

            // Act - complete forward, then backward halfway
            tween.Tick(0.5f);
            tween.Tick(0.5f);

            // Assert - backward: reversed eased t = 1 - 0.25 = 0.75, value = 7.5
            Assert.AreEqual(7.5f, appliedValue, 0.001f);
        }

        [Test]
        public void OnComplete_FiresOnceAtEnd()
        {
            // Arrange
            int completeCount = 0;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(0.5f);
            tween.SetLoops(3, LoopType.Restart);
            tween.OnComplete += () => completeCount++;

            // Act
            tween.Play();
            tween.Tick(0.5f);
            tween.Tick(0.5f);
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, completeCount);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void OnUpdate_FiresEveryFrame()
        {
            // Arrange
            int updateCount = 0;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(0.5f);
            tween.SetLoops(2, LoopType.Restart);
            tween.OnUpdate += () => updateCount++;

            // Act
            tween.Play();
            tween.Tick(0.5f);
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(2, updateCount);
        }

        [Test]
        public void ManualTicking_ProducesSameResults()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Yoyo);
            tween.From = 0f;
            tween.To = 10f;
            tween.SetEase(Ease.InQuad);
            tween.Apply = v => appliedValue = v;

            // Act
            tween.Play();
            tween.Tick(0.5f); // forward halfway: 2.5
            tween.Tick(0.5f); // loop 1 complete, start backward
            tween.Tick(0.5f); // backward halfway: 7.5
            tween.Tick(0.5f); // loop 2 complete

            // Assert
            Assert.AreEqual(0f, appliedValue, 0.001f);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void InfiniteLoop_NeverCompletes()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(0.5f);
            tween.SetLoops(-1, LoopType.Restart);

            // Act
            tween.Play();
            tween.Tick(0.5f);
            tween.Tick(0.5f);
            tween.Tick(0.5f);

            // Assert
            Assert.IsFalse(tween.IsComplete);
            Assert.IsTrue(tween.IsPlaying);
        }

        [Test]
        public void Yoyo_FiniteEvenLoops_EndsAtFrom()
        {
            // Arrange
            float appliedValue = -1f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Yoyo);
            tween.From = 0f;
            tween.To = 10f;
            tween.Apply = v => appliedValue = v;

            // Act
            tween.Play();
            tween.Complete();

            // Assert
            Assert.AreEqual(0f, appliedValue, 0.001f);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void Yoyo_FiniteOddLoops_EndsAtTo()
        {
            // Arrange
            float appliedValue = -1f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(3, LoopType.Yoyo);
            tween.From = 0f;
            tween.To = 10f;
            tween.Apply = v => appliedValue = v;

            // Act
            tween.Play();
            tween.Complete();

            // Assert
            Assert.AreEqual(10f, appliedValue, 0.001f);
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void Reset_ResetsLoopState()
        {
            // Arrange
            float appliedValue = 0f;
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Yoyo);
            tween.From = 0f;
            tween.To = 10f;
            tween.SetEase(Ease.InQuad);
            tween.Apply = v => appliedValue = v;

            // Act - complete one loop, halfway through second
            tween.Play();
            tween.Tick(1f);
            tween.Tick(0.5f);

            // Assert - should be in backward loop
            Assert.AreEqual(7.5f, appliedValue, 0.001f);

            // Act - reset and play again
            tween.Reset();
            tween.Play();
            tween.Tick(0.5f);

            // Assert - should be forward again
            Assert.AreEqual(2.5f, appliedValue, 0.001f);
        }

        [Test]
        public void SetLoops_OnSequencedTween_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            Sequence seq = new Sequence();
            seq.Append(tween);

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => tween.SetLoops(2, LoopType.Restart));
        }

        [Test]
        public void Append_LoopingTween_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Restart);
            Sequence seq = new Sequence();

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => seq.Append(tween));
        }

        [Test]
        public void Join_LoopingTween_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Restart);
            Sequence seq = new Sequence();

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => seq.Join(tween));
        }

        [Test]
        public void Insert_LoopingTween_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween();
            tween.SetDuration(1f);
            tween.SetLoops(2, LoopType.Restart);
            Sequence seq = new Sequence();

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => seq.Insert(0f, tween));
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
