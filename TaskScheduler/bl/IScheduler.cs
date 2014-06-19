using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.BL.Triggers;

namespace CH.Tutteli.TaskScheduler.BL
{
    public interface IScheduler
    {
        

        void AddOrUpdate(string id, ITrigger trigger, SchedulerCallback callback);
        
        void Remove(string id);
    }

    public delegate void SchedulerCallback(string id);
}
