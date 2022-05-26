using FlowerShowerService.Data.Entities;

namespace FlowerShowerService.Infrastructure;

public interface ILoggingService
{
    Task Log(LogEntry logEntry);
}
