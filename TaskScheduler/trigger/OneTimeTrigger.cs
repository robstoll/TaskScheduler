using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch.tutteli.taskscheduler.trigger
{
	public class OneTimeTrigger : ITrigger
	{
		public DateTime TriggerDate { get; set; }

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