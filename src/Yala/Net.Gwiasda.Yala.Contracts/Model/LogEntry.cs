namespace Net.Gwiasda.Yala
{
    public class LogEntry
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? AppName { get; set; }
        public string? SourceName { get; set; }
        public string? Message { get; set; }
        public LogType LogType { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Dictionary<string, string> AdditionalData { get; } = new Dictionary<string, string>();
        public List<string> Tags { get; } = new List<string>();
    }
}
