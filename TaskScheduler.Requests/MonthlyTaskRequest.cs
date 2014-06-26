using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CH.Tutteli.TaskScheduler.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace CH.Tutteli.TaskScheduler.Requests
{
    [Restrict(EndpointAttributes.Any ^ EndpointAttributes.Soap11 ^ EndpointAttributes.Soap12)]
    [DataContract]
    public class MonthlyTaskRequest : ARecurringTaskRequest, IReturn<TaskResponse>, IReturn<MonthlyTaskRequest>
	{
        [DataMember]
		public HashSet<EMonth> RecursOnMonth { get; set; }

        [DataMember]
        public HashSet<EDayOfMonth> RecursOnDayOfMonth { get; set; }

        [DataMember]
		public Dictionary<EMonthlyOn, IList<DayOfWeek>> RecursOnSpecialDayOfMonth { get; set; }
	}
}