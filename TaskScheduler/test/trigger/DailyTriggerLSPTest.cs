using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.trigger;
using NUnit.Framework;

namespace ch.tutteli.taskscheduler.test.trigger
{
	[TestFixture]
	public class DailyTriggerLSPTest : ARecuringTriggerTest
	{

		protected override ARecuringTrigger CreateTrigger(DateTime startDate, DateTime endDate)
		{
			return new DailyTrigger(startDate, endDate, 1);
		}
	}
}