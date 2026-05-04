using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit.Tests
{
    public class LayoutTweenTests
    {
        [SetUp]
        public void SetUp()
        {
            TK.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            TK.Shutdown();
        }

        [Test]
        public void ToWidth_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToWidth(200f, 100f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(200f, element.style.width.value.value, 0.001f);
        }

        [Test]
        public void ByWidth_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.width = 100f;
            Tweener<float> tween = element.ByWidth(50f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(150f, element.style.width.value.value, 0.001f);
        }

        [Test]
        public void ToHeight_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToHeight(200f, 100f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(200f, element.style.height.value.value, 0.001f);
        }

        [Test]
        public void ByHeight_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.height = 100f;
            Tweener<float> tween = element.ByHeight(50f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(150f, element.style.height.value.value, 0.001f);
        }

        [Test]
        public void ToFlexGrow_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToFlexGrow(2f, 0f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(2f, element.style.flexGrow.value, 0.001f);
        }

        [Test]
        public void ByFlexGrow_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.flexGrow = 1f;
            Tweener<float> tween = element.ByFlexGrow(0.5f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.flexGrow.value, 0.001f);
        }

        [Test]
        public void ToFlexShrink_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<float> tween = element.ToFlexShrink(2f, 1f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(2f, element.style.flexShrink.value, 0.001f);
        }

        [Test]
        public void ByFlexShrink_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.flexShrink = 1f;
            Tweener<float> tween = element.ByFlexShrink(0.5f);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.flexShrink.value, 0.001f);
        }

        [Test]
        public void ToMargin_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Vector4 from = new Vector4(0f, 0f, 0f, 0f);
            Vector4 to = new Vector4(10f, 20f, 30f, 40f);
            Tweener<Vector4> tween = element.ToMargin(to, from);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(10f, element.style.marginLeft.value.value, 0.001f);
            Assert.AreEqual(20f, element.style.marginTop.value.value, 0.001f);
            Assert.AreEqual(30f, element.style.marginRight.value.value, 0.001f);
            Assert.AreEqual(40f, element.style.marginBottom.value.value, 0.001f);
        }

        [Test]
        public void ByMargin_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.marginLeft = 5f;
            element.style.marginTop = 10f;
            element.style.marginRight = 15f;
            element.style.marginBottom = 20f;
            Tweener<Vector4> tween = element.ByMargin(new Vector4(1f, 2f, 3f, 4f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(6f, element.style.marginLeft.value.value, 0.001f);
            Assert.AreEqual(12f, element.style.marginTop.value.value, 0.001f);
            Assert.AreEqual(18f, element.style.marginRight.value.value, 0.001f);
            Assert.AreEqual(24f, element.style.marginBottom.value.value, 0.001f);
        }

        [Test]
        public void ToPadding_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Vector4 from = new Vector4(0f, 0f, 0f, 0f);
            Vector4 to = new Vector4(10f, 20f, 30f, 40f);
            Tweener<Vector4> tween = element.ToPadding(to, from);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(10f, element.style.paddingLeft.value.value, 0.001f);
            Assert.AreEqual(20f, element.style.paddingTop.value.value, 0.001f);
            Assert.AreEqual(30f, element.style.paddingRight.value.value, 0.001f);
            Assert.AreEqual(40f, element.style.paddingBottom.value.value, 0.001f);
        }

        [Test]
        public void ByPadding_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.paddingLeft = 5f;
            element.style.paddingTop = 10f;
            element.style.paddingRight = 15f;
            element.style.paddingBottom = 20f;
            Tweener<Vector4> tween = element.ByPadding(new Vector4(1f, 2f, 3f, 4f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(6f, element.style.paddingLeft.value.value, 0.001f);
            Assert.AreEqual(12f, element.style.paddingTop.value.value, 0.001f);
            Assert.AreEqual(18f, element.style.paddingRight.value.value, 0.001f);
            Assert.AreEqual(24f, element.style.paddingBottom.value.value, 0.001f);
        }

        [Test]
        public void ToTranslate_AppliesCorrectStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            Tweener<Vector2> tween = element.ToTranslate(new Vector2(10f, 20f), Vector2.zero);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(10f, element.style.translate.value.x.value, 0.001f);
            Assert.AreEqual(20f, element.style.translate.value.y.value, 0.001f);
        }

        [Test]
        public void ByTranslate_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.translate = new Translate(5f, 10f);
            Tweener<Vector2> tween = element.ByTranslate(new Vector2(15f, 25f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(20f, element.style.translate.value.x.value, 0.001f);
            Assert.AreEqual(35f, element.style.translate.value.y.value, 0.001f);
        }

        [Test]
        public void ToWidth_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToWidth(200f, 100f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(200f, element.style.width.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByWidth_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.width = 100f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByWidth(50f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(150f, element.style.width.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToHeight_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToHeight(200f, 100f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(200f, element.style.height.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByHeight_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.height = 100f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByHeight(50f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(150f, element.style.height.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToFlexGrow_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToFlexGrow(2f, 0f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(2f, element.style.flexGrow.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByFlexGrow_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.flexGrow = 1f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByFlexGrow(0.5f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.flexGrow.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToFlexShrink_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ToFlexShrink(2f, 1f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(2f, element.style.flexShrink.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByFlexShrink_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.flexShrink = 1f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<float> tween = element.ByFlexShrink(0.5f, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(1.5f, element.style.flexShrink.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToMargin_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector4> tween = element.ToMargin(new Vector4(10f, 20f, 30f, 40f), Vector4.zero, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(10f, element.style.marginLeft.value.value, 0.001f);
            Assert.AreEqual(20f, element.style.marginTop.value.value, 0.001f);
            Assert.AreEqual(30f, element.style.marginRight.value.value, 0.001f);
            Assert.AreEqual(40f, element.style.marginBottom.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByMargin_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.marginLeft = 5f;
            element.style.marginTop = 10f;
            element.style.marginRight = 15f;
            element.style.marginBottom = 20f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector4> tween = element.ByMargin(new Vector4(1f, 2f, 3f, 4f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(6f, element.style.marginLeft.value.value, 0.001f);
            Assert.AreEqual(12f, element.style.marginTop.value.value, 0.001f);
            Assert.AreEqual(18f, element.style.marginRight.value.value, 0.001f);
            Assert.AreEqual(24f, element.style.marginBottom.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToPadding_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector4> tween = element.ToPadding(new Vector4(10f, 20f, 30f, 40f), Vector4.zero, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(10f, element.style.paddingLeft.value.value, 0.001f);
            Assert.AreEqual(20f, element.style.paddingTop.value.value, 0.001f);
            Assert.AreEqual(30f, element.style.paddingRight.value.value, 0.001f);
            Assert.AreEqual(40f, element.style.paddingBottom.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByPadding_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.paddingLeft = 5f;
            element.style.paddingTop = 10f;
            element.style.paddingRight = 15f;
            element.style.paddingBottom = 20f;
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector4> tween = element.ByPadding(new Vector4(1f, 2f, 3f, 4f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(6f, element.style.paddingLeft.value.value, 0.001f);
            Assert.AreEqual(12f, element.style.paddingTop.value.value, 0.001f);
            Assert.AreEqual(18f, element.style.paddingRight.value.value, 0.001f);
            Assert.AreEqual(24f, element.style.paddingBottom.value.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ToTranslate_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ToTranslate(new Vector2(10f, 20f), Vector2.zero, engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(10f, element.style.translate.value.x.value, 0.001f);
            Assert.AreEqual(20f, element.style.translate.value.y.value, 0.001f);
            engine.Dispose();
        }

        [Test]
        public void ByTranslate_ExplicitEngine_RegistersWithProvidedEngine()
        {
            // Arrange
            VisualElement element = new VisualElement();
            element.style.translate = new Translate(5f, 10f);
            ManualTweenTicker ticker = new ManualTweenTicker();
            TweenEngine engine = new TweenEngine(ticker);

            // Act
            Tweener<Vector2> tween = element.ByTranslate(new Vector2(15f, 25f), engine);
            tween.SetDuration(1f);
            ticker.TickManual(1f);

            // Assert
            Assert.AreEqual(20f, element.style.translate.value.x.value, 0.001f);
            Assert.AreEqual(35f, element.style.translate.value.y.value, 0.001f);
            engine.Dispose();
        }
    }
}
