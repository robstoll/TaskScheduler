using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler.requests
{
    public class DailyTaskRequest : IRecurringTaskRequest
	{

        #region general properties - code duplication in all request objects

        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CallbackUrl { get; set; }

        #endregion

        #region recurring task properties - code duplication in daily- , weakly- and monthly-task-request

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion

		public int RecursEveryXDays { get; set; }    
    
        public HashSet<DayOfWeek> TriggerWhenDayOfWeek { get; set; }

		public DailyTaskRequest() { }
	}
}