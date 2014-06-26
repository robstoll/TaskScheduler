using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Dtos;

namespace CH.Tutteli.TaskScheduler.Test.Utils
{
    public static class TaskHelper
    {
        public static string URL = "http://localhost:4658/";
        
        public static OneTimeTaskRequest CreateOneTimeTaskRequest()
        {
            var task = new OneTimeTaskRequest();
            InitBasicTaskProperties(task);
            task.Trigger = DateTime.Now.AddDays(10);
            return task;
        }

        public static OneTimeTaskDto CreateOneTimeTaskDto()
        {
            var task = new OneTimeTaskDto();
            InitBasicTaskProperties(task);
            task.Trigger = DateTime.Now.AddDays(10);
            return task;
        }

        private static void InitBasicTaskProperties(ITaskRequest task)
        {
            task.Name = "test";
            task.Description = "descr";
            task.CallbackUrl = URL;
        }

        public static DailyTaskRequest CreateDailyTaskRequest()
        {
            var task = new DailyTaskRequest();
            InitRecurringTaskProperties(task);
            task.RecursEveryXDays = 10;
            task.TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday };
            return task;
        }

        public static DailyTaskDto CreateDailyTaskDto()
        {
            var task = new DailyTaskDto();
            InitRecurringTaskProperties(task);
            task.RecursEveryXDays = 10;
            task.TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday };
            return task;
        }

        private static void InitRecurringTaskProperties(IRecurringTaskRequest task)
        {
            InitBasicTaskProperties(task);
            task.StartDate = task.StartDate = DateTime.Now;
            task.EndDate = DateTime.Now.AddYears(10);
        }

        public static WeeklyTaskRequest CreateWeaklyTaskRequest()
        {
            var task = new WeeklyTaskRequest();
            InitRecurringTaskProperties(task);
            task.TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday };
            task.RecursEveryXWeeks = 2;
            return task;
        }

        public static WeeklyTaskDto CreateWeaklyTaskDto()
        {
            var task = new WeeklyTaskDto();
            InitRecurringTaskProperties(task);
            task.TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday };
            task.RecursEveryXWeeks = 2;
            return task;
        }

        public static MonthlyTaskRequest CreateMonthlyTaskRequest()
        {
            var task = new MonthlyTaskRequest();
            InitRecurringTaskProperties(task);
            task.RecursOnDayOfMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1, EDayOfMonth.D15 };
            task.RecursOnMonth = new HashSet<EMonth> { EMonth.March, EMonth.July };
            task.RecursOnSpecialDayOfMonth = new Dictionary<EMonthlyOn, IList<DayOfWeek>> { { EMonthlyOn.First, new List<DayOfWeek> { DayOfWeek.Tuesday } } };
            return task;
        }

        public static MonthlyTaskDto CreateMonthlyTaskDto()
        {
            var task = new MonthlyTaskDto();
            InitRecurringTaskProperties(task);
            task.RecursOnDayOfMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1, EDayOfMonth.D15 };
            task.RecursOnMonth = new HashSet<EMonth> { EMonth.March, EMonth.July };
            task.RecursOnSpecialDayOfMonth = new Dictionary<EMonthlyOn, IList<DayOfWeek>> { { EMonthlyOn.First, new List<DayOfWeek> { DayOfWeek.Tuesday } } };
            return task;
        }
    }
}
