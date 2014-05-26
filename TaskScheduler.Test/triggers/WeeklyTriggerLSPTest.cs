using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.triggers;

namespace ch.tutteli.taskscheduler.triggers
{
	public class WeeklyTriggerLSPTest : ARecuringTriggerTest
	{
		protected override ARecuringTrigger CreateTrigger(DateTime startDate, DateTime endDate)
		{
			return WeeklyTrigger.CreateAllDayWeeklyTrigger(startDate, endDate, 1);
		}
	}
}