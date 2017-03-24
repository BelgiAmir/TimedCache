using System;

namespace TimedCacheBase
{
    public class CachedElement<T> : IEquatable<CachedElement<T>>
    {
        public T Element { get; }
        public DateTime CachingTime { get; }

        public CachedElement(T cachedElement, DateTime cachingTime)
        {
            Element = cachedElement;
            CachingTime = cachingTime;
        }

        public bool Equals(CachedElement<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Element.Equals(other.Element);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((CachedElement<T>) obj);
        }

        public override int GetHashCode()
        {
            return Element.GetHashCode();
        }
    }
}