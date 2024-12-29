using MySqlConnector;

namespace Net.Gwiasda.Yala.Infrastructure
{
    public sealed class MariaDbLogEntryRepository : MariaDbRepository, ILogEntryRepository
    {
        public MariaDbLogEntryRepository(string connectionString) : base(connectionString) { }

        public async Task<List<string>> ReadApplicationNamesAsync()
        {
            const string sql = @"
                SELECT DISTINCT appname FROM LogEntry;";


            using var connection = await base.GetConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            using (var reader = command.ExecuteReader())
            {
                var appNames = new List<string>();
                while (reader.Read())
                {
                    appNames.Add(reader.GetString(0));
                }
                return appNames;
            }
        }

        public async Task<List<LogEntry>> ReadLogEntriesFromAppAsync(string appName, int startIndex, int count)
        {
            if (string.IsNullOrWhiteSpace(appName)) throw new ArgumentNullException(nameof(appName));
            if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (count > 1000) throw new ArgumentOutOfRangeException(nameof(count));

            const string sql = @"
                SELECT id, sourceName, message, logType, `timestamp` 
                FROM yaladb.LogEntry
                WHERE appName = @appName
                LIMIT @limit OFFSET @offset;";


            using var connection = await base.GetConnectionAsync();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            command.Parameters.Add(new MySqlParameter("@appName", MySqlDbType.VarChar) { Value = appName });
            command.Parameters.Add(new MySqlParameter("@limit", MySqlDbType.Int32) { Value = count });
            command.Parameters.Add(new MySqlParameter("@offset", MySqlDbType.Int32) { Value = startIndex });

            using (var reader = command.ExecuteReader())
            {
                var entries = new List<LogEntry>();
                while (reader.Read())
                {
                    entries.Add(
                        new LogEntry
                        {
                            Id = reader.GetString(0),
                            SourceName = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Message = reader.GetString(2),
                            LogType = (LogType)reader.GetInt32(3),
                            Timestamp = reader.GetDateTime(4),
                            AppName = appName
                        }
                    );
                }
                return entries;
            }
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