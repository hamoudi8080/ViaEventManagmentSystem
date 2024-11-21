# Domain-Driven Design (DDD)

Domain-Driven Design is a backend-focused approach, leveraging concepts and implementations from Clean Architecture and Hexagonal Architecture. The goal is to build a clean, long-term sustainable core for the application.

## Architectures and Software Design

- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **Hexagonal Architecture**
- **Vertical Slice Architecture**

## Tools and Patterns
- **SOLID Design Principles**
- **ResultPatterns**
- **Command Query Responsibility Segregation (CQRS)**

## Testing Practices

- **Test-Driven Development (TDD)**
- **Unit Tests**
- **Integration Tests**



# Domain-Driven Design (DDD)

## Problems of Anemic Domain Model
An Anemic Domain Model is essentially a model where database tables are mapped to objects that contain only field values with minimal to no behavior. These objects act as data containers, providing just basic getters and setters for manipulating values, rather than encapsulating business logic. This approach leads to two primary issues:

### Lack of Business Logic in Domain Model
Since the model is merely a representation of the database with limited or no logic, the actual business logic ends up being spread across different layers. This lack of encapsulation can lead to difficulties in maintaining and scaling the application, as there is no clear central location for core business rules.

### DAO Bloat
Because there’s minimal logic in the domain model, the Data Access Object (DAO) layer grows large, with a focus on database operations. For example, instead of keeping business rules in the domain, you end up adding highly specific methods to DAOs, which are essentially database-focused and don't represent true business concepts. This approach causes DAO interfaces to grow large and cluttered with highly specific methods as different parts of the application require distinct database queries.

### Conclusion
An Anemic Domain Model = database tables mapped to objects (only field values, no real behavior). They are just data-containers with a bunch of setters and getters when manipulating the model. Another problem is that it is database-focused, leading to DAO bloat as the DAO grows large when you want to get/add data to the database.

## Rich Domain Model
A Rich Domain Model is a collection of objects that expose behavior.

### Domain-Driven Design (DDD)
Domain-driven design (DDD) is an approach to software development for complex needs by connecting the implementation to an evolving model. The premise of domain-driven design is the following:
1. Placing the project's primary focus on the core domain and domain logic.
2. Basing complex designs on a model of the domain.
3. Initiating a creative collaboration between technical and domain experts to iteratively refine a conceptual model that addresses particular domain problems.

# Architectural Patterns

## The project uses the these architectural patterns

### Clean Architecture
- **Concept**: Structuring software to make it easy to understand and extend.
- **Layers**: Entities, Use Cases, Interface Adapters, Frameworks & Drivers.
- **Focus**: Dependency rule, where outer layers depend on inner layers, facilitating independence between business logic and external concerns.

### Hexagonal Architecture (Ports and Adapters)
- **Concept**: Designing software to be flexible and adaptable to changing technologies.
- **Components**: Core domain logic (inside hexagon), Ports (interfaces), and Adapters (implementations).
- **Focus**: Isolate the core domain logic from external systems (e.g., UI, databases).

### Vertical Slice Architecture
- **Concept**: Structuring software by features rather than layers.
- **Structure**: Each vertical slice represents a feature and contains all the necessary layers (e.g., UI, API, business logic, data access).
- **Focus**: Simplifies dependencies and promotes cohesive feature development.

## Differences

### Dependency Management
- **Clean Architecture** emphasizes directional dependencies where outer layers depend on the inner.
- **Hexagonal Architecture** uses ports and adapters to decouple the core logic from external systems.
- **Vertical Slice Architecture** minimizes cross-cutting concerns by encapsulating features end-to-end.

### Modularity
- **Clean Architecture** and **Hexagonal Architecture** focus on modularity through layered and decoupled structures.
- **Vertical Slice Architecture** focuses on modularity through feature-driven slices.

### Usage
- **Clean Architecture** is often chosen for complex applications that need robust separation of concerns.
- **Hexagonal Architecture** suits scenarios where adaptability to changing external systems is critical.
- **Vertical Slice Architecture** is preferred for simplifying development and maintenance by focusing on individual features.
