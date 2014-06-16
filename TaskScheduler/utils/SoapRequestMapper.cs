using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Requests;

namespace CH.Tutteli.TaskScheduler.Utils
{
    public static class SoapRequestHelper
    {

        internal static OneTimeTaskRequest CreateRequest(PostOneTimeTask request)
        {
            return CreateOneTimeTaskRequest(request);
        }

        internal static OneTimeTaskRequest CreateRequest(PutOneTimeTask request)
        {
            return CreateOneTimeTaskRequest(request);
        }
        internal static OneTimeTaskRequest CreateRequest(DeleteOneTimeTask request)
        {
            return new OneTimeTaskRequest { Id = request.Id };
        }

        private static OneTimeTaskRequest CreateOneTimeTaskRequest(OneTimeTaskRequest request)
        {
            var result = new OneTimeTaskRequest();
            MapBasicPropertiesFromTo(request, result);
            result.Trigger = request.Trigger;
            return result;
        }
      

        internal static DailyTaskRequest CreateRequest(PostDailyTask request)
        {
            return CreateDailyTaskRequest(request);
        }

        internal static DailyTaskRequest CreateRequest(PutDailyTask request)
        {
            return CreateDailyTaskRequest(request);
        }

        internal static DailyTaskRequest CreateRequest(DeleteDailyTask request)
        {
            return new DailyTaskRequest{Id = request.Id};
        }

        private static DailyTaskRequest CreateDailyTaskRequest(DailyTaskRequest request)
        {
            var result = new DailyTaskRequest();
            MapBasicPropertiesFromTo(request, result);
            MapRecurringPropertiesFromTo(request, result);
            result.RecursEveryXDays = request.RecursEveryXDays;
            result.TriggerWhenDayOfWeek = request.TriggerWhenDayOfWeek;
            return result;
        }


        internal static WeeklyTaskRequest CreateRequest(PostWeeklyTask request)
        {
            return CreateWeeklyTaskRequest(request);
        }

        internal static WeeklyTaskRequest CreateRequest(PutWeeklyTask request)
        {
            return CreateWeeklyTaskRequest(request);
        }

        internal static WeeklyTaskRequest CreateRequest(DeleteWeeklyTask request)
        {
            return new WeeklyTaskRequest{Id = request.Id};
        }

        private static WeeklyTaskRequest CreateWeeklyTaskRequest(WeeklyTaskRequest request)
        {
            var result = new WeeklyTaskRequest();
            MapBasicPropertiesFromTo(request, result);
            MapRecurringPropertiesFromTo(request, result);
            result.RecursEveryXWeeks = request.RecursEveryXWeeks;
            result.TriggerWhenDayOfWeek = request.TriggerWhenDayOfWeek;
            return result;
        }


        internal static MonthlyTaskRequest CreateRequest(PostMonthlyTask request)
        {
            return CreateMonthlyTaskRequest(request);
        }

        internal static MonthlyTaskRequest CreateRequest(PutMonthlyTask request)
        {
            return CreateMonthlyTaskRequest(request);
        }

        internal static MonthlyTaskRequest CreateRequest(DeleteMonthlyTask request)
        {
            return new MonthlyTaskRequest{Id = request.Id};
        }

        private static MonthlyTaskRequest CreateMonthlyTaskRequest(MonthlyTaskRequest request)
        {
            var result = new MonthlyTaskRequest();
            MapBasicPropertiesFromTo(request, result);
            MapRecurringPropertiesFromTo(request, result);
            result.RecursOnMonth = request.RecursOnMonth;
            result.RecursOnDayOfMonth = request.RecursOnDayOfMonth;
            result.RecursOnSpecialDayOfMonth = request.RecursOnSpecialDayOfMonth;
            return result;
        }


        private static void MapBasicPropertiesFromTo(ITaskRequest task, ITaskRequest result)
        {
            result.Id = task.Id;
            result.DateCreated = task.DateCreated;
            result.DateUpdated = task.DateUpdated;
            result.Name = task.Name;
            result.Description = task.Description;
            result.CallbackUrl = task.CallbackUrl;
        }

        private static void MapRecurringPropertiesFromTo(IRecurringTaskRequest request, IRecurringTaskRequest result)
        {
            result.StartDate = request.StartDate;
            result.EndDate = request.EndDate;
        }

        internal static TRequest MapResponseFromTo<TRequest>(TaskResponse response, TRequest result)
        where TRequest : TaskResponse
        {
            result.Id = response.Id;
            result.Result = response.Result;
            return result;
        }

      
    }
}