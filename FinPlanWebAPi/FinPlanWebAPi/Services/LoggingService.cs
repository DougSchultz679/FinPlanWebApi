using System;
using System.Diagnostics;

namespace FinPlanWebAPi.Services 
{
    public class LoggingService : ILoggingService
    {
        private readonly string AppName = nameof(FinPlanWebAPi);
        private readonly string LogName = "Application";

        public LoggingService()
        {
            if (!EventLog.SourceExists(AppName))
                EventLog.CreateEventSource(AppName, LogName);
        }

        public void AddErrorWinEventLog(Exception ex)
        {
            EventLog.WriteEntry(this.AppName, ex.ToString(), EventLogEntryType.Error);
        }

    }
}