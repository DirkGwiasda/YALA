namespace Net.Gwiasda.Yala
{
    public interface ILogEntryRepository
    {
        Task<List<string>> ReadApplicationNamesAsync();
        Task WriteLogEntryAsync(LogEntry entry);
    }
}
