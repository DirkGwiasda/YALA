namespace Net.Gwiasda.Yala
{
    public interface ILogEntryManager
    {
        Task WriteLogEntryAsync(LogEntry entry);
    }
}
