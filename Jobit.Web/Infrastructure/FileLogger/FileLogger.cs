using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jobit.Web.Infrastructure.FileLogger
{
    public class FileLogger : ILogger
    {
        private readonly Func<LogLevel, bool> _filter;
        private string filePath;
        private static object _lock = new object();
        public FileLogger(string path, Func<LogLevel, bool> filter)
        {
            _filter = filter;
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter == null || _filter(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel) && (formatter != null))
            {
                lock (_lock)
                {
                    File.AppendAllText(filePath, $"{logLevel} - {eventId.Id} " + 
                        $"- {formatter(state, exception)}" + Environment.NewLine);
                }
            }
                
        }

    }
}
