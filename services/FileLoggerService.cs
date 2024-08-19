using System.Text.Json;

public class FileLoggerService : ICustomLoggerService
{
    private readonly string _filePath;

    public FileLoggerService(string filePath)
    {
        _filePath = filePath;
    }

    public async Task StoreLogAsync(LogEntry logEntry)
    {
        var timestamp = DateTime.UtcNow;
        logEntry.Timestamp = timestamp;

        var logEntryJson = JsonSerializer.Serialize(logEntry);
        await File.AppendAllTextAsync(_filePath, logEntryJson + Environment.NewLine);
    }
}



