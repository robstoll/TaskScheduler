using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.trigger;

namespace ch.tutteli.taskscheduler.test.trigger
{
	public class WeeklyTriggerLSPTest : ARecuringTriggerTest
	{
		protected override ARecuringTrigger CreateTrigger(DateTime startDate, DateTime endDate)
		{
			return WeeklyTrigger.CreateAllDayWeeklyTrigger(startDate, endDate, 1);
		}
	}
}