using Library.Class;
using Library.Plugins.Logger;
using System;

namespace Library.StandardImplementation.StandardLogger
{
    public class StandardLogger : Logger
    {
        protected sealed override string GetFilePath()
        {
            return "logs\\standard.log";
        }

        protected sealed override string GetLogLine(Log log)
        {
            return "|" + log.LogType + "| " + DateTime.Now + " : " + log.Message;
        }
    }
}
