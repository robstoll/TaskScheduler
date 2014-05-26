using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.bl;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace ch.tutteli.taskscheduler
{
	public class TaskSchedulerService : Service
	{
		private TaskHandler TaskHandler { get; set; }

		public TaskSchedulerService()
		{
			TaskHandler = new TaskHandler();
		}

		public TaskResponse Get(TasksRequest request)
		{
			return new TaskResponse { Result = "test" };
		}

		public TaskResponse Post(OneTimeTaskRequest request)
		{
			return TaskHandler.Create(request);
		}

		public TaskResponse Post(DailyTaskRequest request)
		{
			return TaskHandler.Create(request);
		}

		public TaskResponse Post(WeeklyTaskRequest request)
		{
			return TaskHandler.Create(request);
		}

		public TaskResponse Post(MonthlyTaskRequest request)
		{
			return TaskHandler.Create(request);
		}

	}
}