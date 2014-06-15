using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;

namespace ch.tutteli.taskscheduler.test.utils
{
    public static class TaskRequestHelper
    {

        public static OneTimeTaskRequest CreateOneTimeTaskRequest()
        {
            return new OneTimeTaskRequest
            {
                Name = "test",
                Description = "descr",
                CallbackUrl = "http://returnurl",
                Trigger = DateTime.Now
            };
        }

        public static DailyTaskRequest CreateDailyTaskRequest()
        {
            return new DailyTaskRequest
            {
                Name = "test",
                Description = "descr",
                CallbackUrl = "http://returnurl",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RecursEveryXDays = 10,
                TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday }
            };
        }

        public static WeeklyTaskRequest CreateWeaklyTaskRequest()
        {
            return new WeeklyTaskRequest
            {
                Name = "test",
                Description = "descr",
                CallbackUrl = "http://returnurl",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday },
                RecursEveryXWeeks = 2
            };
        }

        public static MonthlyTaskRequest CreateMonthlyTaskRequest()
        {
            return new MonthlyTaskRequest
            {
                Name = "test",
                Description = "descr",
                CallbackUrl = "http://returnurl",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                RecursOnDayOfMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1, EDayOfMonth.D15 },
                RecursOnMonth = new HashSet<EMonth> { EMonth.March, EMonth.July },
                RecursOnSpecialDayOfMonth = new Dictionary<EMonthlyOn, IList<DayOfWeek>> { { EMonthlyOn.First, new List<DayOfWeek> { DayOfWeek.Tuesday } } }
            };
        }
    }
}
