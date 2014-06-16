using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CH.Tutteli.TaskScheduler.Triggers;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace CH.Tutteli.TaskScheduler.Requests
{
    [Restrict(EndpointAttributes.Any ^ EndpointAttributes.Soap11 ^ EndpointAttributes.Soap12)]
    [DataContract]
    public class MonthlyTaskRequest : IRecurringTaskRequest, IReturn<TaskResponse>, IReturn<MonthlyTaskRequest>
	{
        #region general properties - code duplication in all request objects

        [PrimaryKey]
        [AutoIncrement]
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateUpdated { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CallbackUrl { get; set; }

        #endregion

        #region recurring task properties - code duplication in daily- , weakly- and monthly-task-request

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        #endregion

        [DataMember]
		public HashSet<EMonth> RecursOnMonth { get; set; }

        [DataMember]
        public HashSet<EDayOfMonth> RecursOnDayOfMonth { get; set; }

        [DataMember]
		public Dictionary<EMonthlyOn, IList<DayOfWeek>> RecursOnSpecialDayOfMonth { get; set; }
	}
}