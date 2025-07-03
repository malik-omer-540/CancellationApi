# CancellationApi (.NET 8 Web API - Using Cancellation Tokens)

This API demonstrates proper and improper usage of **cancellation tokens** in long-running operations in .NET 8.

## Why Use Cancellation Tokens?

- ✅ Save server resources when users cancel requests
- ✅ Prevent long-running background tasks from piling up
- ✅ Gracefully shut down hosted services and background tasks
- ✅ Improve performance and responsiveness under load

## API Endpoints

| Endpoint                                 | Description                                 |
|------------------------------------------|---------------------------------------------|
| `GET /api/task/long-task`                | ✅ Supports cancellation (recommended)      |
| `GET /api/task/long-task-uncancellable`  | ❌ Ignores cancellation (wastes resources)  |

## How It Works

- ASP.NET Core injects `CancellationToken` into controller actions.
- Services and DB queries receive the token and honor it using:

```csharp
await Task.Delay(1000, cancellationToken);
```

or:

```csharp
var users = await _context.Users.ToListAsync(cancellationToken);
```

- Cancellations (like closing the tab or navigating away) will trigger `OperationCanceledException` and stop processing.

## How to Run

```bash
dotnet restore
dotnet build
dotnet run
```

Access via Swagger UI:
`https://localhost:5001/swagger`

## Folder Structure

```plaintext
CancellationApi/
│
├── CancellationApi.sln
├── README.md
│
├── CancellationApi/
│   ├── Controllers/
│   │   └── TaskController.cs
│   ├── Services/
│   │   ├── ILongTaskService.cs
│   │   └── LongTaskService.cs
│   ├── Program.cs
│   └── CancellationApi.csproj
```

## Bonus: Graceful Cancellation in Hosted Services

```csharp
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await DoWorkAsync(stoppingToken);
        await Task.Delay(1000, stoppingToken);
    }
}
```

---

> Use cancellation tokens wherever you do async work. Small cost, big gains in performance and resilience.
