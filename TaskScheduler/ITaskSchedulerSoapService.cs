using System;
using CH.Tutteli.TaskScheduler.Requests;
namespace CH.Tutteli.TaskScheduler
{
    interface ITaskSchedulerSoapService
    {
     
        GetDailyTaskResponse Any(GetDailyTask request);
        GetMonthlyTaskResponse Any(GetMonthlyTask request);
        GetOneTimeTaskResponse Any(GetOneTimeTask request);
        GetWeeklyTaskResponse Any(GetWeeklyTask request);

        PostDailyTaskResponse Any(PostDailyTask request);
        PostMonthlyTaskResponse Any(PostMonthlyTask request);
        PostOneTimeTaskResponse Any(PostOneTimeTask request);
        PostWeeklyTaskResponse Any(PostWeeklyTask request);

        PutDailyTaskResponse Any(PutDailyTask request);
        PutMonthlyTaskResponse Any(PutMonthlyTask request);
        PutOneTimeTaskResponse Any(PutOneTimeTask request);
        PutWeeklyTaskResponse Any(PutWeeklyTask request);

        DeleteDailyTaskResponse Any(DeleteDailyTask request);
        DeleteMonthlyTaskResponse Any(DeleteMonthlyTask request);
        DeleteOneTimeTaskResponse Any(DeleteOneTimeTask request);
        DeleteWeeklyTaskResponse Any(DeleteWeeklyTask request);
    }
}
