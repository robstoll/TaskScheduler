using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.trigger;

namespace ch.tutteli.taskscheduler.test.trigger
{
	public class MonthlyTriggerLSPTest : ARecuringTriggerTest
	{
		protected override ARecuringTrigger CreateTrigger(DateTime startDate, DateTime endDate)
		{
			var dayOfMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1 };
			MonthlyRecurrence recurrence = MonthlyRecurrence.CreateAllMonthReccurence(dayOfMonth, null);
			return new MonthlyTrigger(startDate, endDate, recurrence);
		}
	}
}