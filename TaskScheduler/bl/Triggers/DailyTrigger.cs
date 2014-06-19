using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CH.Tutteli.TaskScheduler.BL.Triggers
{
    public class DailyTrigger : ARecuringTrigger
    {
        private int _recursEveryXDays;
        public int RecursEveryXDays
        {
            get { return _recursEveryXDays; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("RecursEveryXDays must be greater than 0");
                }

                _recursEveryXDays = value;
            }
        }


        public DailyTrigger(DateTime startDate, DateTime endDate, int recursEveryXDays)
            : base(startDate, endDate)
        {
            RecursEveryXDays = recursEveryXDays;
        }

        protected override DateTime CalculateNextTrigger(DateTime dateTime)
        {
            var diffDays = (dateTime - StartDate).Days;
            var daysToNextTrigger = diffDays % RecursEveryXDays;
            var compare = dateTime.TimeOfDay.CompareTo(StartDate.TimeOfDay);

            DateTime nextTrigger;
            if (compare > 0)
            {
                nextTrigger = SetTimeToStartTime(dateTime).AddDays(RecursEveryXDays - daysToNextTrigger);
            }
            else if (compare < 0)
            {
                nextTrigger = SetTimeToStartTime(dateTime).AddDays(daysToNextTrigger);
            }
            else
            {
                nextTrigger = dateTime.AddDays(daysToNextTrigger);
            }
            return nextTrigger;
        }
    }
}