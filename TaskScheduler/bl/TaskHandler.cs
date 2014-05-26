using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;

namespace ch.tutteli.taskscheduler.bl
{
	public class TaskHandler
	{
		public TaskResponse Create(OneTimeTaskRequest request)
		{
			return SaveTrigger(request, new OneTimeTrigger(request.Trigger));
		}

		public TaskResponse Create(DailyTaskRequest request)
		{

			return SaveTrigger(request, new DailyTrigger(
				request.StartDate,
				request.EndDate,
				request.RecursEveryXDays));
		}

		public TaskResponse Create(WeeklyTaskRequest request)
		{

			return SaveTrigger(request, new WeeklyTrigger(
				request.StartDate,
				request.EndDate,
				request.RecursEveryXWeeks,
				request.TriggerWhenDayOfWeek));
		}

		public TaskResponse Create(MonthlyTaskRequest request)
		{
			var monthlyRecurrence = new MonthlyRecurrence(request.RecursOnMonth, request.RecursOnDayOfMonth, request.RecursOnSpecialDayOfMonth);
			return SaveTrigger(request, new MonthlyTrigger(
				request.StartDate,
				request.EndDate,
				monthlyRecurrence));
		}

		private TaskResponse SaveTrigger(ATaskRequest request, ITrigger trigger)
		{
			//TODO save
			return new TaskResponse { Result = "Request: " + request.Name + " created." };
		}
	}
}