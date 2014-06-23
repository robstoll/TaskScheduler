using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Requests;

namespace CH.Tutteli.TaskScheduler.BL.Triggers
{
	public class MonthlyRecurrence
	{

		private ISet<EMonth> _recursOnMonth;
		public ISet<EMonth> RecursOnMonth
		{
			get { return _recursOnMonth; }
			set
			{
				if (value == null || value.Count == 0)
				{
                    throw new ArgumentException("RecursOnMonth was null or empty, at least one month has to be specified otherwise it would never trigger");
				}
				_recursOnMonth = value;
			}
		}

		public ISet<EDayOfMonth> RecursOnDayOfMonth { get; private set; }
		/// <summary>
		/// Allowes to define special days like every first Monday of the month etc.
		/// </summary>
		public IDictionary<EMonthlyOn, IList<DayOfWeek>> RecursOnSpecialDayOfMonth { get; private set; }

		public static MonthlyRecurrence CreateAllMonthReccurence(
			ISet<EDayOfMonth> recursOnDayOfMonth,
			IDictionary<EMonthlyOn, IList<DayOfWeek>> recursOnSpecialDayOfMonth)
		{
			ISet<EMonth> recursOnMonth = new HashSet<EMonth>(){
				EMonth.January,
				EMonth.February,
				EMonth.March,
				EMonth.April,
				EMonth.May,
				EMonth.June,
				EMonth.July,
				EMonth.August,
				EMonth.September,
				EMonth.October,
				EMonth.November,
				EMonth.December
			};
			return new MonthlyRecurrence(recursOnMonth, recursOnDayOfMonth, recursOnSpecialDayOfMonth);
		}

		public MonthlyRecurrence(ISet<EMonth> recursOnMonth,
			ISet<EDayOfMonth> recursOnDayOfMonth,
			IDictionary<EMonthlyOn, IList<DayOfWeek>> recursOnSpecialDayOfMonth)
		{
			if ((recursOnDayOfMonth == null || recursOnDayOfMonth.Count == 0)
				&& (recursOnSpecialDayOfMonth == null || recursOnSpecialDayOfMonth.Count == 0))
			{
				throw new ArgumentException("at least one day or special day has to be specified where the reccurence happens.");
			}
			RecursOnMonth = recursOnMonth;
			RecursOnDayOfMonth = recursOnDayOfMonth != null ? recursOnDayOfMonth : new HashSet<EDayOfMonth>();
			RecursOnSpecialDayOfMonth = recursOnSpecialDayOfMonth == null ? new Dictionary<EMonthlyOn, IList<DayOfWeek>>() : recursOnSpecialDayOfMonth;

			CheckDayOfMonthNotOutOfMonths();
		}

		private void CheckDayOfMonthNotOutOfMonths()
		{
			if (RecursOnMonth.Count == 1
				&& RecursOnMonth.Contains(EMonth.February))
			{
				if (RecursOnDayOfMonth.Contains(EDayOfMonth.D30))
				{
					throw new ArgumentException("Will never recur on 30th since only february is defined");
				}
				if (RecursOnDayOfMonth.Contains(EDayOfMonth.D31))
				{
					throw new ArgumentException("Will never recur on 31st since only february is defined");
				}
			}

			if (RecursOnDayOfMonth.Contains(EDayOfMonth.D31))
			{
				bool no31DaysMonth = true;
				foreach (var month in RecursOnMonth)
				{
					switch (month)
					{
						case EMonth.January:
						case EMonth.March:
						case EMonth.May:
						case EMonth.July:
						case EMonth.August:
						case EMonth.October:
						case EMonth.December:
							no31DaysMonth = false;
							break;
					}
					if (!no31DaysMonth)
					{
						break;
					}
				}
				if (no31DaysMonth)
				{
					throw new ArgumentException("Will never recur on 31st since only months with 30 or less days are defined");
				}
			}
		}
	}
}