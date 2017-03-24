using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TimedCacheCore;

namespace TimedCacheBase
{
    public class TimedCache<T> : ITimedCache<T>
    {
        public event Action<T> ItemTimedout;

        private readonly List<CachedElement<T>> _elements;
        private readonly TimeSpan _timeout;
        private readonly ITimeProvider _timerProvider;

        public TimedCache(TimeSpan timeoutPeriod, TimeSpan timerResulution) :
            this(timeoutPeriod, new TimerWrapper(timerResulution), new DefaultTimeProvider())
        {

        }

        public TimedCache(TimeSpan timeoutPeriod, ITimer timer, ITimeProvider timerProvider)
        {
            //Guard Clauses
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            if (timerProvider == null)
            {
                throw new ArgumentNullException(nameof(timerProvider));
            }

            _elements = new List<CachedElement<T>>();
            _timeout = timeoutPeriod;
            _timerProvider = timerProvider;

            timer.Start();
            timer.Elapsed += CleanOldElementsFromCache;
        }

        public int Count => _elements.Count();

        public void Add(T element)
        {
            _elements.Add(WrapToCachedElement(element));
        }

        public void Remove(T element)
        {
            _elements.Remove(WrapToCachedElement(element));
        }

        private void CleanOldElementsFromCache()
        {
            _elements.RemoveAll(ElementIsOld);
            return;
        }

        private bool ElementIsOld(CachedElement<T> cachedElement)
        {
            return _timerProvider.Now - cachedElement.CachingTime > _timeout;
        }

        private CachedElement<T> WrapToCachedElement(T element)
        {
            return new CachedElement<T>(element, _timerProvider.Now);
        }

        protected virtual void OnItemTimedout(T timedoutElement)
        {
            ItemTimedout?.Invoke(timedoutElement);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.Select(cachedElement => cachedElement.Element).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
