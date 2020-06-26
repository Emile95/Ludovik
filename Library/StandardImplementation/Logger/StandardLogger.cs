using Library.Plugins.Logger;

namespace Library.StandardImplementation.StandardLogger
{
    public class StandardLogger : Logger
    {
        public StandardLogger()
        {
            LogPaths = new string[] {
                "logs\\standard.log"
            };
        }
    }
}
