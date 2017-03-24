using NUnit.Framework;
using System;
using Moq;
using TimedCacheBase;
using TimedCacheCore;

namespace TimedCacheTests
{
    [TestFixture]
    public class TimedCacheTests
    {
        private TimedCache<int> CreateDefaultTimedCache()
        {
         var timeProviderMock = new Mock<ITimeProvider>();
         return new TimedCache<int>(TimeSpan.MaxValue, new TimerMock(), timeProviderMock.Object);
        }

        private readonly int _defaultElement = 1;
        private readonly DateTime _defaultTime = new DateTime(2016,01,02,03,04,05,06);

        [Test]
        public void Count_BeforeAnyElementsAdded_ReturnZero()
        {
            ITimedCache<int> timedCache = CreateDefaultTimedCache();
            Assert.AreEqual(0, timedCache.Count);
        }

        [Test]
        public void Count_AfterFirstElementAdded_ReturnsTwo()
        {
            ITimedCache<int> timedCache = CreateDefaultTimedCache();
            timedCache.Add(_defaultElement);
            Assert.AreEqual(1, timedCache.Count);
        }

        [Test]
        public void Count_AddingElementsThenRemovingIt_ReturnsZero()
        {
            ITimedCache<int> timedCache = CreateDefaultTimedCache();
            timedCache.Add(_defaultElement);
            timedCache.Remove(_defaultElement);
            Assert.AreEqual(0, timedCache.Count);
        }

        [Test]
        public void Count_AddItemThenTimeout_ReturnsZero()
        {
            var timedCache = AddItemAndSimulateTimerCall(
                timeoutPeriod: TimeSpan.FromMinutes(value: 2),
                timeUntilTimerCall: TimeSpan.FromMinutes(value: 3));

            //Assert:
            Assert.AreEqual(expected: 0, actual: timedCache.Count);
        }

        [Test]
        public void Count_AddItemThenWaitLessThenTimeout_ReturnsOne()
        {
            var timedCache = AddItemAndSimulateTimerCall(
                timeoutPeriod: TimeSpan.FromMinutes(2),
                timeUntilTimerCall: TimeSpan.FromMinutes(1));

            Assert.AreEqual(1, timedCache.Count);
        }

        [Test]
        public void AddItemThenTimeout_EventCalled()
        {
            //Todo: Write UT
        }

        [Test]
        public void AddTwoItems_ForeachRunsTwoIterations()
        {
            ITimedCache<int> timedCache = CreateDefaultTimedCache();
            timedCache.Add(_defaultElement);
            timedCache.Add(_defaultElement);

            int iterationsPreformed = 0;
            foreach (int item in timedCache)
            {
                iterationsPreformed++;
            }

            Assert.AreEqual(2, iterationsPreformed);
        }

        //Todo: Write IT

        //Todo:Refactor name
        private TimedCache<int> AddItemAndSimulateTimerCall(TimeSpan timeoutPeriod, TimeSpan timeUntilTimerCall)
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.Setup(timeProvider => timeProvider.Now).Returns(_defaultTime);
            TimerMock timerMock = new TimerMock();

            var timedCache = new TimedCache<int>(timeoutPeriod, timerMock, timeProviderMock.Object);

            timedCache.Add(_defaultElement);
            timeProviderMock.Setup(timeProvider => timeProvider.Now).Returns(_defaultTime + timeUntilTimerCall);
            timerMock.RaiseElapsed();
            return timedCache;
        }
    }
}