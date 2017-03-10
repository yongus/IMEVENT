using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace IMEVENT.Exceptions
{
    public class GlobalExceptionHandling : Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter, IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Dispose()
        {
           
        }

        public void OnException(ExceptionContext context)
        {
            logger.Log(LogLevel.Error, "Un expected exception occured view stack trace: " + context.Exception.StackTrace);
        }
    }
}
