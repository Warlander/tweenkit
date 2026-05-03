using NUnit.Framework;
using UnityEngine;

namespace Warlogic.Tweenkit.Tests
{
    public class SequenceTests
    {
        [Test]
        public void Append_OrderRespected()
        {
            // Arrange
            float valueA = 0f;
            float valueB = 0f;
            Tweener<float> tweenA = CreateTween(1f, v => valueA = v);
            Tweener<float> tweenB = CreateTween(1f, v => valueB = v);
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Append(tweenB);

            // Act - complete first tween
            seq.Play();
            seq.Tick(1.0f);

            // Assert
            Assert.AreEqual(1f, valueA, 0.001f);
            Assert.AreEqual(0f, valueB, 0.001f);

            // Act - tick into second tween
            seq.Tick(0.5f);

            // Assert
            Assert.AreEqual(0.5f, valueB, 0.001f);
        }

        [Test]
        public void AppendInterval_WaitsCorrectTime()
        {
            // Arrange
            float value = 0f;
            Tweener<float> tween = CreateTween(1f, v => value = v);
            Sequence seq = new Sequence();
            seq.Append(tween);
            seq.AppendInterval(0.5f);

            // Act - complete tween and interval
            seq.Play();
            seq.Tick(1.0f);
            seq.Tick(0.5f);

            // Assert
            Assert.AreEqual(1f, value, 0.001f);
            Assert.IsTrue(seq.IsComplete);
        }

        [Test]
        public void AppendCallback_FiresOnce()
        {
            // Arrange
            int callbackCount = 0;
            Sequence seq = new Sequence();
            seq.AppendInterval(0.5f);
            seq.AppendCallback(() => callbackCount++);

            // Act
            seq.Play();
            seq.Tick(0.5f);

            // Assert
            Assert.AreEqual(1, callbackCount);
            Assert.IsTrue(seq.IsComplete);
        }

        [Test]
        public void Join_RunsInParallel()
        {
            // Arrange
            float valueA = 0f;
            float valueB = 0f;
            Tweener<float> tweenA = CreateTween(1f, v => valueA = v);
            Tweener<float> tweenB = CreateTween(0.5f, v => valueB = v);
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Join(tweenB);

            // Act - tick halfway through the longer tween
            seq.Play();
            seq.Tick(0.5f);

            // Assert
            Assert.AreEqual(0.5f, valueA, 0.001f);
            Assert.AreEqual(1f, valueB, 0.001f); // tweenB is complete (0.5s duration)
        }

