using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ServiceStack.Common.Web;

namespace ch.tutteli.taskscheduler.requests
{
    [DataContract]
    public class TaskResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public long Id { get; set; }
    }
}