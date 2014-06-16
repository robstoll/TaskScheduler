using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CH.Tutteli.TaskScheduler.Triggers
{
    public class WeeklyTrigger : ARecuringTrigger
    {
        private int _recursEveryXWeeks;
        public int RecursEveryXWeeks
        {
            get { return _recursEveryXWeeks; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("RecursEveryXWeeks must be greater than 0");
                }
                _recursEveryXWeeks = value;
            }
        }

        private ISet<DayOfWeek> _triggerWhenDayOfWeek;
        public ISet<DayOfWeek> TriggerWhenDayOfWeek
        {
            get { return _triggerWhenDayOfWeek; }

            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new ArgumentException("TriggerWhenDayOfWeek null or empty , at least one day of the week has to be specified, otherwise it would never trigger");
                }
                _triggerWhenDayOfWeek = value;
            }
        }

        public static WeeklyTrigger CreateAllDayWeeklyTrigger(DateTime startDate, DateTime endDate, int recursEveryXWeeks)
        {
            return new WeeklyTrigger(startDate, endDate, recursEveryXWeeks,
                new HashSet<DayOfWeek>(){
					DayOfWeek.Monday,
					DayOfWeek.Tuesday,
					DayOfWeek.Wednesday,
					DayOfWeek.Thursday,
					DayOfWeek.Friday,
					DayOfWeek.Saturday,
					DayOfWeek.Sunday
				});
        }

        public WeeklyTrigger(DateTime startDate, DateTime endDate, int recursEveryXWeeks, ISet<DayOfWeek> triggerWhenDayOfWeek)
            : base(startDate, endDate)
        {
            RecursEveryXWeeks = recursEveryXWeeks;
            TriggerWhenDayOfWeek = triggerWhenDayOfWeek;
        }


        protected override DateTime CalculateNextTrigger(DateTime dateTime)
        {
            var adjustedDateTime = CheckAndAdjustTime(dateTime);
            var nextPotentialTrigger = GetNextPotentialDay(adjustedDateTime);

            int daysToNextWeek = GetDaysToNextWeek(StartDate);
            var diffDays = (nextPotentialTrigger - StartDate).Days;
            bool isNotWithinTheSameWeekAsStartDate = diffDays >= daysToNextWeek;

            int weeksToNextTrigger = 0;
            if (RecursEveryXWeeks > 1 && isNotWithinTheSameWeekAsStartDate)
            {
                var diffWeeks = (diffDays - daysToNextWeek) / 7 + 1;
                weeksToNextTrigger = RecursEveryXWeeks - diffWeeks % RecursEveryXWeeks;
            }

            return nextPotentialTrigger.AddDays(weeksToNextTrigger * 7);
        }

        private int GetDaysToNextWeek(DateTime dateTime)
        {
            int dayOfTheWeek = dateTime.DayOfWeek != DayOfWeek.Sunday ?
                         (int)dateTime.DayOfWeek - 1
                         : 6;

            return 7 - dayOfTheWeek;
        }

        private DateTime GetNextPotentialDay(DateTime dateTime)
        {
            var day = dateTime.DayOfWeek;
            int addDays = 0;
            while (!_triggerWhenDayOfWeek.Contains(day))
            {
                ++addDays;
                ++day;
                if ((int)day == 7)
                {
                    day = 0;
                }
            }
            return dateTime.AddDays(addDays);
        }
    }
}