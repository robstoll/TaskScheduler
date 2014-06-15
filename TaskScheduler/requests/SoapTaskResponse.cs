using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ServiceStack.ServiceInterface.ServiceModel;

namespace ch.tutteli.taskscheduler.requests
{
    [DataContract]
    public class SoapTaskResponse : TaskResponse, IHasResponseStatus
    {
         
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        public SoapTaskResponse()
        {
            this.ResponseStatus = new ResponseStatus();
        }
    }
}