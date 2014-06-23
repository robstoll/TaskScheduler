﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.BL.Triggers;

namespace CH.Tutteli.TaskScheduler.Test.Utils
{
    public static class TaskRequestHelper
    {
        public static string URL = "http://localhost:4658/";
        public static OneTimeTaskRequest CreateOneTimeTaskRequest()
        {
            return InitOneTimeTaskRequest(new OneTimeTaskRequest());
        }

        public static TRequest InitOneTimeTaskRequest<TRequest>(TRequest request)
            where TRequest : OneTimeTaskRequest
        {
            request.Name = "test";
            request.Description = "descr";
            request.CallbackUrl = URL;
            request.Trigger = DateTime.Now.AddDays(10);
            return request;
        }

        public static DailyTaskRequest CreateDailyTaskRequest()
        {
            return InitDailyTaskRequest(new DailyTaskRequest());

        }
        public static TRequest InitDailyTaskRequest<TRequest>(TRequest request)
           where TRequest : DailyTaskRequest
        {
            request.Name = "test";
            request.Description = "descr";
            request.CallbackUrl = URL;
            request.StartDate = DateTime.Now;
            request.EndDate = DateTime.Now.AddYears(10);
            request.RecursEveryXDays = 10;
            request.TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday };
            return request;
        }

        public static WeeklyTaskRequest CreateWeaklyTaskRequest()
        {
            return InitWeeklyTaskRequest(new WeeklyTaskRequest());
        }

        public static TRequest InitWeeklyTaskRequest<TRequest>(TRequest request)
            where TRequest : WeeklyTaskRequest
        {
            request.Name = "test";
            request.Description = "descr";
            request.CallbackUrl = URL;
            request.StartDate = DateTime.Now;
            request.EndDate = DateTime.Now.AddYears(10);
            request.TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday };
            request.RecursEveryXWeeks = 2;
            return request;
        }

        public static MonthlyTaskRequest CreateMonthlyTaskRequest()
        {
            return InitMonthlyTaskRequest(new MonthlyTaskRequest());
        }

        public static TRequest InitMonthlyTaskRequest<TRequest>(TRequest request)
            where TRequest : MonthlyTaskRequest
        {
            request.Name = "test";
            request.Description = "descr";
            request.CallbackUrl = URL;
            request.StartDate = DateTime.Now;
            request.EndDate = DateTime.Now.AddYears(10);
            request.RecursOnDayOfMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1, EDayOfMonth.D15 };
            request.RecursOnMonth = new HashSet<EMonth> { EMonth.March, EMonth.July };
            request.RecursOnSpecialDayOfMonth = new Dictionary<EMonthlyOn, IList<DayOfWeek>> { { EMonthlyOn.First, new List<DayOfWeek> { DayOfWeek.Tuesday } } };
            return request;
        }
    }
}