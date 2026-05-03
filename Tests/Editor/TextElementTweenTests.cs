using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit.Tests
{
    public class TextElementTweenTests
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
        public void ToTextColor_AppliesCorrectStyleValue()
        {
            // Arrange
            TextElement element = new TextElement();
            Tweener<Color> tween = element.ToTextColor(Color.red, Color.white);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(Color.red, element.style.color.value);
        }

        [Test]
        public void ToTextColor_ExplicitFrom_IsRespected()
        {
            // Arrange
            TextElement element = new TextElement();
            Tweener<Color> tween = element.ToTextColor(Color.red, Color.blue);
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert
            Assert.AreEqual(new Color(0.5f, 0f, 0.5f, 1f), element.style.color.value);
        }

        [Test]
        public void ByTextColor_AppliesCorrectRelativeStyleValue()
        {
            // Arrange
            TextElement element = new TextElement();
            element.style.color = Color.white;
            Tweener<Color> tween = element.ByTextColor(new Color(0.2f, 0f, 0f, 0f));
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual(new Color(1.2f, 1f, 1f, 1f), element.style.color.value);
        }
    }
}
