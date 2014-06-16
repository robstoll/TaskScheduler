using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.Triggers;
using NUnit.Framework;

namespace CH.Tutteli.TaskScheduler.Triggers
{
	[TestFixture]
	public class OneTimeTriggerTest
	{
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Construct_TriggerDateNotSet_ThrowArgumentException()
        {
            var result = CreateOneTimeTrigger(DateTime.MinValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTriggerDate_DateTimeMinValue_ThrowArgumentException()
        {
            var trigger = CreateOneTimeTrigger(DateTime.Now);

            trigger.TriggerDate = DateTime.MinValue;
        }

		[Test]
		public void GetNextTrigger_DateLongAgoBeforeTriggerDate_ReturnTriggerDate()
		{
			var triggerDate = DateTime.Now;

			var dailyTrigger = CreateOneTimeTrigger(triggerDate);
			var result = dailyTrigger.GetNextTrigger(triggerDate.AddDays(-10));

			Assert.That(result, IsEqual.WithinOneMillisecond(triggerDate));
		}

		[Test]
		public void GetNextTrigger_DateOneHourBeforeTriggerDate_ReturnTriggerDate()
		{
			var triggerDate = DateTime.Now;

			var dailyTrigger = CreateOneTimeTrigger(triggerDate);
			var result = dailyTrigger.GetNextTrigger(triggerDate.AddHours(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(triggerDate));
		}

		[Test]
		public void GetNextTrigger_DateOneSecondBeforeTriggerDate_ReturnTriggerDate()
		{
			var triggerDate = DateTime.Now;

			var dailyTrigger = CreateOneTimeTrigger(triggerDate);
			var result = dailyTrigger.GetNextTrigger(triggerDate.AddSeconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(triggerDate));
		}

		[Test]
		public void GetNextTrigger_DateOneMillisecondBeforeTriggerDate_ReturnTriggerDate()
		{
			var triggerDate = DateTime.Now;

			var dailyTrigger = CreateOneTimeTrigger(triggerDate);
			var result = dailyTrigger.GetNextTrigger(triggerDate.AddMilliseconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(triggerDate));
		}

		[Test]
		public void GetNextTrigger_DateEqualsTriggerDate_ReturnTriggerDate()
		{
			var triggerDate = DateTime.Now;

			var dailyTrigger = CreateOneTimeTrigger(triggerDate);
			var result = dailyTrigger.GetNextTrigger(triggerDate);

			Assert.That(result, IsEqual.WithinOneMillisecond(triggerDate));
		}

		[Test]
		public void GetNextTrigger_DateAfterTriggerDate_ThrowNoNextTriggerException()
		{
			var triggerDate = DateTime.Now;

			var dailyTrigger = CreateOneTimeTrigger(triggerDate);
			try
			{
				var result = dailyTrigger.GetNextTrigger(triggerDate.AddMilliseconds(1));
				Assert.Fail("no exception occured even though date is after trigger date");
			}
			catch (NoNextTriggerException ex)
			{
				Assert.That(ex.EndDate, IsEqual.WithinOneMillisecond(triggerDate));
			}
		}

		protected virtual OneTimeTrigger CreateOneTimeTrigger(DateTime triggerDate)
		{
			return new OneTimeTrigger(triggerDate);
		}
	}
}