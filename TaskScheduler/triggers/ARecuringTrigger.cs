using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CH.Tutteli.TaskScheduler.Triggers
{
	public abstract class ARecuringTrigger : ITrigger
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public ARecuringTrigger(DateTime startDate, DateTime endDate)
		{
			if (startDate.CompareTo(endDate) > 0)
			{
				throw new ArgumentException("startDate cannot be after the endDate");
			}

			StartDate = TruncateToMilliseconds(startDate);
			EndDate = TruncateToMilliseconds(endDate);
		}

		public DateTime GetNextTrigger(DateTime dateTime)
		{
			var date = TruncateToMilliseconds(dateTime);
			if (date.CompareTo(StartDate) <= 0)
			{
				return StartDate;
			}

			var nextTrigger = CalculateNextTrigger(date);

			if (nextTrigger.CompareTo(EndDate) > 0)
			{
				throw new NoNextTriggerException(EndDate);
			}

			return nextTrigger;
		}

		protected abstract DateTime CalculateNextTrigger(DateTime dateTime);

		protected DateTime TruncateToMilliseconds(DateTime dateTime)
		{
			return dateTime.AddTicks(-dateTime.Ticks % 10000000).AddMilliseconds(dateTime.Millisecond);
		}

		protected DateTime SetTimeToStartTime(DateTime dateTime) {
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, StartDate.Hour, StartDate.Minute, StartDate.Second, StartDate.Millisecond);
		}

		protected DateTime CheckAndAdjustTime(DateTime dateTime)
		{
			DateTime adjustedDateTime;
			var compare = dateTime.TimeOfDay.CompareTo(StartDate.TimeOfDay);
			if (compare > 0)
			{
				adjustedDateTime = SetTimeToStartTime(dateTime).AddDays(1);
			}
			else if (compare < 0)
			{
				adjustedDateTime = SetTimeToStartTime(dateTime);
			}
			else
			{
				adjustedDateTime = dateTime;
			}
			return adjustedDateTime;
		}
	}
}