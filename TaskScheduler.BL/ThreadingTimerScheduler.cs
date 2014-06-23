using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using TaskSched = System.Threading.Tasks.TaskScheduler;

namespace CH.Tutteli.TaskScheduler.BL
{
    public class ThreadingTimerScheduler : IScheduler
    {
        private IDictionary<string, Timer> timers = new Dictionary<string, Timer>();
        private object lockObject = new object();


        public void AddOrUpdate(string id, ITrigger trigger, SchedulerCallback callback)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id was null or empty");
            }

            if (timers.ContainsKey(id))
            {
                Remove(id);
            }

            SetNextTrigger(id, trigger, callback);
        }

        private void SetNextTrigger(string id, ITrigger trigger, SchedulerCallback callback)
        {
            var milliSeconds = GetMilliSecondsToNextTrigger(trigger);

            bool isNotIntermediateCallback = true;
            long max = int.MaxValue * 2L;
            if (milliSeconds > max)
            {
                isNotIntermediateCallback = false;
                milliSeconds = max;
            }

            TimerCallback timerCallback = s =>
            {
                if (TimerIsNotCancelled(id))
                {
                    if (isNotIntermediateCallback)
                    {
                        InvokeCallbackAndSetNextTrigger(id, trigger, callback);
                    }
                    else
                    {
                        SetNextTrigger(id, trigger, callback);
                    }
                }
            };

            timers[id] = new System.Threading.Timer(timerCallback, null, milliSeconds, Timeout.Infinite);
        }

        private long GetMilliSecondsToNextTrigger(ITrigger trigger)
        {
            var nextTrigger = trigger.GetNextTrigger(DateTime.Now);
            var duration = nextTrigger - DateTime.Now;
            return (long)duration.TotalMilliseconds;
        }

        private void InvokeCallbackAndSetNextTrigger(string id, ITrigger trigger, SchedulerCallback callback)
        {
            callback(id);

            try
            {
                SetNextTrigger(id, trigger, callback);
            }
            catch (NoNextTriggerException)
            {
                // fair enough - no further timer necessary
            }
        }

        private bool TimerIsNotCancelled(string id)
        {
            bool contains;
            lock (lockObject)
            {
                contains = timers.ContainsKey(id);
            }
            return contains;
        }

        public void Remove(string id)
        {
            lock (lockObject)
            {
                timers[id].Change(Timeout.Infinite, Timeout.Infinite);
                timers.Remove(id);
            }
        }
    }
}