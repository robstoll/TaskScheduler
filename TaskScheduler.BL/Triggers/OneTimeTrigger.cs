using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CH.Tutteli.TaskScheduler.BL.Triggers
{
    public class OneTimeTrigger : ITrigger
    {
        private DateTime _triggerDate;
        public DateTime TriggerDate
        {
            get
            {
                return _triggerDate;
            }
            set
            {
                var diff = value - DateTime.MinValue;
                if (diff.TotalDays < 1)
                {
                    throw new ArgumentException("TriggerDate was not set (was 01.01.0001)");
                }
                _triggerDate = value;
            }
        }

        public OneTimeTrigger(DateTime triggerDate)
        {
            TriggerDate = triggerDate;
        }

        public DateTime GetNextTrigger(DateTime dateTime)
        {
            if (dateTime.CompareTo(TriggerDate) > 0)
            {
                throw new NoNextTriggerException(TriggerDate);
            }
            return TriggerDate;
        }
    }
}