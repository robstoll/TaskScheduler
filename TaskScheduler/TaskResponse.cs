using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common.Web;

namespace ch.tutteli.taskscheduler
{
    public class TaskResponse
    {
        public string Result { get; set; }

        public long Id { get; set; }
    }
}