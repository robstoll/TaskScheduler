using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Common;

namespace CH.Tutteli.TaskScheduler.Requests
{
    [DataContract]
    public abstract class ARecurringTaskRequest : ATaskRequest, IRecurringTaskRequest
    {
        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }
    }
}
