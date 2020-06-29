using Library.Class;
using Library.Interface;
using System;

namespace Application.ThreadApplication
{
    public interface IThreadApplication
    {
        void AddInterval(string key, int sec, Action action);
        void RemoveInterval(string key);

        void AddRun(string key, IRunnable runner, LoggerList loggers);

        void CancelRun(string key);

        object[] GetRuns<T>() where T : class;
    }
}
