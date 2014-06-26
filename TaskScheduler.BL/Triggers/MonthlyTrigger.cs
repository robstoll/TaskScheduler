using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.Requests;

namespace CH.Tutteli.TaskScheduler.BL.Triggers
{
	public class MonthlyTrigger : ARecuringTrigger
	{
        /// <summary>
        /// Helper variable which denotes a date after the specified EndDate
        /// </summary>
		private DateTime _afterEndDate;

		public MonthlyRecurrence MonthlyRecurrence { get; set; }

		public MonthlyTrigger(DateTime startDate, DateTime endDate, MonthlyRecurrence monthlyRecurrence)
			: base(startDate, endDate)
		{
			MonthlyRecurrence = monthlyRecurrence;
		}

		protected override DateTime CalculateNextTrigger(DateTime dateTime)
		{
			_afterEndDate = EndDate.AddDays(1);
			DateTime nextTrigger = _afterEndDate;
			var adjustedDateTime = CheckAndAdjustTime(dateTime);

			var doesNotIncludeLeapYear = new DateTime(adjustedDateTime.Year, 3, 1).AddDays(1).Day != 29;


			nextTrigger = GetNextTriggerInSameMonth(adjustedDateTime);

			//if not in same month
			if (HasNotFoundATrigger(nextTrigger))
			{
				nextTrigger = GetNextTriggerStartingFromNextMonthAndInSameYear(adjustedDateTime);

				//if not in same year
				if (HasNotFoundATrigger(nextTrigger))
				{
					nextTrigger = GetNextTriggerStartingFromJanFirstNextYearUpToMonth(adjustedDateTime, dateTime, ref doesNotIncludeLeapYear);

					// if have not found a trigger yet, probably a leap year trigger
					if (HasNotFoundATrigger(nextTrigger)
						&& doesNotIncludeLeapYear
						&& MonthlyRecurrence.RecursOnDayOfMonth.Contains(EDayOfMonth.D29)
						&& MonthlyRecurrence.RecursOnMonth.Contains(EMonth.February))
					{
						nextTrigger = GetNextLeapYearTrigger(adjustedDateTime);
					}
				}
			}
			return nextTrigger;
		}

		private bool HasNotFoundATrigger(DateTime nextTrigger)
		{
			return nextTrigger.CompareTo(_afterEndDate) == 0;
		}

		private DateTime GetNextTriggerStartingFromJanFirstNextYearUpToMonth(DateTime adjustedDateTime, DateTime dateTime, ref bool doesNotIncludeLeapYear)
		{
			DateTime nextTrigger = _afterEndDate;
			adjustedDateTime = new DateTime(adjustedDateTime.Year + 1, 1, 1);
			adjustedDateTime = SetTimeToStartTime(adjustedDateTime);

			doesNotIncludeLeapYear &= new DateTime(adjustedDateTime.Year, 3, 1).AddDays(1).Day != 29;
			for (int i = 1; i <= dateTime.Month; ++i)
			{
				nextTrigger = GetNextTriggerInSameMonth(adjustedDateTime);
				if (nextTrigger.CompareTo(EndDate.AddDays(1)) != 0)
				{
					break;
				}
				adjustedDateTime = adjustedDateTime.AddMonths(1);
			}
			return nextTrigger;
		}

		private DateTime GetNextTriggerInSameMonth(DateTime adjustedDateTime)
		{
			DateTime nextTrigger = _afterEndDate;
			if (MonthlyRecurrence.RecursOnMonth.Contains((EMonth)adjustedDateTime.Month))
			{
				int lastDayOfMonth = new DateTime(adjustedDateTime.Year, adjustedDateTime.Month, 1).AddMonths(1).AddDays(-1).Day;
				for (int day = adjustedDateTime.Day; day <= lastDayOfMonth; ++day)
				{
					if (MonthlyRecurrence.RecursOnDayOfMonth.Contains((EDayOfMonth)day)
						|| (day == lastDayOfMonth && MonthlyRecurrence.RecursOnDayOfMonth.Contains(EDayOfMonth.Last))
					)
					{
						nextTrigger = adjustedDateTime.AddDays(day - adjustedDateTime.Day);
						break;
					}
					else
					{
						nextTrigger = CheckIfSpecialDay(adjustedDateTime, day);
						if (!HasNotFoundATrigger(nextTrigger))
						{
							break;
						}
					}
				}
			}
			return nextTrigger;
		}

