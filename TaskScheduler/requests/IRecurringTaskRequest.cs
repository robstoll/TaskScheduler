using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.tutteli.taskscheduler.requests
{
    public interface IRecurringTaskRequest : ITaskRequest
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
