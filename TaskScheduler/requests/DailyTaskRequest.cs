using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler.requests
{
	public class DailyTaskRequest : AReccuringTaskRequest
	{

		public int RecursEveryXDays { get; set; }

		public ISet<DayOfWeek> TriggerWhenDayOfWeek { get; set; }

		public DailyTaskRequest() { }

		public DailyTaskRequest(DateTime startDate, DateTime endDate, int recursEveryXDays) :
			base(startDate, endDate)
		{
			RecursEveryXDays = recursEveryXDays;
		}

	}
}