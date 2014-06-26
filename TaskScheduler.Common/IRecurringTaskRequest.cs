using System;

namespace CH.Tutteli.TaskScheduler.Common
{
    public interface IRecurringTaskRequest : ITaskRequest
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
