using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch.tutteli.taskscheduler.triggers
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
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentException("TriggerDate was not set (was DateTime.MinValue)");
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