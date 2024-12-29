namespace Net.Gwiasda.Yala
{
    public interface ILogEntryManager
    {
        Task<List<string>> GetApplicationNamesAsync();
        Task<List<LogEntry>> GetLogEntriesFromAppAsync(string appName, int startIndex, int count);
        Task WriteLogEntryAsync(LogEntry entry);
    }
}
