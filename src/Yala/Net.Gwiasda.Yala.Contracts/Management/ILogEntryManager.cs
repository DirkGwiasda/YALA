namespace Net.Gwiasda.Yala
{
    public interface ILogEntryManager
    {
        Task<List<string>> GetApplicationNamesAsync();
        Task WriteLogEntryAsync(LogEntry entry);
    }
}
