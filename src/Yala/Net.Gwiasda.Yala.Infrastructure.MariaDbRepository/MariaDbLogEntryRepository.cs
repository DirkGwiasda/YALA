using MySqlConnector;

namespace Net.Gwiasda.Yala.Infrastructure
{
    public sealed class MariaDbLogEntryRepository : MariaDbRepository, ILogEntryRepository
    {
        public MariaDbLogEntryRepository(string connectionString) : base(connectionString) { }

        public Task ForceRepositoryExists()
        {
            throw new NotImplementedException();
        }

        public async Task WriteLogEntryAsync(LogEntry entry)
        {
            if(entry == null) throw new ArgumentNullException(nameof(entry));

            const string sql = @"
                INSERT INTO LogEntry (id, appName, sourceName, message, logType, timestamp) 
                VALUES (@id, @appName, @sourceName, @message, @logType, @timestamp);";


            using var connection = await base.GetConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.Add(new MySqlParameter("@id", MySqlDbType.VarChar) { Value = entry.Id });
            command.Parameters.Add(new MySqlParameter("@appName", MySqlDbType.VarChar) { Value = entry.AppName });
            command.Parameters.Add(new MySqlParameter("@sourceName", MySqlDbType.VarChar)
            {
                Value = entry.SourceName ?? (object)DBNull.Value
            });
            command.Parameters.Add(new MySqlParameter("@message", MySqlDbType.VarChar) { Value = entry.Message });
            command.Parameters.Add(new MySqlParameter("@logType", MySqlDbType.Int32) { Value = entry.LogType });
            command.Parameters.Add(new MySqlParameter("@timestamp", MySqlDbType.DateTime) { Value = entry.Timestamp });

            command.ExecuteNonQuery();
        }
    }
}