using System;
using System.Timers;
using NUnit.Framework;
using TimedCacheBase;

namespace TimedCacheTests
{
    [TestFixture]
    public class TimedCacheCtorTests
    {
        private readonly TimeSpan _defaultIntervalLength = TimeSpan.FromSeconds(1);

        [Test]
        public void Ctor_NullTimer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TimedCache<int>(TimeSpan.MaxValue, timer: null, timerProvider: new DefaultTimeProvider()));
        }

        [Test]
        public void Ctor_NullTimeProvider_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TimedCache<int>(TimeSpan.MaxValue,new TimerMock(),  timerProvider: null));
        }

        [Test]
        public void CtorWithDefaults_DoesNotThrow()
        {
            Assert.DoesNotThrow(()=>new TimedCache<int>(_defaultIntervalLength, _defaultIntervalLength));
        }

        [Test]
        public void Ctor_DoesNotThrow()
        {
            Assert.DoesNotThrow(()=>
                new TimedCache<int>(_defaultIntervalLength, new TimerWrapper(_defaultIntervalLength), new DefaultTimeProvider()));
        }
    }
}