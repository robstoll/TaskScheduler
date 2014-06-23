using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Tutteli.TaskScheduler.Requests
{
    public interface IRecurringTaskRequest : ITaskRequest
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
