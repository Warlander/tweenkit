using System;

namespace Warlogic.Tweenkit
{
    /// <summary>
    /// Global entry point for the Tweenkit fluent API.
    /// </summary>
    /// <remarks>
    /// The static mutable engine field is intentional API design, not a bug or temporary workaround.
    /// Tweenkit is designed as a zero-configuration, globally accessible tweening library
    /// (similar to DOTween). A single shared engine keeps call sites lightweight and avoids
    /// passing engine references through every UI screen and presenter.
    /// </remarks>
    public static class Tweenkit
    {
        // This is for internal use by the fluent API only.
        // Never use it directly outside of the Tweenkit package.
        internal static ITweenEngine Engine { get; private set; }

        public static void Initialize(ITweenEngine engine = null)
        {
            if (Engine != null && !Engine.IsDisposed)
            {
                return;
            }

            if (engine != null)
            {
                Engine = engine;
            }
            else
            {
                DefaultTweenEngineFactory factory = new DefaultTweenEngineFactory();
                Engine = factory.Create();
            }
        }

        public static void Shutdown()
        {
            Engine?.Dispose();
            Engine = null;
        }

        public static void KillAll()
        {
            Engine?.KillAll();
        }

        public static int ActiveTweenCount
        {
            get { return Engine?.ActiveTweenCount ?? 0; }
        }

        public static Sequence Sequence(ITweenEngine engine = null)
        {
            Sequence sequence = new Sequence();
            ITweenEngine targetEngine = engine ?? Engine;
            if (targetEngine == null)
            {
                throw new InvalidOperationException("Tweenkit not initialized.");
            }

            targetEngine.Register(sequence);
            return sequence;
        }
    }
}
