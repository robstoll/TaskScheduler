using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.triggers;
using NUnit.Framework;

namespace ch.tutteli.taskscheduler.triggers
{
	[TestFixture]
	public class MonthlyReccurenceTest
	{

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_MonthSetIsNull_ThrowArgumentException()
		{
			var dayOfTheMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1 };
			var specialDays = new Dictionary<EMonthlyOn, IList<DayOfWeek>> {
				{ EMonthlyOn.First, new List<DayOfWeek>{DayOfWeek.Monday } }
			};
			CreateMonthlyRecurrence(null, dayOfTheMonth, specialDays);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_MonthSetIsEmpty_ThrowArgumentException()
		{
			var dayOfTheMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1 };
			var specialDays = new Dictionary<EMonthlyOn, IList<DayOfWeek>> {
				{ EMonthlyOn.First, new List<DayOfWeek>{DayOfWeek.Monday } }
			};
			CreateMonthlyRecurrence(new HashSet<EMonth>(), dayOfTheMonth, specialDays);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_DayAndSpecialDayAreBothEmpty_ThrowArgumentException()
		{
			CreateAllMonthRecurrence(new HashSet<EDayOfMonth>(), new Dictionary<EMonthlyOn, IList<DayOfWeek>>());
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_DayIsEmptyAndSpecialDayIsNull_ThrowArgumentException()
		{
			CreateAllMonthRecurrence(new HashSet<EDayOfMonth>(), null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_DayIsNullAndSpecialDayIsEmpty_ThrowArgumentException()
		{
			CreateAllMonthRecurrence(null, new Dictionary<EMonthlyOn, IList<DayOfWeek>>());
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_DayAndSpecialDayAreBothNull_ThrowArgumentException()
		{
			CreateAllMonthRecurrence(null, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_FebruaryAnd30OfMonth_ThrowArgumentException()
		{
			var dayOfTheMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D30 };
			CreateMonthlyRecurrence(new HashSet<EMonth> { EMonth.February }, dayOfTheMonth, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_FebruaryAnd31OfMonth_ThrowArgumentException()
		{
			var dayOfTheMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D31 };
			CreateMonthlyRecurrence(new HashSet<EMonth> { EMonth.February }, dayOfTheMonth, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Construct_30DayMonthsAnd31OfMonth_ThrowArgumentException([ValueSource("GetMonthsWith30OrLessDays")] ISet<EMonth> months)
		{
			var dayOfTheMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D31 };
			CreateMonthlyRecurrence(months, dayOfTheMonth, null);
		}

		[Test]
		public void Construct_DayOfMonthSetIsNull_IsEmptySetAfterConstruct()
		{
			var specialDays = new Dictionary<EMonthlyOn, IList<DayOfWeek>> {
			 { EMonthlyOn.First, new List<DayOfWeek>{ DayOfWeek.Monday } }
			 };

			var result = CreateAllMonthRecurrence(null, specialDays);

			Assert.That(result.RecursOnDayOfMonth.Count, Is.EqualTo(0));
		}

		[Test]
		public void Construct_SpecialDayOfMonthSetIsNull_IsEmptySetAfterConstruct()
		{
			var specialDays = new Dictionary<EMonthlyOn, DayOfWeek> { { EMonthlyOn.First, DayOfWeek.Monday } };

			var result = CreateAllMonthRecurrence(new HashSet<EDayOfMonth> { EDayOfMonth.D1 }, null);

			Assert.That(result.RecursOnSpecialDayOfMonth.Count, Is.EqualTo(0));
		}

		private IEnumerable<ISet<EMonth>> GetMonthsWith30OrLessDays()
		{
			return new ISet<EMonth>[]{
				new HashSet<EMonth>{EMonth.February},
				new HashSet<EMonth>{EMonth.April},
				new HashSet<EMonth>{EMonth.June},
				new HashSet<EMonth>{EMonth.September},
				new HashSet<EMonth>{EMonth.November},
				new HashSet<EMonth>{EMonth.February,EMonth.April},
				new HashSet<EMonth>{EMonth.February,EMonth.April,EMonth.June},
				new HashSet<EMonth>{EMonth.February,EMonth.April,EMonth.June,EMonth.September},
				new HashSet<EMonth>{EMonth.February,EMonth.April,EMonth.June,EMonth.September,EMonth.November},	
			};
		}

		[Test]
		public void CreateAllMonthReccurence_Standard_RecursOnMonthContainsAllMonths()
		{
			var dayOfTheMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1 };
			var specialDays = new Dictionary<EMonthlyOn, IList<DayOfWeek>> {
				{ EMonthlyOn.First, new List<DayOfWeek>{DayOfWeek.Monday } }
			};
			var monthlyRecurrence = CreateAllMonthRecurrence(dayOfTheMonth, specialDays);
			Assert.That(monthlyRecurrence.RecursOnMonth.Count, Is.EqualTo(12));
		}

		protected virtual MonthlyRecurrence CreateMonthlyRecurrence(
			ISet<EMonth> recursOnMonth,
			ISet<EDayOfMonth> recursOnDayOfTheMonth,
			IDictionary<EMonthlyOn, IList<DayOfWeek>> recursOnSpecialDayOfMonth)
		{
			return new MonthlyRecurrence(recursOnMonth, recursOnDayOfTheMonth, recursOnSpecialDayOfMonth);
		}

		protected virtual MonthlyRecurrence CreateAllMonthRecurrence(
		ISet<EDayOfMonth> recursOnDayOfTheMonth,
		IDictionary<EMonthlyOn, IList<DayOfWeek>> recursOnSpecialDayOfMonth)
		{
			return MonthlyRecurrence.CreateAllMonthReccurence(recursOnDayOfTheMonth, recursOnSpecialDayOfMonth);
		}

	}
}