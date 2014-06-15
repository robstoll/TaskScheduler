using System;
namespace ch.tutteli.taskscheduler.test
{
    interface ISyncReplyClient
    {
        ch.tutteli.taskscheduler.requests.DeleteDailyTaskResponse DeleteDailyTask(ch.tutteli.taskscheduler.requests.DeleteDailyTask DeleteDailyTask1);
        ch.tutteli.taskscheduler.requests.DeleteMonthlyTaskResponse DeleteMonthlyTask(ch.tutteli.taskscheduler.requests.DeleteMonthlyTask DeleteMonthlyTask1);
        ch.tutteli.taskscheduler.requests.DeleteOneTimeTaskResponse DeleteOneTimeTask(ch.tutteli.taskscheduler.requests.DeleteOneTimeTask DeleteOneTimeTask1);
        ch.tutteli.taskscheduler.requests.DeleteWeeklyTaskResponse DeleteWeeklyTask(ch.tutteli.taskscheduler.requests.DeleteWeeklyTask DeleteWeeklyTask1);
        ch.tutteli.taskscheduler.requests.PostDailyTaskResponse PostDailyTask(ch.tutteli.taskscheduler.requests.PostDailyTask PostDailyTask1);
        ch.tutteli.taskscheduler.requests.PostMonthlyTaskResponse PostMonthlyTask(ch.tutteli.taskscheduler.requests.PostMonthlyTask PostMonthlyTask1);
        ch.tutteli.taskscheduler.requests.PostOneTimeTaskResponse PostOneTimeTask(ch.tutteli.taskscheduler.requests.PostOneTimeTask PostOneTimeTask1);
        ch.tutteli.taskscheduler.requests.PostWeeklyTaskResponse PostWeeklyTask(ch.tutteli.taskscheduler.requests.PostWeeklyTask PostWeeklyTask1);
        ch.tutteli.taskscheduler.requests.PutDailyTaskResponse PutDailyTask(ch.tutteli.taskscheduler.requests.PutDailyTask PutDailyTask1);
        ch.tutteli.taskscheduler.requests.PutMonthlyTaskResponse PutMonthlyTask(ch.tutteli.taskscheduler.requests.PutMonthlyTask PutMonthlyTask1);
        ch.tutteli.taskscheduler.requests.PutOneTimeTaskResponse PutOneTimeTask(ch.tutteli.taskscheduler.requests.PutOneTimeTask PutOneTimeTask1);
        ch.tutteli.taskscheduler.requests.PutWeeklyTaskResponse PutWeeklyTask(ch.tutteli.taskscheduler.requests.PutWeeklyTask PutWeeklyTask1);
    }
}
