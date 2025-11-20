# Xunit and TDD

**Xunit** is a free, open-source, and community-focused unit testing framework for the .NET framework. It was developed by the original inventor of NUnit, aiming to address various shortcomings of NUnit and other unit testing frameworks. Xunit is widely used in the .NET ecosystem for writing unit tests because of its simplicity, extensibility, and flexibility.

---

We use **TDD (Test-Driven Development)** in this project.

## What is TDD?

Test-Driven Development (TDD) is a software development approach where tests are written before the actual code. It follows a cyclic process of writing a test, writing code to make the test pass, and then refactoring the code. This approach helps ensure code quality, maintainability, and robustness throughout the development lifecycle.

### Key Principles of TDD

1. **Write a Test First**:
    - **Red Phase**: Begin by writing a failing test for the next small piece of functionality. Writing tests first helps clarify the requirements and sets a clear goal for the development.

2. **Make the Test Pass**:
    - **Green Phase**: Write the minimum amount of code necessary to make the test pass. This step focuses purely on implementation to meet the test requirements without worrying about code quality just yet.

3. **Refactor**:
    - **Refactor Phase**: Once the test passes, refactor the code to improve its structure and design while ensuring that all tests still pass. This step is crucial for maintaining code quality and performance.

---

### Note

#### Testing and the Domain Model

- In a perfect world, we cannot retrieve data from the Domain Model. All is private, encapsulated.
- How do we then verify the behavior? A lot of the behavior has to do with state-changes.
- We make all properties internal, and let the test assembly access them.
- The properties are now also accessible to the rest of the Domain assembly, but not to e.g. Application assembly, so we accept this minor flaw.