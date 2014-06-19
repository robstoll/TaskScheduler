using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using NUnit.Framework;

namespace CH.Tutteli.TaskScheduler.Test.BL.Triggers
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