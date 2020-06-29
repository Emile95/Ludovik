using Library.Class;
using System.Threading;

namespace Library.Interface
{
    public interface IRunnable
    {
        void Run(CancellationToken taskCancelToken, LoggerList loggers = null);
    }
}
