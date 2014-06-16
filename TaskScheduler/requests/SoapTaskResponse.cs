﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ServiceStack.ServiceInterface.ServiceModel;

namespace CH.Tutteli.TaskScheduler.Requests
{
    [DataContract]
    public class SoapTaskResponse : TaskResponse, IHasResponseStatus
    {
         
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        public SoapTaskResponse()
        {
            ResponseStatus = new ResponseStatus();
        }
    }
}