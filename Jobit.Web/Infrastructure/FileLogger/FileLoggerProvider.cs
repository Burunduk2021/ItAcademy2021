using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jobit.Web.Infrastructure.FileLogger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly Func<LogLevel, bool> _filter;
        private string path;
        public FileLoggerProvider(string _path, Func<LogLevel, bool> filter)
        {
            _filter = filter;
            path = _path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path,  _filter);
        }

        public void Dispose()
        {
        }

    }
}
