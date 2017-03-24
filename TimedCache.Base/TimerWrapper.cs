using System;
using System.Timers;
using TimedCacheCore;

namespace TimedCacheBase
{
    public class TimerWrapper : ITimer
    {
        private readonly Timer _timer;

        public TimerWrapper(TimeSpan interval)
        {
            _timer = new Timer(interval.TotalMilliseconds);
            _timer.Elapsed += OnElpased;
        }

        private void OnElpased(object sender, ElapsedEventArgs e)
        {
            RaiseElapsed();
            return;
        }

        public void Start()
        {
            _timer.Start();
        }

        public event Action Elapsed;

        protected virtual void RaiseElapsed()
        {
            Elapsed?.Invoke();
        }
    }
}