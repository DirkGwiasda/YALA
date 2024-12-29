namespace Net.Gwiasda.Yala
{
    public interface ILogEntryRepository
    {
        Task<List<string>> ReadApplicationNamesAsync();
        Task<List<LogEntry>> ReadLogEntriesFromAppAsync(string appName, int startIndex, int count);
        Task WriteLogEntryAsync(LogEntry entry);
    }
}
