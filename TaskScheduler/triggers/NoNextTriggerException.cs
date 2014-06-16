using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CH.Tutteli.TaskScheduler.Triggers
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