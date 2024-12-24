using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Gwiasda.Yala.Infrastructure
{
    public sealed class MariaDbLogEntryRepository : MariaDbRepository, ILogEntryRepository
    {
        public Task ForceRepositoryExists()
        {
            throw new NotImplementedException();
        }

        public Task WriteLogEntryAsync(LogEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}