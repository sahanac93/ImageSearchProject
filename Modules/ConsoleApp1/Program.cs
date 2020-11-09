using System;
using System.Diagnostics;
using System.Reflection;

namespace Assessment
{
    public interface ITrace
    {
        void info(string msg);
    }

    public interface IDevLogger
    {
        void Error(object msg);
        void Info(string msg);

        void Warning(string msg);
    }

    class Program
    {
        static IDevLogger devLog = new DevLogger();
        static ITrace traceLog = new TraceLog();

        static void Main(string[] args)
        {
            try
            {
                traceLog.info("Trace log sample");
                CreateException();
            }
            catch (Exception ex)
            {
                devLog.Error(ex);
            }
            Console.ReadLine();
        }

        private static void CreateException()
        {
            devLog.Info("Entering method - CreateException()");
            devLog.Info("Exiting method - CreateException()");
            throw new Exception("Sample Error thrown...");
        }
    }

    public class DevLogger : IDevLogger
    {
        private log4net.ILog log;



        public DevLogger()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string path = String.Format("{0}{1}{2}{3}{4}", @"Development\DevLog_", DateTime.Now.Month.ToString()
                                   , DateTime.Now.Day.ToString()
                                   , DateTime.Now.Year.ToString(), ".log");

            ChangeFilePath("DevLogFileAppender", path);

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

        public void Error(object msg)
        {
            //var name = log.Logger.Name;
            StackFrame stackTrace = new StackFrame(1);
            Console.WriteLine(stackTrace.GetMethod().Name);
            Console.WriteLine(stackTrace.GetMethod().DeclaringType.Name);
            string asm = Assembly.GetCallingAssembly().GetName().ToString();
            log.Error("[" + Assembly.GetCallingAssembly().GetName().Name + "." + stackTrace.GetMethod().DeclaringType.Name + "." + stackTrace.GetMethod().Name + "]" + msg);
        }

        public void Info(string msg)
        {
            StackFrame stackFrame = new StackFrame(1);
            log.Info("[" + Assembly.GetCallingAssembly().GetName().Name + "." + stackFrame.GetMethod().DeclaringType.Name + "." + stackFrame.GetMethod().Name + "]" + msg);
        }

        public void Warning(string msg)
        {
            StackFrame stackFrame = new StackFrame(1);
            log.Warn("[" + Assembly.GetCallingAssembly().GetName().Name + "." + stackFrame.GetMethod().DeclaringType.Name + "." + stackFrame.GetMethod().Name + "]" + msg);
        }
    }

    public class TraceLog : ITrace
    {
        private log4net.ILog log;

        public TraceLog()
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
        public void info(string msg)
        {
            StackFrame stackFrame = new StackFrame(1);
            log.Info("[" + Assembly.GetCallingAssembly().GetName().Name + "." + stackFrame.GetMethod().DeclaringType.Name + "." + stackFrame.GetMethod().Name + "]" + msg);
        }
    }
}
