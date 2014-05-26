using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch.tutteli.taskscheduler.requests
{
	public class AReccuringTaskRequest : ATaskRequest
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public AReccuringTaskRequest(DateTime startDate, DateTime endDate) { 
			StartDate = startDate;
			EndDate = endDate;
		}
	}
}