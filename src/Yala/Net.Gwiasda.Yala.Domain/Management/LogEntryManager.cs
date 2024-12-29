
namespace Net.Gwiasda.Yala
{
    public class LogEntryManager : ILogEntryManager
    {
        private readonly ILogEntryRepository _repository;

        public LogEntryManager(ILogEntryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<string>> GetApplicationNamesAsync()
            => await _repository.ReadApplicationNamesAsync();

        public async Task<List<LogEntry>> GetLogEntriesFromAppAsync(string appName, int startIndex, int count)
            => await _repository.ReadLogEntriesFromAppAsync(appName, startIndex, count);

        public async Task WriteLogEntryAsync(LogEntry entry)
            => await _repository.WriteLogEntryAsync(entry);
    }
}