        [Test]
        public void Join_SequenceDurationEqualsLongestParallelItem()
        {
            // Arrange
            Tweener<float> tweenA = CreateTween(1f, _ => { });
            Tweener<float> tweenB = CreateTween(0.5f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Join(tweenB);

            // Act & Assert
            Assert.AreEqual(1f, seq.Duration, 0.001f);
        }

        [Test]
        public void Join_SequenceExtendsWhenJoinedTweenIsLonger()
        {
            // Arrange
            Tweener<float> tweenA = CreateTween(0.5f, _ => { });
            Tweener<float> tweenB = CreateTween(1f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Join(tweenB);

            // Act & Assert
            Assert.AreEqual(1f, seq.Duration, 0.001f);
        }

        [Test]
        public void Insert_PlacesTweenAtAbsoluteTime()
        {
            // Arrange
            float value = 0f;
            Tweener<float> tweenA = CreateTween(1f, _ => { });
            Tweener<float> tweenB = CreateTween(0.5f, v => value = v);
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Insert(0.5f, tweenB);

            // Act - tick to when tweenB should be halfway
            seq.Play();
            seq.Tick(0.75f);

            // Assert
            Assert.AreEqual(0.5f, value, 0.001f);
        }

        [Test]
        public void Insert_OverlappingExpandsDuration()
        {
            // Arrange
            Tweener<float> tweenA = CreateTween(1f, _ => { });
            Tweener<float> tweenB = CreateTween(1f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Insert(0.5f, tweenB);

            // Act & Assert
            Assert.AreEqual(1.5f, seq.Duration, 0.001f);
        }

        [Test]
        public void MultipleJoins_AllStartTogether()
        {
            // Arrange
            float valueA = 0f;
            float valueB = 0f;
            float valueC = 0f;
            Tweener<float> tweenA = CreateTween(1f, v => valueA = v);
            Tweener<float> tweenB = CreateTween(0.5f, v => valueB = v);
            Tweener<float> tweenC = CreateTween(0.3f, v => valueC = v);
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Join(tweenB);
            seq.Join(tweenC);

            // Act
            seq.Play();
            seq.Tick(0.3f);

            // Assert
            Assert.AreEqual(0.3f, valueA, 0.001f);
            Assert.AreEqual(0.6f, valueB, 0.001f);
            Assert.AreEqual(1f, valueC, 0.001f);
        }

        [Test]
        public void MultipleJoins_SequenceWaitsForLongest()
        {
            // Arrange
            Tweener<float> tweenA = CreateTween(1f, _ => { });
            Tweener<float> tweenB = CreateTween(0.5f, _ => { });
            Tweener<float> tweenC = CreateTween(0.3f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Join(tweenB);
            seq.Join(tweenC);

            // Act & Assert
            Assert.AreEqual(1f, seq.Duration, 0.001f);
        }

        [Test]
        public void SequenceCompletion_FiresAfterAllSegmentsDone()
        {
            // Arrange
            bool completeFired = false;
            Tweener<float> tweenA = CreateTween(0.5f, _ => { });
            Tweener<float> tweenB = CreateTween(0.5f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Append(tweenB);
            seq.OnComplete += () => completeFired = true;

            // Act
            seq.Play();
            seq.Tick(1.0f);

            // Assert
            Assert.IsTrue(completeFired);
            Assert.IsTrue(seq.IsComplete);
        }

        [Test]
        public void Complete_FiresOnUpdateBeforeOnComplete()
        {
            // Arrange
            int updateCount = 0;
            int completeCount = 0;
            int updateCountAtComplete = -1;
            Tweener<float> tweenA = CreateTween(0.5f, _ => { });
            Tweener<float> tweenB = CreateTween(0.5f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Append(tweenB);
            seq.OnUpdate += () =>
            {
                updateCount++;
            };
            seq.OnComplete += () =>
            {
                completeCount++;
                updateCountAtComplete = updateCount;
            };

            // Act
            seq.Play();
            seq.Complete();

            // Assert
            Assert.AreEqual(1, updateCount);
            Assert.AreEqual(1, completeCount);
            Assert.AreEqual(1, updateCountAtComplete);
        }

        [Test]
        public void Play_AfterComplete_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween(1f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tween);
            seq.Play();
            seq.Tick(1f);
            Assert.IsTrue(seq.IsComplete);

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => seq.Play());
        }

        [Test]
        public void Play_AfterKill_ThrowsInvalidOperationException()
        {
            // Arrange
            Tweener<float> tween = CreateTween(1f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tween);
            seq.Play();
            seq.Kill();
            Assert.IsTrue(seq.IsKilled);

            // Act & Assert
            Assert.Throws<System.InvalidOperationException>(() => seq.Play());
        }

        [Test]
        public void Play_WhilePaused_ResumesWithoutReset()
        {
            // Arrange
            float valueA = 0f;
            float valueB = 0f;
            Tweener<float> tweenA = CreateTween(1f, v => valueA = v);
            Tweener<float> tweenB = CreateTween(1f, v => valueB = v);
            Sequence seq = new Sequence();
            seq.Append(tweenA);
            seq.Append(tweenB);
            seq.Play();
            seq.Tick(0.5f);
            Assert.AreEqual(0.5f, valueA, 0.001f);
            seq.Pause();

            // Act
            seq.Play();
            seq.Tick(0.5f);

            // Assert
            Assert.AreEqual(1f, valueA, 0.001f);
            Assert.AreEqual(0f, valueB, 0.001f);
        }

        [Test]
        public void Kill_DoesNotSetIsComplete()
        {
            // Arrange
            Tweener<float> tween = CreateTween(1f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tween);
            seq.Play();

            // Act
            seq.Kill();

            // Assert
            Assert.IsTrue(seq.IsKilled);
            Assert.IsFalse(seq.IsComplete);
        }

        [Test]
        public void Complete_SetsIsKilled()
        {
            // Arrange
            Tweener<float> tween = CreateTween(1f, _ => { });
            Sequence seq = new Sequence();
            seq.Append(tween);
            seq.Play();

            // Act
            seq.Complete();

            // Assert
            Assert.IsTrue(seq.IsComplete);
            Assert.IsTrue(seq.IsKilled);
        }

        private Tweener<float> CreateTween(float duration, System.Action<float> apply)
        {
            return new Tweener<float>
            {
                From = 0f,
                To = 1f,
                Duration = duration,
                Lerp = Mathf.Lerp,
                Apply = apply
            };
        }
    }
}
