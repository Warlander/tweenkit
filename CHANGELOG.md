# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.2] - 2026-05-05

### Fixed
- Removed orphaned `Tests/Runtime.meta` file that caused Unity to recreate an empty directory on every import.

## [1.0.1] - 2026-05-05

### Fixed
- `ToTypewriter` now clamps eased progress to `[0, 1]`, preventing crashes when using easings that overshoot (e.g. `InBack`, `OutBounce`).
- Renamed static entry-point class from `Tweenkit` to `TK` to resolve namespace collision with `Warlogic.Tweenkit`.

## [1.0.0] - 2026-05-05

### This is the first release of *Tweenkit*.
