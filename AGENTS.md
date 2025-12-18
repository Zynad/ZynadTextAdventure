# Engineering Rules for TextAdventure

This repository follows a strict layered architecture. Read this file **before** changing any code. Keep it aligned with the solution layout described in [README.md](README.md) and any contributor documentation.

## Layer boundaries
- **TextAdventure.Api**: HTTP hosting and controllers only. No business logic, persistence details, or domain mutations. Controllers delegate to application services.
- **ApplicationServices**: Orchestration layer that composes domain contracts and emits DTOs. No infrastructure concerns (files, JSON, DB), and no direct knowledge of API/web concerns beyond validating input/output.
- **Domain**: Pure models and contracts (entities, value objects, interfaces such as `IGameDatabase`). No external dependencies, serialization, or infrastructure logic.
- **TextAdventure.Infrastructure**: Implementations of domain contracts (e.g., repositories, persistence adapters). No controller logic or application orchestration.
- Allowed dependency directions: `TextAdventure.Api -> ApplicationServices -> Domain <- TextAdventure.Infrastructure`. Cross-layer shortcuts (e.g., API -> Domain or ApplicationServices -> Infrastructure types) are prohibited. Domain remains dependency-free on higher layers.

## DTOs, domain models, and mapping
- Keep DTOs out of the Domain layer. Domain types stay persistence-agnostic; API/application DTOs belong in their respective layers.
- Use Mapperly for object projections. Mapping configurations live with the consumer layer (e.g., API mappers inside `TextAdventure.Api`, application mappers inside `ApplicationServices`). Avoid manual, ad-hoc mapping in controllers or services.
- When mapping, prefer narrow DTOs; do not expose domain entities directly through API responses.
- Keep mapping code DRY: define reusable mapping methods or partials rather than duplicating per-endpoint conversions.

## File-per-type policy
- Each public type (class, interface, record, enum) lives in its own file named after the type. Nested types should be rare and justified.
- Avoid “misc” utility files containing multiple unrelated types.

## DRY and composition
- If logic is used in more than one place, extract a shared method/service in the appropriate layer. Do not duplicate validation or mapping rules across controllers or services.

## Testing expectations
- Add or update unit tests for any behavioral change. Tests live alongside the layer they exercise (API tests in `TextAdventureTests`, application/domain tests in their respective test projects).
- Run the full test suite (`dotnet test`) before submitting changes, and ensure new rules remain consistent with the architecture above.

## How to use this guidance
- Start by reviewing the project overview in [README.md](README.md) to confirm responsibilities per layer.
- If contributor docs are added, keep this file aligned and reference them when updating rules so humans and AI agents encounter the latest expectations.
