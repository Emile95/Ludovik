using System;
using System.Timers;

namespace Application.ThreadApplication
{
    public class ThreadApplication : IThreadApplication
    {
        #region IThreadApplication

        public void AddInterval(int sec, Action action)
        {
            Timer timer = new System.Timers.Timer();
            timer.Interval = sec;

            timer.Elapsed += (Object source, System.Timers.ElapsedEventArgs e) => {
                action();
            };

            timer.Enabled = true;
        }

        #endregion
    }
}
