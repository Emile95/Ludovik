using Library.Plugins.Logger;

namespace Library.StandardImplementation.StandardLogger
{
    public class StandardLogger : Logger
    {
        public StandardLogger()
        {
            LogPaths = new System.Collections.Generic.Dictionary<string, string>();
            LogPaths["standard"] = "logs\\standard.log";
        }
    }
}
