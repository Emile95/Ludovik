using System;

namespace Library.Plugins.Logger
{
    public abstract class Logger
    {
        protected string[] _logPaths;

        public void Log(string message)
        {
            foreach(string logPath in _logPaths)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(logPath, true))
                {
                    file.WriteLine(DateTime.Now + " : " + message);
                }
            }
        }
    }
}
