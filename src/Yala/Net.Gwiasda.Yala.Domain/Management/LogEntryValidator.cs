using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Gwiasda.Yala
{
    public class LogEntryValidator : ILogEntryValidator
    {
        public void Validate(LogEntry entry)
        {
            if(entry == null) throw new ArgumentNullException("entry");
        }
    }
}
