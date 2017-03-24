using System;
using TimedCacheCore;

namespace TimedCacheBase
{
    public class DefaultTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}