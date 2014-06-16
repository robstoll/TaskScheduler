using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Triggers;

namespace CH.Tutteli.TaskScheduler.Triggers
{
	public class WeeklyTriggerLSPTest : ARecuringTriggerTest
	{
		protected override ARecuringTrigger CreateTrigger(DateTime startDate, DateTime endDate)
		{
			return WeeklyTrigger.CreateAllDayWeeklyTrigger(startDate, endDate, 1);
		}
	}
}