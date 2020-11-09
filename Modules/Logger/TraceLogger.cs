/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.Interfaces.Logging;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Assessment.Logger
{
    public class TraceLogger : ITraceLogger
    {
        private log4net.ILog log;

        public TraceLogger()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string path = String.Format("{0}{1}{2}{3}{4}", @"Trace\TraceLog_", DateTime.Now.Month.ToString()
                                   , DateTime.Now.Day.ToString()
                                   , DateTime.Now.Year.ToString(), ".log");

            ChangeFilePath("TraceLogFileAppender", path);
        }
        private static void ChangeFilePath(string appenderName, string newFilename)
        {
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetRepository();
            foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
            {
                if (appender.Name.CompareTo(appenderName) == 0 && appender is log4net.Appender.FileAppender)
                {
                    log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)appender;
                    fileAppender.File = System.IO.Path.Combine(fileAppender.File, newFilename);
                    fileAppender.ActivateOptions();
                }
            }
        }
        public void Info(string msg)
        {
            StackFrame stackFrame = new StackFrame(1);
            log.Info("[" + Assembly.GetCallingAssembly().GetName().Name + "." + stackFrame.GetMethod().DeclaringType.Name + "." + stackFrame.GetMethod().Name + "]" + msg);
        }
    }
}
