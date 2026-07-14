# RDT Insurance Challenge

This repository contains an intentionally incomplete insurance domain. The goal is to turn it into a small, well-reasoned solution that behaves correctly and can be verified clearly.

---

## Prerequisites

You will need the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) installed.

Verify your installation:

```pwsh
dotnet --version
```

The output should start with `10.`.

You can work from the command line, or open `Rdt.Insurance.App.slnx` in one of:

- **Visual Studio 2022** (17.10 or later)
- **VS Code** with the [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension
- **JetBrains Rider**

---

## Getting started

All commands in this README need to be run in a terminal, from inside the `apprentice/challenge/` folder. Here is how to open one in each IDE:

- **Visual Studio 2022** — go to **View → Terminal**. A terminal panel will open at the bottom. Type the following and press Enter to navigate to the right folder:
  ```pwsh
  cd apprentice/challenge
  ```
- **VS Code** — go to **Terminal → New Terminal**. A terminal panel will open at the bottom. Type the following and press Enter:
  ```pwsh
  cd apprentice/challenge
  ```
- **JetBrains Rider** — go to **View → Tool Windows → Terminal**. Type the following and press Enter:
  ```pwsh
  cd apprentice/challenge
  ```

Once you are in the right folder, you can run the commands below. To confirm you are in the correct place, your terminal prompt should end with `apprentice/challenge` or `apprentice\challenge`.

Build the project to confirm everything compiles:

```pwsh
dotnet build
```

---

## What is here

| Path | Description |
|------|-------------|
| `Rdt.Insurance.App/Models/PolicyHolder.cs` | Represents the person who holds a policy |
| `Rdt.Insurance.App/Models/Policy.cs` | A policy with dates, a premium, and a vehicle registration number |
| `Rdt.Insurance.App/Models/Claim.cs` | A claim made against a policy |
| `Rdt.Insurance.App/Handlers/PolicyHandler.cs` | Current policy creation flow |
| `Rdt.Insurance.App/Handlers/ClaimHandler.cs` | Incomplete claim creation flow |
| `Rdt.Insurance.App/Repositories/*.cs` | Repository contracts and in-memory implementations for policies and claims |
| `Rdt.Insurance.App/Program.cs` | Console application entry point — policy creation is wired up; claim creation is not |

---

## Running the application

Start the console application with:

```pwsh
dotnet run --project Rdt.Insurance.App
```

The menu has two options. **Create Policy** is fully wired up. **Create Claim** is not — completing it is part of the challenge.

---

## How this code is structured

The codebase is split into three layers, each with a specific responsibility:

- **Models** (`Models/`) — plain data classes that represent the core concepts: a `PolicyHolder`, a `Policy`, and a `Claim`. They hold data but contain no logic.
- **Repositories** (`Repositories/`) — responsible for storing and retrieving data. Each repository comes in two parts: an interface (a file starting with `I`, e.g. `IClaimRepository`) that lists *what* the repository must be able to do, and the class that actually does it (e.g. `InMemoryClaimRepository`) — in this case, using an in-memory list. `InMemoryPolicyRepository` is already fully implemented and is a good example to follow.
- **Handlers** (`Handlers/`) — responsible for the business logic. A handler takes inputs, validates them, builds a model, and passes it to a repository to be saved. `PolicyHandler` is fully implemented and shows exactly the pattern you should follow when completing `ClaimHandler`.

If you are unsure how a piece should work, look at the equivalent piece for policies — the structure for claims should mirror it closely.

---

## What you need to complete

There are a few gaps to fill — completing them all is the task:

1. **`InMemoryClaimRepository`** — the `IClaimRepository` interface (the file that lists what a claim repository must do) already declares a `Save` method. Add the implementation in `InMemoryClaimRepository` — the body of the method should add the claim to the list.
2. **`ClaimHandler.CreateClaim`** — the method exists but currently throws `NotImplementedException`, which is a placeholder that will crash the application. Replace it with the real logic.
3. **`Program.cs CreateClaim`** — same situation: the method is a placeholder that crashes. Complete it so it collects input from the user and calls the handler. The `CreatePolicy` method just above it shows exactly the pattern to follow.

To help you navigate, here is a quick guide to which files to read as examples and which ones to change:

| File | What to do |
|------|------------|
| `Handlers/PolicyHandler.cs` | **Read** — shows the pattern your `ClaimHandler` should follow |
| `Repositories/InMemoryPolicyRepository.cs` | **Read** — shows what your repository implementation should look like |
| `Models/Claim.cs` | **Read** — lists the properties you need to fill in when creating a claim |
| `Repositories/InMemoryClaimRepository.cs` | **Change** — Step 1 |
| `Handlers/ClaimHandler.cs` | **Change** — Step 2 |
| `Program.cs` | **Change** — Step 3 (see `CreatePolicy` just above the stub) |

---

## Business rules for claims

When implementing `ClaimHandler.CreateClaim`, your code should enforce:

- The policy referenced by `policyId` must exist.
- The `dateOfIncident` must fall within the policy's start and end dates.
- `description` must not be empty or whitespace.
- `amountClaiming` must be greater than zero.
- `DateFiled` should be set to today's date when the claim is created.
- Claims above £1,000 should be flagged as high risk (`IsHighRisk = true`).

---

## The challenge

Complete the domain so that policies and claims can be created and validated safely.

The current code is only a starting point. You may refactor the implementation, but keep `PolicyHolder`, `Policy`, and `Claim` as the recognizable core domain entities so submissions stay comparable. You can make justified model or contract changes where that improves correctness or clarity — explain them in `COMPLETION.MD`.

You may:

- refactor the current implementation
- add collaborators or helper types
- change handler constructors or interfaces
- add domain logic where you think it belongs
- make justified model or contract changes if needed, and explain why in `COMPLETION.MD`

Keep the scope focused on the domain itself. No UI, API, database, or external services are needed.

Expected effort is around **2 hours**. If time is tight, prioritise a working claim flow with clear validation first; any extra depth is optional.

---

## When you are done

Fill in `COMPLETION.MD` (in this folder). Explain:

- how you approached the task
- what decisions you made and why
- how you verified the solution works
- any trade-offs or follow-up improvements you would make

If you used any AI tooling, log it honestly in the AI usage table in `COMPLETION.MD`.

---
