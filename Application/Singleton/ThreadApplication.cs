using Library.Class;
using Library.Interface;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ThreadApplication
{
    public class ThreadApplication
    {
        private readonly Dictionary<string, System.Timers.Timer> _intervals;
        private readonly Dictionary<string, IRunnable> _runs;
        private readonly Dictionary<string, CancellationTokenSource> _tokens;

        public ThreadApplication()
        {
            _intervals = new Dictionary<string, System.Timers.Timer>();
            _runs = new Dictionary<string, IRunnable>();
            _tokens = new Dictionary<string, CancellationTokenSource>();
        }

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
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            _runs.Add(key, runner);
            _tokens.Add(key, tokenSource);

            Task task = Task.Run(() =>
            {
                try
                {
                    runner.Run(tokenSource.Token, loggers);
                }
                catch (OperationCanceledException e) 
                {
                    _tokens.Remove(key);
                    _runs.Remove(key);
                }
                finally
                {
                    tokenSource.Dispose();
                }

                _runs.Remove(key);
                _tokens.Remove(key);

            }, tokenSource.Token);
        }

        public void CancelRun(string key)
        {
            _tokens[key].Cancel();
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
    }
}
