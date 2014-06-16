using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.Triggers;
using NUnit.Framework;

namespace CH.Tutteli.TaskScheduler.Triggers
{
	public class DummyTrigger : ARecuringTrigger
	{

		public DummyTrigger(DateTime startDate, DateTime endDate)
			: base(startDate, endDate)
		{
		}

		protected override DateTime CalculateNextTrigger(DateTime dateTime)
		{
			return StartDate;
		}
	}

	[TestFixture]
	public class ARecuringTriggerTest
	{

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_StartDateAfterEndDate_ThrowArgumentException()
		{
			var startDate = DateTime.Now;
			CreateTrigger(startDate.AddDays(1), startDate);
		}

		[Test]
		public void GetNextTrigger_DateLongBeforeStartDate_ReturnStartDate()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateTrigger(startDate, startDate.AddDays(1));
			var result = dailyTrigger.GetNextTrigger(startDate.AddDays(-10));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate));
		}

		[Test]
		public void GetNextTrigger_DateOneHourBeforeStartDate_ReturnStartDate()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateTrigger(startDate, startDate.AddDays(1));
			var result = dailyTrigger.GetNextTrigger(startDate.AddHours(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate));
		}

		[Test]
		public void GetNextTrigger_DateOneMinuteBeforeStartDate_ReturnStartDate()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateTrigger(startDate, startDate.AddDays(1));
			var result = dailyTrigger.GetNextTrigger(startDate.AddMinutes(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate));
		}

		[Test]
		public void GetNextTrigger_DateOneSecondBeforeStartDate_ReturnStartDate()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateTrigger(startDate, startDate.AddDays(1));
			var result = dailyTrigger.GetNextTrigger(startDate.AddSeconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate));
		}

		[Test]
		public void GetNextTrigger_DateOneMillisecondBeforeStartDate_ReturnStartDate()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateTrigger(startDate, startDate.AddDays(1));
			var result = dailyTrigger.GetNextTrigger(startDate.AddMilliseconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate));
		}

		[Test]
		public void GetNextTrigger_DateEqualsStartDate_ReturnStartDate()
		{
			var startDate = DateTime.Now;

			var dailyTrigger = CreateTrigger(startDate, startDate.AddDays(1));
			var result = dailyTrigger.GetNextTrigger(startDate);

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate));
		}

		protected virtual ARecuringTrigger CreateTrigger(DateTime startDate, DateTime endDate)
		{
			return new DummyTrigger(startDate, endDate);
		}
	}
}