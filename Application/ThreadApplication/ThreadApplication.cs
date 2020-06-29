using Library.Class;
using Library.Interface;
using Library.Plugins.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Application.ThreadApplication
{
    public class ThreadApplication : IThreadApplication
    {
        private readonly Dictionary<string, System.Timers.Timer> _intervals;
        private readonly Dictionary<string, IRunnable> _runs;
        private readonly Dictionary<string, Thread> _threads;

        public ThreadApplication()
        {
            _intervals = new Dictionary<string, System.Timers.Timer>();
            _runs = new Dictionary<string, IRunnable>();
            _threads = new Dictionary<string, Thread>();
        }

        #region IThreadApplication

        public void AddInterval(string key, int sec, Action action)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = sec;

            timer.Elapsed += (source, e) => {
                action();
            };

            timer.Enabled = true;

            _intervals.Add(key, timer);
        }

        public void RemoveInterval(string key)
        {
            _intervals[key].Enabled = false;
            _intervals.Remove(key);
        }

        public void AddRun(string key, IRunnable runner, LoggerList loggers)
        {
            _runs.Add(key, runner);
            Thread thread = new Thread(() => { 
                runner.Run(loggers);
                _runs.Remove(key);
                _threads.Remove(key);
            });
            _threads.Add(key,thread);
            thread.Start();
        }

        public void CancelRun(string key)
        {
            _threads[key].Abort();
            _threads.Remove(key);
            _runs.Remove(key);
        }

        public object[] GetRuns<T>() where T : class
        {
            List<T> runs = new List<T>();
            foreach(KeyValuePair<string, IRunnable> run in _runs)
            {
                if (run.Value is T)
                    runs.Add(run.Value as T);
            }
            return runs.ToArray();
        }

        #endregion
    }
}
