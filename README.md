# Tweenkit

Lightweight tweening library for Unity UI Toolkit. Provides a fluent, zero-configuration API for animating properties such as position, size, opacity, rotation, scale, colors, and more. Supports sequences, loops, delays, custom easing, and manual tick control for more flexibility in how you want to use it and testability.

# Installation

## Via Git URL

Open **Window > Package Manager**, click **+**, and choose **Add package from git URL**.

To install the latest version:
```
https://github.com/Warlander/tweenkit.git
```

To install a specific release, append the tag:
```
https://github.com/Warlander/tweenkit.git#1.0.0
```

## Via Registry Browser

If you have [Registry Browser](https://github.com/Warlander/registry-browser) in the project, make sure you have tracked registry added:

```
Scope Prefix: com.warlogic
Registry URL: https://upm.maciejcyranowicz.com
```

Then open **Window > Warlander > Registry Browser** and add `com.warlogic.tweenkit` to the project.

## Via Scoped Registry

Add the Warlogic registry to your `Packages/manifest.json`:

```json
{
  "scopedRegistries": [
    {
      "name": "Warlogic",
      "url": "https://upm.maciejcyranowicz.com",
      "scopes": ["com.warlogic"]
    }
  ],
  "dependencies": {
    "com.warlogic.tweenkit": "1.0.0"
  }
}
```

Then open **Window > Package Manager** and look for `com.warlogic.tweenkit`.

# Setup

Call `TK.Initialize()` once at startup. This creates a hidden `GameObject` with a ticker that drives the tween engine on `Update`.

```csharp
TK.Initialize();
```

Call `TK.Shutdown()` when cleaning up to dispose the engine and destroy the ticker. This is only necessary if you no longer wish to use Tweenkit within the same game session and/or plan to restart it later.

```csharp
TK.Shutdown();
```

You can also provide your own `ITweenEngine` implementation or use default `TweenEngine` outside of `TK` utility and fluent API if you prefer custom tick control.

# Usage

## Basic Tweens

Animate any `VisualElement` with fluent extension methods. Every tween returns a `Tweener<T>` that can be further configured.

```csharp
VisualElement element = ...;

element.ToOpacity(0f).SetDuration(0.5f);
element.ToScale(1.5f).SetDuration(0.3f).SetEase(Ease.OutBack);
element.ToPosition(new Vector2(100f, 200f)).SetDuration(1f);
```

## Relative Tweens

Use `By*` methods to animate relative to the current value:

```csharp
element.ByPosition(new Vector2(50f, 0f)).SetDuration(0.5f);
element.ByRotation(90f).SetDuration(0.3f);
```

## Available Extensions

### VisualElement
- `ToOpacity` / `ByOpacity`
- `ToScale` / `ByScale` (`float` or `Vector2`)
- `ToPosition` / `ByPosition`
- `ToRotation` / `ByRotation`
- `ToSize` / `BySize`
- `ToWidth` / `ByWidth`
- `ToHeight` / `ByHeight`
- `ToFlexGrow` / `ByFlexGrow`
- `ToFlexShrink` / `ByFlexShrink`
- `ToMargin` / `ByMargin`
- `ToPadding` / `ByPadding`
- `ToTranslate` / `ByTranslate`
- `ToBackgroundColor` / `ByBackgroundColor`

### TextElement
- `ToTextColor` / `ByTextColor`

### Label
- `ToTypewriter` — reveals text character-by-character

## Configuration

Chain configuration:

```csharp
element.ToOpacity(0f)
    .SetDuration(1f)
    .SetDelay(0.5f)
    .SetEase(Ease.InOutQuad)
    .SetLoops(3, LoopType.Yoyo);
```

### Easing

Built-in easing functions cover the standard set. By default, ease used is `Ease.Linear`:

```csharp
element.ToScale(2f).SetEase(Ease.OutElastic);
```

You can also provide custom easing delegates:

```csharp
element.ToOpacity(0f).SetEase(t => t * t);
```

Or combine separate in/out eases:

```csharp
element.ToOpacity(0f).SetEase(Ease.InQuad, Ease.OutCubic);
```

### Loops

```csharp
.SetLoops(5, LoopType.Restart)  // Play 5 times from start
.SetLoops(-1, LoopType.Yoyo)    // Loop forever, reversing each time
```

## Callbacks

```csharp
Tweener<float> tween = element.ToOpacity(0f).SetDuration(1f);
tween.OnComplete += () => Debug.Log("Done!");
tween.OnUpdate += () => Debug.Log("Tween updated");
```

## Sequences

Chain tweens and callbacks in time with `Sequence`:

```csharp
Sequence seq = TK.Sequence();
seq.Append(element.ToOpacity(0f).SetDuration(0.5f))
   .AppendInterval(0.2f)
   .Append(element.ToOpacity(1f).SetDuration(0.5f))
   .AppendCallback(() => Debug.Log("Sequence finished"))
   .Play();
```

- `Append` — adds a tween or callback after the previous item ends.
- `Join` — adds a tween that starts at the same time as the previous item.
- `Insert` — places a tween or callback at an explicit time offset.

## Control

```csharp
tween.Pause();
tween.Play();   // resumes from pause
tween.Kill();   // stops and marks for removal
tween.Complete(); // jumps to end, fires OnComplete and kills the tween
```

Kill all active tweens:

```csharp
TK.KillAll();
```

## Manual Tick

For unit tests or custom update loops, use `ManualTweenTicker`:

```csharp
ManualTweenTicker ticker = new ManualTweenTicker();
TweenEngine engine = new TweenEngine(ticker);
TK.Initialize(engine); // Optional - only if you plan to use fluent API

ticker.TickManual(0.5f); // advance time by 0.5 seconds
```

You can also tick tweens and sequences directly without using engine at all.

# License

MIT License — see [LICENSE](LICENSE) for details.
