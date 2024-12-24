namespace Net.Gwiasda.Yala
{
    public class LogEntryManager : ILogEntryManager
    {
        private readonly ILogEntryRepository _repository;

        public LogEntryManager(ILogEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task WriteLogEntryAsync(LogEntry entry)
            => await _repository.WriteLogEntryAsync(entry);
    }
}
