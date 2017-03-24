using System;
using System.Collections.Generic;

namespace TimedCacheCore
{
    public interface ITimedCache<T> : IEnumerable<T>
    {
        int Count { get; }
        void Add(T element);
        void Remove(T element);

        event Action<T> ItemTimedout;
    }
}