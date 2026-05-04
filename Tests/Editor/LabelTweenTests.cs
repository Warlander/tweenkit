using NUnit.Framework;
using UnityEngine.UIElements;

namespace Warlogic.Tweenkit.Tests
{
    public class LabelTweenTests
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
        public void ToTypewriter_ShowsCorrectSubstringAtProgress()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("Hello");
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(0.3f);

            // Assert
            Assert.AreEqual("H", label.text);
        }

        [Test]
        public void ToTypewriter_ShowsFullTextAtCompletion()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("Hello");
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual("Hello", label.text);
        }

        [Test]
        public void ToTypewriter_EmptyString_ShowsEmpty()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("");
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert
            Assert.AreEqual("", label.text);
        }

        [Test]
        public void ToTypewriter_AtStart_ShowsEmpty()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("Hello");
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(0f);

            // Assert
            Assert.AreEqual("", label.text);
        }

        [Test]
        public void ToTypewriter_HalfProgress_ShowsCorrectSubstring()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("Hello");
            tween.SetDuration(1f);

            // Act
            tween.Play();
            tween.Tick(0.5f);

            // Assert - 0.5 * 5 = 2.5, floor = 2, "He"
            Assert.AreEqual("He", label.text);
        }

        [Test]
        public void ToTypewriter_WithBackEase_DoesNotCrash()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("Hello");
            tween.SetDuration(1f);
            tween.SetEase(Ease.InBack);

            // Act
            tween.Play();
            tween.Tick(0.1f);

            // Assert - clamped to empty, not crashed
            Assert.AreEqual("", label.text);
        }

        [Test]
        public void ToTypewriter_WithBounceEase_DoesNotOvershoot()
        {
            // Arrange
            Label label = new Label();
            Tweener<string> tween = label.ToTypewriter("Hello");
            tween.SetDuration(1f);
            tween.SetEase(Ease.OutBounce);

            // Act
            tween.Play();
            tween.Tick(1f);

            // Assert - clamped to full text, not overshot
            Assert.AreEqual("Hello", label.text);
        }
    }
}
