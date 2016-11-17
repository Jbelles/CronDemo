using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication2;

namespace ConsoleApplication2.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Cron CronFunctions;

        [TestInitialize]
        public void TestInitialize()
        {
            CronFunctions = new Cron();
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestBlankEveryWeek()
        {
            string result = CronFunctions.GenerateWeeklyExpression( new DayOfWeek[] 
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday }
            );
            Assert.AreEqual("0 0 0 ? * 5,3,1,6", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekStartTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(new DayOfWeek[]
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(9, 30, 0)
            );
            Assert.AreEqual("0 30 9 ? * 5,3,1,6", result);
        }
        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekHourly()
        {
            string result = CronFunctions.GenerateWeeklyExpression(new DayOfWeek[]
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            frequency: 8
            );
            Assert.AreEqual("0 0 0/8 ? * 5,3,1,6", result);
        }


        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekEndTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            EndTime: new TimeSpan(15, 0, 0)
            );
            Assert.AreEqual("0 0 0-15 ? * 5,3,1,6", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekHourlyAndStartTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(new DayOfWeek[]
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(9, 30, 0),
            8
            );
            Assert.AreEqual("0 30 9/8 ? * 5,3,1,6", result);
        }


        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekStartTimeAndEndTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(5,0,0),
            EndTime: new TimeSpan(15, 0, 0)
            );
            Assert.AreEqual("0 0 5-15 ? * 5,3,1,6", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekRepeatHourlyAndEndTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            frequency: 2,
            EndTime: new TimeSpan(15, 0, 0)
            );
            Assert.AreEqual("0 0 0-15/2 ? * 5,3,1,6", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleDaysEveryWeekRepeatHourlyAndStartTimeAndEndTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(2,45,15),
            2,
            new TimeSpan(15, 0, 0)
            );
            Assert.AreEqual("15 45 2-15/2 ? * 5,3,1,6", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleEveryDayEveryWeek()
        {
            string result = CronFunctions.GenerateWeeklyExpression( new DayOfWeek[] 
            { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday },
            new TimeSpan(9, 30, 0));
            Assert.AreEqual("0 30 9 ? * *", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void TestScheduleEveryDay()
        {
            string result = CronFunctions.GenerateDailyExpression(new TimeSpan(9, 30, 0));
            Assert.AreEqual("0 30 9 ? * *", result);
        }
    }
}