using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using NUnit.Framework;


namespace CH.Tutteli.TaskScheduler.Test.BL.Triggers
{
	[TestFixture]
	public class DailyTriggerTest
	{
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_IllegalRecurrence_ThrowArgumentException([Values(0, -1, -2, -4)] int recursEveryXDays)
		{
			var startDate = DateTime.Now;
			CreateDailyTrigger(startDate, startDate.AddDays(1), recursEveryXDays);
		}

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetRecursEveryXDays_IllegalRecurrence_ThrowArgumentException([Values(0, -1, -2, -4)] int recursEveryXDays)
        {
            var startDate = DateTime.Now;
            var trigger = CreateDailyTrigger(startDate, startDate.AddDays(1), 10);

            trigger.RecursEveryXDays = recursEveryXDays;
        }

		[Test]
		public void GetNextTrigger_OneMillisecondAfterStartDate_ReturnStartDatePlusOneDay()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateDailyTrigger(startDate, startDate.AddDays(1), 1);
			var result = dailyTrigger.GetNextTrigger(startDate.AddMilliseconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate.AddDays(1)));
		}

		[Test]
		public void GetNextTrigger_OneSecondAfterStartDate_ReturnStartDatePlusOneDay()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateDailyTrigger(startDate, startDate.AddDays(1), 1);
			var result = dailyTrigger.GetNextTrigger(startDate.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate.AddDays(1)));
		}

		[Test]
		public void GetNextTrigger_OneSecondBeforeGetNextTrigger_ReturnStartDatePlusOneDay()
		{
			var startDate = DateTime.Now;
			var nextTrigger = startDate.AddDays(1);

			var dailyTrigger = CreateDailyTrigger(startDate, startDate.AddDays(2), 1);
			var result = dailyTrigger.GetNextTrigger(nextTrigger.AddSeconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		[Test]
		public void GetNextTrigger_OneMillisecondBeforeGetNextTrigger_ReturnStartDatePlusOneDay()
		{
			var startDate = DateTime.Now;
			var nextTrigger = startDate.AddDays(1);

			var dailyTrigger = CreateDailyTrigger(startDate, startDate.AddDays(2), 1);
			var result = dailyTrigger.GetNextTrigger(nextTrigger.AddMilliseconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}


		[Test]
		public void GetNextTrigger_OneMillisecondBeforeEndDateAndEndTimeEqualsStartTime_ReturnEndDate()
		{
			var startDate = DateTime.Now;
			var endDate = startDate.AddDays(2);

			var dailyTrigger = CreateDailyTrigger(startDate, endDate, 1);
			var result = dailyTrigger.GetNextTrigger(endDate.AddMilliseconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(endDate));
		}

		[Test]
		public void GetNextTrigger_OneMillisecondBeforeEndDateAndEndTimeAfterStartTime_ReturnEndDate()
		{
			var startDate = DateTime.Now;
			var lastTrigger = startDate.AddDays(2);
			var endDate = lastTrigger.AddMilliseconds(1);

			var dailyTrigger = CreateDailyTrigger(startDate, endDate, 1);
			var result = dailyTrigger.GetNextTrigger(endDate.AddMilliseconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(lastTrigger));
		}

		[Test]
		public void GetNextTrigger_OneMillisecondBeforeEndDateAndEndTimeBeforeStartTime_ThrowNoNextTriggerException()
		{
			var startDate = DateTime.Now;
			var endDate = startDate.AddDays(1).AddMilliseconds(-1);

			var dailyTrigger = CreateDailyTrigger(startDate, endDate, 1);
			try
			{
				var result = dailyTrigger.GetNextTrigger(endDate.AddMilliseconds(-1));
				Assert.Fail("no exception occured even though there is no next trigger date");
			}
			catch (NoNextTriggerException ex)
			{
				Assert.That(ex.EndDate, IsEqual.WithinOneMillisecond(endDate));
			}
		}

		[Test]
		public void GetNextTrigger_OneMillisecondAfterEndDateAndEndTimeEqualsStartTime_ThrowNoNextTriggerException()
		{
			var startDate = DateTime.Now;
			var endDate = startDate.AddDays(1);

			var dailyTrigger = CreateDailyTrigger(startDate, endDate, 1);
			try
			{
				var result = dailyTrigger.GetNextTrigger(endDate.AddMilliseconds(1));
				Assert.Fail("no exception occured even though there is no next trigger date");
			}
			catch (NoNextTriggerException ex)
			{
				Assert.That(ex.EndDate, IsEqual.WithinOneMillisecond(endDate));
			}
		}

		[Test]
		public void GetNextTrigger_RecurrenceXDays_ReturnStartDatePlusXDays([Values(2, 3, 4, 5)] int recursEveryXDay)
		{
			var startDate = DateTime.Now;
			var dailyTrigger = CreateDailyTrigger(startDate, startDate.AddDays(recursEveryXDay * 2), recursEveryXDay);

			var result = dailyTrigger.GetNextTrigger(startDate.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate.AddDays(recursEveryXDay)));
		}

		protected virtual DailyTrigger CreateDailyTrigger(DateTime startDate, DateTime endDate, int recursEveryXDays)
		{
			return new DailyTrigger(startDate, endDate, recursEveryXDays);
		}

	}
}