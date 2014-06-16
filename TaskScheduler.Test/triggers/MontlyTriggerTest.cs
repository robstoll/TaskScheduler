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
	public class MontlyTriggerTest
	{
		[Test]
		public void GetNextTrigger_DxNextYMonthInSameYear_ReturnsDateAccordingly(
			[Values(1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
				20, 21, 22, 23, 24, 25, 26, 27, 28)] int dayOfMonth,
			[Values(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)] int plusMonth)
		{
			var startDate = new DateTime(2014, 1, 1, 12, 02, 01, 123);
			var month = startDate.Month + plusMonth;
			var months = new HashSet<EMonth>{
				(EMonth) month
			};
			var days = new HashSet<EDayOfMonth>{
				(EDayOfMonth) 	dayOfMonth		
			};
			var recurrence = new MonthlyRecurrence(months, days, null);

			var trigger = CreateMonthlyTrigger(startDate, startDate.AddYears(2), recurrence);
			var result = trigger.GetNextTrigger(startDate.AddSeconds(1));

			var nextTrigger = new DateTime(startDate.Year, month, dayOfMonth,
				startDate.Hour, startDate.Minute, startDate.Second, startDate.Millisecond);
			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		public void GetNextTrigger_D29OrD30NextYMonthInSameYear_ReturnsDateAccordingly(
		[Values(29, 30)] int dayOfMonth,
			[Values(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)] int plusMonth)
		{
			GetNextTrigger_DxNextYMonthInSameYear_ReturnsDateAccordingly(dayOfMonth, plusMonth);
		}

		public void GetNextTrigger_D31NextYMonthInSameYear_ReturnsDateAccordingly(
		[Values(1, 3, 5, 7, 8, 10, 12)] int plusMonth)
		{
			GetNextTrigger_DxNextYMonthInSameYear_ReturnsDateAccordingly(31, plusMonth);
		}

		[Test]
		public void GetNextTrigger_DxInSameMonthButNextYear_ReturnsDateAccordingly(
			[Values(1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
				20, 21, 22, 23, 24, 25, 26, 27, 28, 30, 31)] int dayOfMonth)
		{
			var startDate = new DateTime(2014, 1, 1, 12, 02, 01, 123);
			var month = startDate.Month;
			var months = new HashSet<EMonth>{
				(EMonth) month
			};
			var days = new HashSet<EDayOfMonth>{
				(EDayOfMonth) 	dayOfMonth		
			};
			var recurrence = new MonthlyRecurrence(months, days, null);

			var trigger = CreateMonthlyTrigger(startDate, startDate.AddYears(2), recurrence);
			var result = trigger.GetNextTrigger(startDate.AddDays(dayOfMonth));

			var nextTrigger = new DateTime(startDate.Year + 1, month, dayOfMonth,
				startDate.Hour, startDate.Minute, startDate.Second, startDate.Millisecond);
			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		[TestCase(1, 31)]
		[TestCase(2, 28)]
		[TestCase(3, 31)]
		[TestCase(4, 30)]
		[TestCase(5, 31)]
		[TestCase(6, 30)]
		[TestCase(7, 31)]
		[TestCase(8, 31)]
		[TestCase(9, 30)]
		[TestCase(10, 31)]
		[TestCase(11, 30)]
		[TestCase(12, 31)]
		public void GetNextTrigger_LastInMonth_ReturnsLastInMonth(int month, int lastDay)
		{
			var startDate = new DateTime(2014, 1, 1, 12, 02, 01, 123);
			var months = new HashSet<EMonth>{
				(EMonth) month
			};
			var days = new HashSet<EDayOfMonth> { EDayOfMonth.Last };
			var recurrence = new MonthlyRecurrence(months, days, null);

			var trigger = CreateMonthlyTrigger(startDate, startDate.AddYears(2), recurrence);
			var result = trigger.GetNextTrigger(startDate.AddSeconds(1));

			var nextTrigger = new DateTime(startDate.Year, month, lastDay,
				startDate.Hour, startDate.Minute, startDate.Second, startDate.Millisecond);
			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		[Test]
		public void GetNextTrigger_29FebruaryNotLeapYear_ReturnNextLeapYear()
		{
			var startDate = new DateTime(2014, 1, 1, 12, 02, 01, 123);
			var months = new HashSet<EMonth> { EMonth.February };
			var days = new HashSet<EDayOfMonth> { EDayOfMonth.D29 };
			var recurrence = new MonthlyRecurrence(months, days, null);

			var trigger = CreateMonthlyTrigger(startDate, startDate.AddYears(4), recurrence);
			var result = trigger.GetNextTrigger(startDate.AddSeconds(1));

			var nextTrigger = new DateTime(2016, 2, 29,
				startDate.Hour, startDate.Minute, startDate.Second, startDate.Millisecond);
			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		[Test]
		public void GetNextTrigger_29FebruaryAndLeapYear_ReturnSameYear()
		{
			var startDate = new DateTime(2012, 1, 1, 12, 02, 01, 123);
			var months = new HashSet<EMonth> { EMonth.February };
			var days = new HashSet<EDayOfMonth> { EDayOfMonth.D29 };
			var recurrence = new MonthlyRecurrence(months, days, null);

			var trigger = CreateMonthlyTrigger(startDate, startDate.AddYears(2), recurrence);
			var result = trigger.GetNextTrigger(startDate.AddSeconds(1));

			var nextTrigger = new DateTime(2012, 2, 29,
				startDate.Hour, startDate.Minute, startDate.Second, startDate.Millisecond);
			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		[TestCase(EMonthlyOn.First, DayOfWeek.Thursday, 1)]
		[TestCase(EMonthlyOn.First, DayOfWeek.Friday, 2)]
		[TestCase(EMonthlyOn.First, DayOfWeek.Saturday, 3)]
		[TestCase(EMonthlyOn.First, DayOfWeek.Sunday, 4)]
		[TestCase(EMonthlyOn.First, DayOfWeek.Monday, 5)]
		[TestCase(EMonthlyOn.First, DayOfWeek.Tuesday, 6)]
		[TestCase(EMonthlyOn.First, DayOfWeek.Wednesday, 7)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Thursday, 8)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Friday, 9)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Saturday, 10)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Sunday, 11)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Monday, 12)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Tuesday, 13)]
		[TestCase(EMonthlyOn.Second, DayOfWeek.Wednesday, 14)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Thursday, 15)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Friday, 16)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Saturday, 17)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Sunday, 18)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Monday, 19)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Tuesday, 20)]
		[TestCase(EMonthlyOn.Third, DayOfWeek.Wednesday, 21)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Thursday, 22)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Friday, 23)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Saturday, 24)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Sunday, 25)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Monday, 26)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Tuesday, 27)]
		[TestCase(EMonthlyOn.Fourth, DayOfWeek.Wednesday, 28)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Thursday, 29)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Friday, 30)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Saturday, 31)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Sunday, 25)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Monday, 26)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Tuesday, 27)]
		[TestCase(EMonthlyOn.Last, DayOfWeek.Wednesday, 28)]
		public void GetNextTrigger_MonthlyOnTheXDayOfWeekYOfMay2014_ReturnZOfMay2014(EMonthlyOn monthlyOn, DayOfWeek dayOfWeek, int expectedDay)
		{
			var startDate = new DateTime(2014, 4, 1, 12, 02, 01, 123);
			var months = new HashSet<EMonth> { EMonth.May };
			var specialDays = new Dictionary<EMonthlyOn, IList<DayOfWeek>>{
				{monthlyOn, new List<DayOfWeek>{dayOfWeek}}
			};
			var recurrence = new MonthlyRecurrence(months, null, specialDays);

			var trigger = CreateMonthlyTrigger(startDate, startDate.AddYears(2), recurrence);
			var result = trigger.GetNextTrigger(startDate.AddSeconds(1));

			var nextTrigger = new DateTime(2014, 5, expectedDay,
				startDate.Hour, startDate.Minute, startDate.Second, startDate.Millisecond);
			Assert.That(result, IsEqual.WithinOneMillisecond(nextTrigger));
		}

		protected virtual MonthlyTrigger CreateMonthlyTrigger(DateTime startDate, DateTime endDate, MonthlyRecurrence monthlyRecurrence)
		{
			return new MonthlyTrigger(startDate, endDate, monthlyRecurrence);
		}
	}
}