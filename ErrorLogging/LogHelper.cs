using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;

namespace ErrorLogging
{
    public class LogHelper
    {
        public static ILog GetLogger(Type type)
        {
            ILog log = log4net.LogManager.GetLogger(type);
            //FileAppender _fileAppender = new FileAppender()

            return (log);
        }

        public static string CreateWebLog(string sessionState, string currentThread, bool authenticated, Exception ex)
        {
            StringBuilder errorString = new StringBuilder();

            if (ex != null)
            {
                //StringBuilder errorString = new StringBuilder();
                errorString.Append("An error was caught in the Application_Error event.\n");
                errorString.Append("Error in: " + (sessionState));
                errorString.Append("\nError Message: " + ex.Message);

                if (ex.InnerException != null)
                    errorString.Append("\nInner Error Message: " + ex.InnerException.Message);

                errorString.Append("\n\nStack Trace: " + ex.StackTrace);

                if (sessionState != null)
                {
                    errorString.Append($"Session: Identity name:[{currentThread}] IsAuthenticated:{authenticated}");
                }

                //log.Error(errorString.ToString());
            }

            return errorString.ToString();      
        }
    }
}
