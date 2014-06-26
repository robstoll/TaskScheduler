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
    public class OneTimeTaskRequest : ATaskRequest, IReturn<TaskResponse>, IReturn<OneTimeTaskRequest>
    {
        [DataMember]
        public DateTime Trigger { get; set; }

    }
}