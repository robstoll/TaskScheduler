using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler.requests
{
	[Route("/task/weekly")]
	public class WeeklyTaskRequest : AReccuringTaskRequest, IReturn<TaskResponse>
	{
		public int RecursEveryXWeeks { get; set; }

		public ISet<DayOfWeek> TriggerWhenDayOfWeek { get; set; }

		public WeeklyTaskRequest(DateTime startDate, DateTime endDate, int recursEveryXWeeks) :
			base(startDate, endDate)
		{
			RecursEveryXWeeks = recursEveryXWeeks;
		}
	}
}