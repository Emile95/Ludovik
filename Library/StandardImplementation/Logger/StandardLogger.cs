using Library.Plugins.Logger;

namespace Library.StandardImplementation.StandardLogger
{
    public class StandardLogger : Logger
    {
        public StandardLogger()
        {
            _logPaths = new string[] {
                "logs\\standard.log"
            };
        }
    }
}
