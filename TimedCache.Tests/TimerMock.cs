using System;
using TimedCacheCore;

namespace TimedCacheTests
{
    public class TimerMock : ITimer
    {
        public void Start()
        {

        }

        public event Action Elapsed;

        public virtual void RaiseElapsed()
        {
            Elapsed?.Invoke();
        }
    }
}