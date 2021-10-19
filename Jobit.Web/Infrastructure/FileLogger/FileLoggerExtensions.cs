using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jobit.Web.Infrastructure.FileLogger
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                string filePath, Func<LogLevel, bool> filter = null)
        {
            factory?.AddProvider(new FileLoggerProvider(filePath, filter));
            return factory;
        }

    }
}
