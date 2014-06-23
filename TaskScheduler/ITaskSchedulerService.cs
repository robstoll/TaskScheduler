using System;
using CH.Tutteli.TaskScheduler.Requests;
namespace CH.Tutteli.TaskScheduler
{
    interface ITaskSchedulerService
    {
        DailyTaskRequest Get(DailyTaskRequest request);
        MonthlyTaskRequest Get(MonthlyTaskRequest request);
        OneTimeTaskRequest Get(OneTimeTaskRequest request);
        WeeklyTaskRequest Get(WeeklyTaskRequest request);

        TaskResponse Post(DailyTaskRequest request);
        TaskResponse Post(MonthlyTaskRequest request);
        TaskResponse Post(OneTimeTaskRequest request);
        TaskResponse Post(WeeklyTaskRequest request);

        TaskResponse Put(DailyTaskRequest request);
        TaskResponse Put(MonthlyTaskRequest request);
        TaskResponse Put(OneTimeTaskRequest request);
        TaskResponse Put(WeeklyTaskRequest request);

        TaskResponse Delete(DailyTaskRequest request);
        TaskResponse Delete(MonthlyTaskRequest request);
        TaskResponse Delete(OneTimeTaskRequest request);
        TaskResponse Delete(WeeklyTaskRequest request);
        
    }
}
