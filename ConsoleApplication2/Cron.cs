using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Cron
    {
        public string GenerateWeeklyExpression(DayOfWeek[] days, TimeSpan StartTime = default(TimeSpan), int frequency = 0, TimeSpan EndTime = default(TimeSpan))
        {

            //FORMAT
            //SECONDS || MINUTES ||   HOURS  || DAYS OF THE MONTH || MONTH || DAYS OF THE WEEK || (optional) YEAR
            //   0    ||    0    ||  18-22/2  ||        ?          ||   *   || MON,TUE,WED,THUR,FRI (or 1,2,3,4,5) || (nothing)
            //Example Above = every 2 hours starting from 6pm to 10pm Monday - Friday



            //Check if the task should just be considerred daily
            if (days.Length> 6 )
            {
                return GenerateDailyExpression(StartTime);
            }
            string DaysToSchedule = "";
            //schedule Seconds
            DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length,  StartTime.Seconds + " ");
            //schedule Minutes
            DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, StartTime.Minutes + " ");

            // schedule Hours
            if(EndTime !=  default(TimeSpan) && frequency != 0)
                //if both end time and frequncy are specified we need both '-' and '/' characters
                DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, StartTime.Hours + "-" + EndTime.Hours + "/" + frequency.ToString() + " ");
            else if (EndTime != default(TimeSpan))
            {
                //we only need '-' character
                DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, StartTime.Hours + "-" + EndTime.Hours + " ");
            }
            else if(frequency != 0)
            {
                //we only need '/' character
                DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, StartTime.Hours + "/" + frequency + " ");
            }
            else
                //just need hours
                DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, StartTime.Hours + " ");

            //we don't need to worry about day of the month for a Weekly schedule
            DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, "? ");
            //we don't need to worry about months for a Weekly schedule
            DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, "* ");

            foreach (int day in days)
            {
                DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, day.ToString() + ",");
            }

            DaysToSchedule = DaysToSchedule.Remove(DaysToSchedule.Length-1);
            //DaysToSchedule = DaysToSchedule.Insert(DaysToSchedule.Length, " ");
            return DaysToSchedule;

        }

        public string GenerateDailyExpression(TimeSpan TimeOfDay = default(TimeSpan))
        {
            string schedule = "";
            schedule = schedule.Insert(schedule.Length, TimeOfDay.Seconds + " ");
            schedule = schedule.Insert(schedule.Length, TimeOfDay.Minutes + " ");
            schedule = schedule.Insert(schedule.Length, TimeOfDay.Hours + " ");
            schedule = schedule.Insert(schedule.Length, "? ");
            schedule = schedule.Insert(schedule.Length, "* ");

            schedule = schedule.Insert(schedule.Length, "*");
            return schedule;
        }

        public static void Main()
        {

        }
    }
}