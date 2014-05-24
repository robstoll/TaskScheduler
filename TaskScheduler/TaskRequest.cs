using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler
{
	[Route("/task")]
	[Route("/task/{Name}")]
	public class TaskRequest
	{
		public string Name { get;set; }
		public string Description { get;set; }

		public ETriggerType triggerType;
	}
	

}