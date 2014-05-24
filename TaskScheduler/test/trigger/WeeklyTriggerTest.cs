using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.test.utils;
using ch.tutteli.taskscheduler.trigger;
using NUnit.Framework;

namespace ch.tutteli.taskscheduler.test.trigger
{
	[TestFixture]
	public class WeeklyTriggerTest
	{

		private ISet<DayOfWeek> allDay = new HashSet<DayOfWeek>(){
			DayOfWeek.Monday,
			DayOfWeek.Tuesday,
			DayOfWeek.Wednesday,
			DayOfWeek.Thursday,
			DayOfWeek.Friday,
			DayOfWeek.Saturday,
			DayOfWeek.Sunday
		};


		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_IllegalRecurrence_ThrowArgumentException([Values(0, -1, -2, -4)] int recursEveryXWeeks)
		{
			var startDate = DateTime.Now;
			CreateWeeklyTrigger(startDate, startDate.AddDays(1), recursEveryXWeeks, allDay);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_DayOfWeekSetIsNull_ThrowArgumentException()
		{
			var startDate = DateTime.Now;
			CreateWeeklyTrigger(startDate, startDate.AddDays(1), 1, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_DayOfWeekSetIsEmpty_ThrowArgumentException()
		{
			var startDate = DateTime.Now;
			CreateWeeklyTrigger(startDate, startDate.AddDays(1), 1, new HashSet<DayOfWeek>());
		}

		[Test]
		public void GetNextTrigger_OneSecondBeforeNextTrigger_ReturnNextTrigger()
		{
			var tuesday = CreateDate(DayOfWeek.Tuesday);

			var weeklyTrigger = CreateWeeklyTrigger(tuesday, tuesday.AddDays(8), 1, new HashSet<DayOfWeek> { DayOfWeek.Wednesday });
			var result = weeklyTrigger.GetNextTrigger(tuesday.AddDays(1).AddSeconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(tuesday.AddDays(1)));
		}

		[Test]
		public void GetNextTrigger_OneMillisecondBeforeNextTrigger_ReturnNextTrigger()
		{
			var tuesday = CreateDate(DayOfWeek.Tuesday);

			var weeklyTrigger = CreateWeeklyTrigger(tuesday, tuesday.AddDays(8), 1, new HashSet<DayOfWeek> { DayOfWeek.Wednesday });
			var result = weeklyTrigger.GetNextTrigger(tuesday.AddDays(1).AddMilliseconds(-1));

			Assert.That(result, IsEqual.WithinOneMillisecond(tuesday.AddDays(1)));
		}

		[Test]
		public void GetNextTrigger_XDaysAfterStartDateAndAllDay_ReturnStartDatePlusXDays([Values(1,2,3,4,5)] int days) { 
			var startDate = DateTime.Now;

			var weeklyTrigger = CreateWeeklyTrigger(startDate, startDate.AddDays(days*2).AddSeconds(1), 1, allDay);
			var result = weeklyTrigger.GetNextTrigger(startDate.AddDays(days));

			Assert.That(result, IsEqual.WithinOneMillisecond(startDate.AddDays(days)));
		}

		[Test]
		public void GetNextTrigger_MondayAndNextIsGivenDayOfWeekWithinWeek_ReturnGivenDayOfWeek([Values(
			DayOfWeek.Tuesday, 
			DayOfWeek.Wednesday, 
			DayOfWeek.Thursday, 
			DayOfWeek.Friday, 
			DayOfWeek.Saturday,
			DayOfWeek.Sunday)] DayOfWeek dayOfWeek)
		{
			var monday = CreateDate(DayOfWeek.Monday);

			var weeklyTrigger = CreateWeeklyTrigger(monday, monday.AddDays(8) , 1, new HashSet<DayOfWeek> { dayOfWeek });
			var result = weeklyTrigger.GetNextTrigger(monday.AddSeconds(1));

			int addDays = dayOfWeek != DayOfWeek.Sunday ? (int)dayOfWeek-1 : 6;
			Assert.That(result, IsEqual.WithinOneMillisecond(monday.AddDays(addDays)));
		}

		[Test]
		public void GetNextTrigger_MondayAndNextIsMonday_ReturnMondayInAWeek()
		{
			var monday = CreateDate(DayOfWeek.Monday);

			var weeklyTrigger = CreateWeeklyTrigger(monday, monday.AddDays(8), 1, new HashSet<DayOfWeek> { DayOfWeek.Monday });
			var result = weeklyTrigger.GetNextTrigger(monday.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(monday.AddDays(7)));
		}

		[Test]
		public void GetNextTrigger_TuesdayAndNextIsMonday_ReturnMondayInAWeek()
		{
			var tuesday = CreateDate(DayOfWeek.Tuesday);

			var weeklyTrigger = CreateWeeklyTrigger(tuesday, tuesday.AddDays(8), 1, new HashSet<DayOfWeek> { DayOfWeek.Monday });
			var result = weeklyTrigger.GetNextTrigger(tuesday.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(tuesday.AddDays(6)));
		}

		[Test]
		public void GetNextTrigger_TuesdayTimeAfterStartDateAndTriggersAreMoAndTue_ReturnMondayInAWeek()
		{
			var tuesday = CreateDate(DayOfWeek.Tuesday);

			var weeklyTrigger = CreateWeeklyTrigger(tuesday.AddDays(-1), tuesday.AddDays(8), 1, 
				new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday });
			var result = weeklyTrigger.GetNextTrigger(tuesday.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(tuesday.AddDays(6)));
		}

		[Test]
		public void GetNextTrigger_SundayTimeAfterStartDateAndTriggersTueAndSunRecursAll2Weeks_ReturnTuesdayIn9Days()
		{
			var sunday = CreateDate(DayOfWeek.Sunday);

			var weeklyTrigger = CreateWeeklyTrigger(sunday.AddDays(-1), sunday.AddDays(10), 2,
				new HashSet<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Sunday });
			var result = weeklyTrigger.GetNextTrigger(sunday.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(sunday.AddDays(2).AddDays(7)));
		}	

		[Test]
		public void GetNextTrigger_TuesdayTimeAfterStartDateAndTriggerTueRecursAllXWeeks_ReturnTuesdayInXWeeks([Values(2,3,4,5)] int recursEveryXWeeks)
		{
			var tuesday = CreateDate(DayOfWeek.Tuesday);

			var weeklyTrigger = CreateWeeklyTrigger(tuesday.AddDays(-1), tuesday.AddDays(recursEveryXWeeks*8), recursEveryXWeeks, new HashSet<DayOfWeek> {  DayOfWeek.Tuesday });
			var result = weeklyTrigger.GetNextTrigger(tuesday.AddSeconds(1));

			Assert.That(result, IsEqual.WithinOneMillisecond(tuesday.AddDays(7*recursEveryXWeeks)));
		}			

		protected virtual WeeklyTrigger CreateWeeklyTrigger(DateTime startDate,DateTime endDate, int recursEveryXWeeks,ISet<DayOfWeek> triggersWhenDayOfWeek)
		{
	 		return new WeeklyTrigger(startDate, endDate, recursEveryXWeeks, triggersWhenDayOfWeek);
		}

		private DateTime CreateDate(DayOfWeek dayOfWeek)
		{
			var now = DateTime.Now;
			return now.AddDays(-((int)now.DayOfWeek - (int)dayOfWeek));
		}
	}

}