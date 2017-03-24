using System;

namespace TimedCacheCore
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}