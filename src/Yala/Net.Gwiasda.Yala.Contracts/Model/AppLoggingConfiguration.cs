namespace Net.Gwiasda.Yala
{
    public class AppLoggingConfiguration
    {
        public string? AppName { get; set; }
        public LogType LogType { get; set; } = LogType.Information;
        public Dictionary<string, LogType> SourceConfigurations { get; } = new Dictionary<string, LogType>();
    }
}