		private DateTime CheckIfSpecialDay(DateTime dateTime, int day)
		{
			DateTime nextTrigger = _afterEndDate;
			var adjustedDateTime = dateTime.AddDays(day - dateTime.Day);

			if (day <= 7 && MonthlyRecurrence.RecursOnSpecialDayOfMonth.ContainsKey(EMonthlyOn.First))
			{
				if (MonthlyRecurrence.RecursOnSpecialDayOfMonth[EMonthlyOn.First].Contains(adjustedDateTime.DayOfWeek))
				{
					nextTrigger = adjustedDateTime;
				}
			}
			else if (day > 7 && day <= 14 && MonthlyRecurrence.RecursOnSpecialDayOfMonth.ContainsKey(EMonthlyOn.Second))
			{
				if (MonthlyRecurrence.RecursOnSpecialDayOfMonth[EMonthlyOn.Second].Contains(adjustedDateTime.DayOfWeek))
				{
					nextTrigger = adjustedDateTime;
				}
			}
			else if (day > 14 && day <= 21 && MonthlyRecurrence.RecursOnSpecialDayOfMonth.ContainsKey(EMonthlyOn.Third))
			{
				if (MonthlyRecurrence.RecursOnSpecialDayOfMonth[EMonthlyOn.Third].Contains(adjustedDateTime.DayOfWeek))
				{
					nextTrigger = adjustedDateTime;
				}
			}
			else if (day > 21 && day <= 28 && MonthlyRecurrence.RecursOnSpecialDayOfMonth.ContainsKey(EMonthlyOn.Fourth))
			{
				if (MonthlyRecurrence.RecursOnSpecialDayOfMonth[EMonthlyOn.Fourth].Contains(adjustedDateTime.DayOfWeek))
				{
					nextTrigger = adjustedDateTime;
				}
			}
			else if (MonthlyRecurrence.RecursOnSpecialDayOfMonth.ContainsKey(EMonthlyOn.Last))
			{
				int lastDayOfMonth = new DateTime(adjustedDateTime.Year, adjustedDateTime.Month, 1).AddMonths(1).AddDays(-1).Day;
				if (lastDayOfMonth - day < 7)
					if (MonthlyRecurrence.RecursOnSpecialDayOfMonth[EMonthlyOn.Last].Contains(adjustedDateTime.DayOfWeek))
					{
						nextTrigger = adjustedDateTime;
					}
			}
			return nextTrigger;
		}

		private DateTime GetNextTriggerStartingFromNextMonthAndInSameYear(DateTime adjustedDateTime)
		{
			DateTime nextTrigger = _afterEndDate;
			adjustedDateTime = new DateTime(adjustedDateTime.Year, adjustedDateTime.Month, 1).AddMonths(1);
			adjustedDateTime = SetTimeToStartTime(adjustedDateTime);
			for (int i = adjustedDateTime.Month; i <= 12; ++i)
			{
				nextTrigger = GetNextTriggerInSameMonth(adjustedDateTime);
				if (nextTrigger.CompareTo(EndDate.AddDays(1)) != 0)
				{
					break;
				}
				adjustedDateTime = adjustedDateTime.AddMonths(1);
			}
			return nextTrigger;
		}

		private DateTime GetNextLeapYearTrigger(DateTime adjustedDateTime)
		{
			var count = 1;
			DateTime potentialD29;
			do
			{
				potentialD29 = new DateTime(adjustedDateTime.Year + count, 3, 1).AddDays(-1);
				++count;
			} while (potentialD29.Day != 29);
			return SetTimeToStartTime(potentialD29);
		}
	}
}