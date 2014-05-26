using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ch.tutteli.taskscheduler.triggers
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