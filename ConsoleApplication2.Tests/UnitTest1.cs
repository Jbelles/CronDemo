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
        public void ScheduleOnlySpecificDays() //defaults 12:00 am on day of
        {
            string result = CronFunctions.GenerateWeeklyExpression( new DayOfWeek[] 
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday }
            );
            //12:00am on mon/wed/fri/sat
            Assert.AreEqual("0 0 0 ? * FRI,WED,MON,SAT", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecifcDaysWithStartTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(new DayOfWeek[]
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(9, 30, 0)
            );
            // 9:30am on mon/wed/fri/sat
            Assert.AreEqual("0 30 9 ? * FRI,WED,MON,SAT", result);
        }
        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecifcDaysWithHourlyFrequency() //defaults to start of the day
        {
            string result = CronFunctions.GenerateWeeklyExpression(new DayOfWeek[]
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            frequency: 8
            );
            //every 8 hours starting at 12:00am on mon/wed/fri/sat
            Assert.AreEqual("0 0 0/8 ? * FRI,WED,MON,SAT", result);
        }


        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecificDaysWithEndTime() //this defaults to hourly recurrence from the start of the day
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            EndTime: new TimeSpan(15, 0, 0)
            );
            // from 12am to 3pm every hour on mon/wed/fri/sat
            Assert.AreEqual("0 0 0-15 ? * FRI,WED,MON,SAT", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecificDaysWithHourlyFrequencyAndStartTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(new DayOfWeek[]
            { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(9, 30, 0),
            8
            );
            // from 9:30am every 8 hours on mon / wed / fri / sat
            Assert.AreEqual("0 30 9/8 ? * FRI,WED,MON,SAT", result);
        }


        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecificDaysWithStartTimeAndEndTime() //this defaults to an hourly recurrence
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(5,0,0),
            EndTime: new TimeSpan(15, 0, 0)
            );
            //from 5am to 3pm every hour on mon / wed / fri / sat
            Assert.AreEqual("0 0 5-15 ? * FRI,WED,MON,SAT", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecificDaysWithHourlyFrequencyAndEndTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            frequency: 2,
            EndTime: new TimeSpan(15, 0, 0)
            );
            //every other hour from 12am to 3pm on mon / wed / fri / sat
            Assert.AreEqual("0 0 0-15/2 ? * FRI,WED,MON,SAT", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleSpecificDaysRepeatHourlyWithStartTimeAndEndTime()
        {
            string result = CronFunctions.GenerateWeeklyExpression(
            new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Wednesday, DayOfWeek.Monday, DayOfWeek.Saturday },
            new TimeSpan(2,45,15),
            2,
            new TimeSpan(15, 0, 0)
            );
            //every other hour from 2am to 3pm on mon / wed / fri / sat
            Assert.AreEqual("15 45 2-15/2 ? * FRI,WED,MON,SAT", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleEveryDay() //essentially the same as Daily 
        {
            string result = CronFunctions.GenerateWeeklyExpression( new DayOfWeek[] 
            { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday },
            new TimeSpan(9, 30, 0));
            //every day at 9:30
            Assert.AreEqual("0 30 9 ? * *", result);
        }

        [TestMethod]
        [TestCategory("CRON")]
        public void ScheduleOnceDaily()
        {
            string result = CronFunctions.GenerateDailyExpression(new TimeSpan(9, 30, 0));
            //every day at 9:30
            Assert.AreEqual("0 30 9 ? * *", result);
        }
    }
}