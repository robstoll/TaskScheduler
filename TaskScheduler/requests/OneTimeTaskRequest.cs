using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler.requests
{
	public class OneTimeTaskRequest : ATaskRequest
	{
		public DateTime Trigger { get; set; }
		public OneTimeTaskRequest() { }
		public OneTimeTaskRequest(DateTime trigger)
		{
			Trigger = trigger;
		}
	}
}