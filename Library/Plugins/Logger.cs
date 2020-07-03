using Library.Class;

namespace Library.Plugins.Logger
{
    public abstract class Logger
    {
        protected abstract string GetFilePath();

        protected abstract string GetLogLine(Log log);

        public void Log(Log log, string partialPath = null)
        {
            bool repeat = true;
            while (repeat)
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(GetFilePath(), true);
                    file.WriteLine(GetLogLine(log));
                    file.Close();
                    repeat = false;
                }
                catch(System.Exception e) { }
            }
        }
    }
}
