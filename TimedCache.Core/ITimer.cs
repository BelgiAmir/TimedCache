using System;

namespace TimedCacheCore
{
    public interface ITimer
    {
        void Start();
        event Action Elapsed;
    }
}