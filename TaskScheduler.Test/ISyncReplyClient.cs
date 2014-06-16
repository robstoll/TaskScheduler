using System;
using CH.Tutteli.TaskScheduler.Requests;
namespace CH.Tutteli.TaskScheduler.Test
{
    public interface ISyncReplyClient
    {
        GetOneTimeTaskResponse GetOneTimeTask(GetOneTimeTask request);
        GetDailyTaskResponse GetDailyTask(GetDailyTask request);
        GetWeeklyTaskResponse GetWeeklyTask(GetWeeklyTask request);
        GetMonthlyTaskResponse GetMonthlyTask(GetMonthlyTask request);

        PostDailyTaskResponse PostDailyTask(PostDailyTask request);
        PostMonthlyTaskResponse PostMonthlyTask(PostMonthlyTask request);
        PostOneTimeTaskResponse PostOneTimeTask(PostOneTimeTask request);
        PostWeeklyTaskResponse PostWeeklyTask(PostWeeklyTask request);

        PutDailyTaskResponse PutDailyTask(PutDailyTask request);
        PutMonthlyTaskResponse PutMonthlyTask(PutMonthlyTask request);
        PutOneTimeTaskResponse PutOneTimeTask(PutOneTimeTask request);
        PutWeeklyTaskResponse PutWeeklyTask(PutWeeklyTask request);

        DeleteDailyTaskResponse DeleteDailyTask(DeleteDailyTask request);
        DeleteMonthlyTaskResponse DeleteMonthlyTask(DeleteMonthlyTask request);
        DeleteOneTimeTaskResponse DeleteOneTimeTask(DeleteOneTimeTask request);
        DeleteWeeklyTaskResponse DeleteWeeklyTask(DeleteWeeklyTask request);
    }
}
