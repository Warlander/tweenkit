using NUnit.Framework;

namespace Warlogic.Tweenkit.Tests
{
    public class ManualTweenTickerTests
    {
        [Test]
        public void TickManual_FiresEventWithExactValue()
        {
            // Arrange
            ManualTweenTicker ticker = new ManualTweenTicker();
            float receivedDelta = 0f;
            ticker.Tick += delta => receivedDelta = delta;

            // Act
            ticker.TickManual(0.016f);

            // Assert
            Assert.AreEqual(0.016f, receivedDelta);
        }
    }
}
