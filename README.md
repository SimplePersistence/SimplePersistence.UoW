# SimplePersistence.UoW
Interfaces and common implementations of the Unit of Work pattern

## Installation
This library can be installed via NuGet package. Just run the following command:

```powershell
Install-Package SimplePersistence.UoW
```

## Usage

```csharp

public interface IApplicationRepository : IRepository<Application, string>
{
  
}

public interface ILevelRepository : IRepository<Level, string>
{
  
}

public interface ILogRepository : IRepository<Log, long>
{
  Task<IEnumerable<Log>> GetAllCreatedAfterAsync(DateTimeOffset on, CancellationToken ct);
}

public interface ILoggingWorkArea : IWorkArea
{
    IApplicationRepository Applications { get; }
    ILevelRepository Levels { get; }
    ILogRepository Logs { get; }
}

public interface IAppUnitOfWork : IUnitOfWork
{
    ILoggingWorkArea Logging { get; }
}

```
