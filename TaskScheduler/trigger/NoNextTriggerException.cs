using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch.tutteli.taskscheduler.trigger
{
	public class NoNextTriggerException : Exception
	{

		public DateTime EndDate { get; set; }

		public NoNextTriggerException(DateTime endDate)
		{
			EndDate = endDate;
		}

	}
}