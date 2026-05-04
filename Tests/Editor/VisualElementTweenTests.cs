using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit.Tests
{
    public class VisualElementTweenTests
    {
        [SetUp]
        public void SetUp()
        {
            Tweenkit.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            Tweenkit.Shutdown();
        }

        [Test]
        public void ToOpacity_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToOpacity(0.5f, 0f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(0.5f, element.style.opacity.value, 0.001f);
        }

        [Test]
        public void ToScaleFloat_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToScale(2f, 1f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(2f, element.style.scale.value.value.x, 0.001f);
            Assert.AreEqual(2f, element.style.scale.value.value.y, 0.001f);
        }

        [Test]
        public void ToScaleVector2_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<Vector2> tween = element.ToScale(new Vector2(2f, 3f), Vector2.one);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(2f, element.style.scale.value.value.x, 0.001f);
            Assert.AreEqual(3f, element.style.scale.value.value.y, 0.001f);
        }

        [Test]
        public void ToPosition_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<Vector2> tween = element.ToPosition(new Vector2(100f, 200f), Vector2.zero);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(100f, element.style.left.value.value, 0.001f);
            Assert.AreEqual(200f, element.style.top.value.value, 0.001f);
            Assert.AreEqual(Position.Absolute, element.style.position.value);
        }

        [Test]
        public void ByPosition_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.left = 10f;
            element.style.top = 20f;
            Tweener<Vector2> tween = element.ByPosition(new Vector2(50f, 30f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(60f, element.style.left.value.value, 0.001f);
            Assert.AreEqual(50f, element.style.top.value.value, 0.001f);
            Assert.AreEqual(Position.Absolute, element.style.position.value);
        }

        [Test]
        public void ToRotation_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToRotation(90f, 0f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(90f, element.style.rotate.value.angle.value, 0.001f);
        }

        [Test]
        public void ByRotation_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.rotate = new Rotate(15f);
            Tweener<float> tween = element.ByRotation(45f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(60f, element.style.rotate.value.angle.value, 0.001f);
        }

        [Test]
        public void ToSize_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<Vector2> tween = element.ToSize(new Vector2(300f, 400f), Vector2.zero);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(300f, element.style.width.value.value, 0.001f);
            Assert.AreEqual(400f, element.style.height.value.value, 0.001f);
        }

        [Test]
        public void BySize_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.width = 100f;
            element.style.height = 80f;
            Tweener<Vector2> tween = element.BySize(new Vector2(50f, 20f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(150f, element.style.width.value.value, 0.001f);
            Assert.AreEqual(100f, element.style.height.value.value, 0.001f);
        }

        [Test]
        public void ToOpacity_ExplicitFrom_IsRespected()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToOpacity(1f, 0.5f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(0.75f, element.style.opacity.value, 0.001f);
        }

        [Test]
        public void ByOpacity_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.opacity = 0.5f;
            Tweener<float> tween = element.ByOpacity(0.3f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(0.8f, element.style.opacity.value, 0.001f);
        }

        [Test]
        public void ByScaleFloat_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.scale = new Scale(new Vector3(1f, 1f, 1f));
            Tweener<float> tween = element.ByScale(0.5f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.scale.value.value.x, 0.001f);
            Assert.AreEqual(1.5f, element.style.scale.value.value.y, 0.001f);
        }

        [Test]
        public void ByScaleVector2_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.scale = new Scale(new Vector3(1f, 1f, 1f));
            Tweener<Vector2> tween = element.ByScale(new Vector2(0.5f, 0.25f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.scale.value.value.x, 0.001f);
            Assert.AreEqual(1.25f, element.style.scale.value.value.y, 0.001f);
        }

        [Test]
        public void ToOpacity_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToOpacity(0.5f, 0f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(0.5f, element.style.opacity.value, 0.001f);
            Assert.AreEqual(0, engine.ActiveTweenCount);
            engine.Dispose();
        }

        [Test]
        public void ByOpacity_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.opacity = 0.5f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByOpacity(0.3f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(0.8f, element.style.opacity.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToScaleFloat_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToScale(2f, 1f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(2f, element.style.scale.value.value.x, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByScaleFloat_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.scale = new Scale(new Vector3(1f, 1f, 1f));
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByScale(0.5f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.scale.value.value.x, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToScaleVector2_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ToScale(new Vector2(2f, 3f), Vector2.one, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(2f, element.style.scale.value.value.x, 0.001f);
            Assert.AreEqual(3f, element.style.scale.value.value.y, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByScaleVector2_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.scale = new Scale(new Vector3(1f, 1f, 1f));
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ByScale(new Vector2(0.5f, 0.25f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.scale.value.value.x, 0.001f);
            Assert.AreEqual(1.25f, element.style.scale.value.value.y, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToPosition_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ToPosition(new Vector2(100f, 200f), Vector2.zero, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(100f, element.style.left.value.value, 0.001f);
            Assert.AreEqual(200f, element.style.top.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByPosition_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.left = 10f;
            element.style.top = 20f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ByPosition(new Vector2(50f, 30f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(60f, element.style.left.value.value, 0.001f);
            Assert.AreEqual(50f, element.style.top.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToRotation_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToRotation(90f, 0f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(90f, element.style.rotate.value.angle.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByRotation_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.rotate = new Rotate(15f);
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByRotation(45f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(60f, element.style.rotate.value.angle.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToSize_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ToSize(new Vector2(300f, 400f), Vector2.zero, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(300f, element.style.width.value.value, 0.001f);
            Assert.AreEqual(400f, element.style.height.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void BySize_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.width = 100f;
            element.style.height = 80f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.BySize(new Vector2(50f, 20f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(150f, element.style.width.value.value, 0.001f);
            Assert.AreEqual(100f, element.style.height.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToBackgroundColor_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<Color> tween = element.ToBackgroundColor(Color.red, Color.blue);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(Color.red, element.style.backgroundColor.value);
        }

        [Test]
        public void ByBackgroundColor_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.backgroundColor = new Color(0.2f, 0.3f, 0.4f);
            Tweener<Color> tween = element.ByBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 0f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(new Color(0.3f, 0.4f, 0.5f, 1f), element.style.backgroundColor.value);
        }

        [Test]
        public void ToBackgroundColor_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Color> tween = element.ToBackgroundColor(Color.red, Color.blue, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(Color.red, element.style.backgroundColor.value);
            engine.Dispose();
        }

        [Test]
        public void ByBackgroundColor_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.backgroundColor = new Color(0.2f, 0.3f, 0.4f);
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Color> tween = element.ByBackgroundColor(new Color(0.1f, 0.1f, 0.1f, 0f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(new Color(0.3f, 0.4f, 0.5f, 1f), element.style.backgroundColor.value);
            engine.Dispose();
        }
    }
}
