using Library.Class;

namespace Library.Plugins.Logger
{
    public abstract class Logger
    {
        protected string[] _filePaths;

        protected abstract string GetLogLine(Log log);

        public void Log(Log log)
        {
            foreach(string logPath in _filePaths)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(logPath, true))
                {
                    file.WriteLine(GetLogLine(log));
                }
            }
        }
    }
}
