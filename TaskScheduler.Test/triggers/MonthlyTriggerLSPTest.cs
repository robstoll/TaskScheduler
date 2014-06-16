using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Triggers;

namespace CH.Tutteli.TaskScheduler.Triggers
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