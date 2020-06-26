using Library.Class;
using System;

namespace Library.Plugins.Logger
{
    public abstract class Logger
    {
        protected string[] _filePaths;

        public void Log(Log log)
        {
            foreach(string logPath in _filePaths)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(logPath, true))
                {
                    file.WriteLine(DateTime.Now + " : " + "|" + log.LogType + "| " + log.Message);
                }
            }
        }
    }
}
