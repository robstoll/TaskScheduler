using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Requests;
using NUnit.Framework;

namespace CH.Tutteli.TaskScheduler.Test.Utils
{
    public static class AssertSame
    {
        public static void OneTimeTaskRequest(OneTimeTaskRequest result, OneTimeTaskRequest task)
        {
            AssertSameBasicTaskProperties(result, task);
            Assert.That(result.Trigger, IsEqual.WithinOneMillisecond(task.Trigger));
        }

        public static void DailyTaskRequest(DailyTaskRequest result, DailyTaskRequest task)
        {
            AssertSameBasicTaskProperties(result, task);
            AssertSameRecurringTaskProperties(result, task);
            Assert.That(result.RecursEveryXDays, Is.EqualTo(task.RecursEveryXDays));
            Assert.That(result.TriggerWhenDayOfWeek, Is.EqualTo(task.TriggerWhenDayOfWeek));
        }

        public static void WeeklyTaskRequest(WeeklyTaskRequest result, WeeklyTaskRequest task)
        {
            AssertSameBasicTaskProperties(result, task);
            AssertSameRecurringTaskProperties(result, task);
            Assert.That(result.TriggerWhenDayOfWeek, Is.EqualTo(task.TriggerWhenDayOfWeek));
            Assert.That(result.RecursEveryXWeeks, Is.EqualTo(task.RecursEveryXWeeks));
        }
        public static void MonthlyTaskRequest(MonthlyTaskRequest result, MonthlyTaskRequest task)
        {
            AssertSameBasicTaskProperties(result, task);
            AssertSameRecurringTaskProperties(result, task);
            Assert.That(result.RecursOnDayOfMonth, Is.EqualTo(task.RecursOnDayOfMonth));
            Assert.That(result.RecursOnMonth, Is.EqualTo(task.RecursOnMonth));
            Assert.That(result.RecursOnSpecialDayOfMonth, Is.EqualTo(task.RecursOnSpecialDayOfMonth));
        }


        private static void AssertSameBasicTaskProperties(ITaskRequest result, ITaskRequest task)
        {
            Assert.That(result.Name, Is.EqualTo(task.Name));
            Assert.That(result.Description, Is.EqualTo(task.Description));
            Assert.That(result.CallbackUrl, Is.EqualTo(task.CallbackUrl));
        }

        private static void AssertSameRecurringTaskProperties(IRecurringTaskRequest result, IRecurringTaskRequest task)
        {
            Assert.That(result.StartDate, IsEqual.WithinOneMillisecond(task.StartDate));
            Assert.That(result.EndDate, IsEqual.WithinOneMillisecond(task.EndDate));
        }


    }
}
