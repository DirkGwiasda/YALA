using MySqlConnector;
using System.Data;

namespace Net.Gwiasda.Yala.Infrastructure
{
    public abstract class MariaDbRepository
    {
        private readonly string _connectionString;

        protected MariaDbRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected async Task<IDbConnection> GetConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}