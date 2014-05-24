using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace ch.tutteli.taskscheduler
{
	public class TaskSchedulerService : Service
	{
		public object Any(TaskRequest request)
		{
			return new TaskResponse { Result = "Task: " + request.Name };
		}
	}
}