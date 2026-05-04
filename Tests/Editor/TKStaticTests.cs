using NUnit.Framework;

namespace Warlogic.Tweenkit.Tests
{
    public class TKStaticTests
    {
        [TearDown]
        public void TearDown()
        {
            TK.Shutdown();
        }

        [Test]
        public void Initialize_WithExplicitEngine_SetsEngine()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            TK.Initialize(engine);

            // Assert
            Assert.AreEqual(engine, TK.Engine);
            engine.Dispose();
        }

        [Test]
        public void Initialize_WithNullEngine_CreatesDefaultEngine()
        {
            // Arrange
            Assert.IsNull(TK.Engine);

            // Act
            TK.Initialize();

            // Assert
            Assert.IsNotNull(TK.Engine);
        }

        [Test]
        public void Sequence_WithExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Sequence sequence = TK.Sequence(engine);
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
            TK.Initialize();
            Sequence sequence = TK.Sequence();
            sequence.AppendCallback(() => { });

            // Act
            sequence.Play();

            // Assert
            Assert.IsTrue(sequence.IsPlaying);
        }
    }
}
