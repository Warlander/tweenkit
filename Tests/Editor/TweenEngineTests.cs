using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Warlogic.Tweenkit.Tests
{
    public class TweenEngineTests
    {
        [Test]
        public void Register_AddsTweenToActiveList()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();

            // Act
            engine.Register(tween);

            // Assert
            Assert.AreEqual(1, engine.ActiveTweenCount);
        }

        [Test]
        public void Unregister_RemovesTweenFromActiveList()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            engine.Register(tween);

            // Act
            engine.Unregister(tween);

            // Assert
            Assert.AreEqual(0, engine.ActiveTweenCount);
        }

        [Test]
        public void Tick_AdvancesTween()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            engine.Register(tween);

            // Act
            ticker.TickManual(0.1f);

            // Assert
            Assert.IsTrue(tween.IsPlaying);
        }

        [Test]
        public void Tick_CompletedTweenIsRemoved()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            engine.Register(tween);

            // Act
            ticker.TickManual(1.0f);

            // Assert
            Assert.IsTrue(tween.IsComplete);
            Assert.AreEqual(0, engine.ActiveTweenCount);
        }

        [Test]
        public void KillAll_RemovesAllTweens()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            engine.Register(CreateDummyTween());
            engine.Register(CreateDummyTween());

            // Act
            engine.KillAll();

            // Assert
            Assert.AreEqual(0, engine.ActiveTweenCount);
        }

        [Test]
        public void Tick_NestedKill_IsSafe()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tweenA = CreateDummyTween();
            Tweener<float> tweenB = CreateDummyTween();
            tweenA.OnUpdate += () => tweenB.Kill();
            engine.Register(tweenA);
            engine.Register(tweenB);

            // Act
            ticker.TickManual(0.1f);

            // Assert - tweenB is killed but evicted on next tick
            Assert.IsTrue(tweenB.IsKilled);
            Assert.IsFalse(tweenB.IsComplete);
            Assert.AreEqual(2, engine.ActiveTweenCount);

            // Act - next tick removes the killed tween
            ticker.TickManual(0.1f);

            // Assert
            Assert.AreEqual(1, engine.ActiveTweenCount);
        }

        [Test]
        public void Tick_PausedTween_IsNotTickedButRemainsInList()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            engine.Register(tween);
            tween.Pause();

            // Act
            ticker.TickManual(0.5f);

            // Assert
            Assert.AreEqual(1, engine.ActiveTweenCount);
            Assert.IsFalse(tween.IsComplete);
        }

        [Test]
        public void Tick_ResumedTween_ContinuesPlayback()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            engine.Register(tween);
            ticker.TickManual(0.5f);
            tween.Pause();
            ticker.TickManual(0.3f);

            // Act
            tween.Play();
            ticker.TickManual(0.5f);

            // Assert
            Assert.IsTrue(tween.IsComplete);
        }

        [Test]
        public void Tick_KilledTween_IsRemovedOnNextTick()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            engine.Register(tween);
            tween.Kill();

            // Act
            ticker.TickManual(0.1f);

            // Assert
            Assert.AreEqual(0, engine.ActiveTweenCount);
            Assert.IsFalse(tween.IsComplete);
        }

        [Test]
        public void ActiveTweenCount_ReflectsCurrentCount()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);

            // Act & Assert
            Assert.AreEqual(0, engine.ActiveTweenCount);
            engine.Register(CreateDummyTween());
            Assert.AreEqual(1, engine.ActiveTweenCount);
        }

        [Test]
        public void Tick_AliveConditionReturnsFalse_AutoKillsTween()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            tween.SetAliveCondition(() => false);
            bool killed = false;
            tween.OnKill += () => killed = true;
            engine.Register(tween);

            // Act
            ticker.TickManual(0.1f);

            // Assert
            Assert.IsTrue(killed);
            Assert.AreEqual(0, engine.ActiveTweenCount);
        }

        [Test]
        public void Tick_AliveConditionTargetDestroyed_AutoKillsTween()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> tween = CreateDummyTween();
            GameObject dummy = new GameObject();
            tween.SetAliveCondition(dummy);
            bool killed = false;
            tween.OnKill += () => killed = true;
            engine.Register(tween);

            // Act
            UnityEngine.Object.DestroyImmediate(dummy);
            ticker.TickManual(0.1f);

            // Assert
            Assert.IsTrue(killed);
            Assert.AreEqual(0, engine.ActiveTweenCount);
        }

        [Test]
        public void Dispose_UnsubscribesFromTicker()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);
            engine.Register(CreateDummyTween());

            // Act
            engine.Dispose();

            // Assert
            Assert.Throws<System.ObjectDisposedException>(() => { int _ = engine.ActiveTweenCount; });
        }

        [Test]
        public void Dispose_ThrowsOnSubsequentRegister()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);
            engine.Dispose();

            // Act & Assert
            Assert.Throws<System.ObjectDisposedException>(() => engine.Register(CreateDummyTween()));
        }

        [Test]
        public void Tick_ThrowingCallback_DoesNotPreventOtherTweensFromTicking()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            using TweenEngine engine = new TweenEngine(ticker);
            Tweener<float> throwingTween = CreateDummyTween();
            throwingTween.OnUpdate += () => throw new InvalidOperationException("boom");
            Tweener<float> normalTween = CreateDummyTween();
            engine.Register(throwingTween);
            engine.Register(normalTween);

            // Act
            LogAssert.Expect(LogType.Error, "Tween tick failed: InvalidOperationException: boom");
            ticker.TickManual(0.5f);

            // Assert
            Assert.IsFalse(normalTween.IsComplete);
            Assert.AreEqual(2, engine.ActiveTweenCount);

            // Act - another tick should advance normalTween to completion
            LogAssert.Expect(LogType.Error, "Tween tick failed: InvalidOperationException: boom");
            ticker.TickManual(0.6f);

            // Assert
            Assert.IsTrue(normalTween.IsComplete);
        }

        private Tweener<float> CreateDummyTween()
        {
            return new Tweener<float>
            {
                Duration = 1f,
                Lerp = Mathf.Lerp,
                Apply = _ => { }
            };
        }
    }
}
