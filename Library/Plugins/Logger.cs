using System;
using System.Collections.Generic;

namespace Library.Plugins.Logger
{
    public abstract class Logger
    {
        public string[]  LogPaths { get; protected set; }

        public void Log(string message)
        {
            foreach(string LogPath in LogPaths)
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(LogPath, true))
                {
                    file.WriteLine(DateTime.Now + " : " + message);
                }
            }
        }
    }
}
