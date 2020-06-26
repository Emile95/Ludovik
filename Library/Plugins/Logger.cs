using System;
using System.Collections.Generic;

namespace Library.Plugins.Logger
{
    public abstract class Logger
    {
        public Dictionary<string,string> LogPaths { get; protected set; }

        public void Log(string key, string message)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(LogPaths[key], true))
            {
                file.WriteLine(DateTime.Now + " : " + message);
            }
        }
    }
}
