namespace FlowerShowerService.Infrastructure;

using FlowerShowerService.Data;
using FlowerShowerService.Data.Entities;

public class LoggingService : ILoggingService
{
    private readonly DataContext _db;

    public LoggingService(DataContext db)
    {
        _db = db;
    }

    public async Task Log(LogEntry logEntry)
    {
        _db.LogEntries.Add(logEntry);
        await _db.SaveChangesAsync();
    }
}
