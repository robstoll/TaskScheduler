using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.triggers;
using NUnit.Framework;

namespace ch.tutteli.taskscheduler.triggers
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