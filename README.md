# Unity Clean Architecture

A small Unity 6 sample that demonstrates **ports & adapters** for a game client: views talk to controllers, controllers talk to ports, and infrastructure provides adapters.

Built as a portfolio rewrite of patterns I used in production education clients — without product branding, secrets, or proprietary assets.

## Architecture

```text
Presentation  →  Application (Controllers + Composition Root)
                      ↓
                   Domain (Models + Ports)
                      ↓
               Infrastructure (Adapters + HTTP)
```

| Layer | Responsibility |
| --- | --- |
| `Domain` | Models + `ICourseAdapter` port |
| `Application` | `CourseController` use-cases + composition root |
| `Infrastructure` | `FakeCourseAdapter` / `HttpCourseAdapter` |
| `Presentation` | `CourseListView` MonoBehaviour |

## Why this shape

- Swap backends without rewriting UI (fake → HTTP).
- Keep Unity lifecycle (`MonoBehaviour`) at the edges.
- Make use-cases unit-testable without a scene.

## Requirements

- Unity **6000.3.9f1** (Unity 6)
- No third-party packages required for the demo

## How to run

1. Open the folder in Unity Hub.
2. Open `Assets/Scenes/Demo.unity`.
3. Add a Canvas + UI `Text`, attach `CourseListView`, wire the `CompositionRoot`.
4. Press Play — the fake adapter returns three sample courses.

Toggle `useHttpAdapter` on `CompositionRoot` when you have a real API.

## Author

[Pedro Paulo](https://github.com/PedroPPMN) — MIT License
