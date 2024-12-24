namespace Net.Gwiasda.Yala
{
    public interface ILogEntryRepository
    {
        Task ForceRepositoryExists();
        Task WriteLogEntryAsync(LogEntry entry);
    }
}
