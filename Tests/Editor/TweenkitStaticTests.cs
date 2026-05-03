using NUnit.Framework;

namespace Warlogic.Tweenkit.Tests
{
    public class TweenkitStaticTests
    {
        [TearDown]
        public void TearDown()
        {
            Tweenkit.Shutdown();
        }

        [Test]
        public void Initialize_WithExplicitEngine_SetsEngine()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweenkit.Initialize(engine);

            // Assert
            Assert.AreEqual(engine, Tweenkit.Engine);
            engine.Dispose();
        }

        [Test]
        public void Initialize_WithNullEngine_CreatesDefaultEngine()
        {
            // Arrange
            Assert.IsNull(Tweenkit.Engine);

            // Act
            Tweenkit.Initialize();

            // Assert
            Assert.IsNotNull(Tweenkit.Engine);
        }

        [Test]
        public void Sequence_WithExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Sequence sequence = Tweenkit.Sequence(engine);
            sequence.AppendCallback(() => { });
            sequence.Play();
            ticker.TickManual(1f);

            // Assert
            Assert.IsTrue(sequence.IsComplete);
            Assert.AreEqual(0, engine.ActiveTweenCount);
            engine.Dispose();
        }

        [Test]
        public void Sequence_WithNoEngineAndInitialized_RegistersWithDefaultEngine()
        {
            // Arrange
            Tweenkit.Initialize();
            Sequence sequence = Tweenkit.Sequence();
            sequence.AppendCallback(() => { });

            // Act
            sequence.Play();

            // Assert
            Assert.IsTrue(sequence.IsPlaying);
        }
    }
}
