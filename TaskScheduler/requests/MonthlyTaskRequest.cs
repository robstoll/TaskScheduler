using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.triggers;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler.requests
{
	[Route("/task/monthly")]
	public class MonthlyTaskRequest : AReccuringTaskRequest
	{
		public ISet<EMonth> RecursOnMonth { get; set; }

		public ISet<EDayOfMonth> RecursOnDayOfMonth { get; set; }

		public IDictionary<EMonthlyOn, IList<DayOfWeek>> RecursOnSpecialDayOfMonth { get; set; }

		public MonthlyTaskRequest(DateTime startDate, DateTime endDate, ISet<EMonth> recursOnMonth, ISet<EDayOfMonth> recursOnDayOfMonth) :
			this(startDate, endDate, recursOnMonth, recursOnDayOfMonth, null)
		{
		}

		public MonthlyTaskRequest(DateTime startDate, DateTime endDate, ISet<EMonth> recursOnMonth, IDictionary<EMonthlyOn, IList<DayOfWeek>> recursOnSpecialDayOfMonth) :
			this(startDate, endDate, recursOnMonth, null, recursOnSpecialDayOfMonth)
		{
		}

		public MonthlyTaskRequest(DateTime startDate, DateTime endDate,
		ISet<EMonth> recursOnMonth,
		ISet<EDayOfMonth> recursOnDayOfMonth,
		IDictionary<EMonthlyOn, IList<DayOfWeek>> recursOnSpecialDayOfMonth) :
			base(startDate, endDate)
		{
			RecursOnMonth = recursOnMonth;
			RecursOnDayOfMonth = recursOnDayOfMonth;
			RecursOnSpecialDayOfMonth = recursOnSpecialDayOfMonth;
		}


	}
}