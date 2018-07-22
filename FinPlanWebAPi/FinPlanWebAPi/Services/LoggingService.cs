using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Reflection;

namespace FinPlanWebAPi.Services 
{
    public class LoggingService : ILoggingService
    {
        private readonly string AppName;
        private readonly string LogName;

        public LoggingService(string _appName = nameof(FinPlanWebAPi), string _logName = "Application")
        {
            AppName = _appName;
            LogName = _logName;

            if (!EventLog.SourceExists(AppName))
                EventLog.CreateEventSource(AppName, LogName);
        }

        public void AddErrorWinEventLog(Exception ex)
        {
            EventLog.WriteEntry(AppName, ex.ToString(), EventLogEntryType.Error);
        }

    }
}