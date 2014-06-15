using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.bl;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.utils;
using ServiceStack.ServiceInterface;

namespace ch.tutteli.taskscheduler
{
    public class TaskSchedulerSoapService : Service
    {

        private ITaskHandler taskHandler;

        public TaskSchedulerSoapService(ITaskHandler theTaskHandler)
        {
            taskHandler = theTaskHandler;
        }

        #region POST
        public PostOneTimeTaskResponse Any(PostOneTimeTask request)
        {
            OneTimeTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Create(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PostOneTimeTaskResponse());
        }

        public PostDailyTaskResponse Any(PostDailyTask request)
        {
            DailyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Create(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PostDailyTaskResponse());
        }

        public PostWeeklyTaskResponse Any(PostWeeklyTask request)
        {
            WeeklyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Create(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PostWeeklyTaskResponse());
        }

        public PostMonthlyTaskResponse Any(PostMonthlyTask request)
        {
            MonthlyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Create(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PostMonthlyTaskResponse());
        }

        #endregion

        #region PUT

        public PutOneTimeTaskResponse Any(PutOneTimeTask request)
        {
            OneTimeTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Update(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PutOneTimeTaskResponse());
        }

        public PutDailyTaskResponse Any(PutDailyTask request)
        {
            DailyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Update(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PutDailyTaskResponse());
        }


        public PutWeeklyTaskResponse Any(PutWeeklyTask request)
        {
            WeeklyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Update(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PutWeeklyTaskResponse());
        }

        public PutMonthlyTaskResponse Any(PutMonthlyTask request)
        {
            MonthlyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Update(req);
            return SoapRequestHelper.MapResponseFromTo(result, new PutMonthlyTaskResponse());
        }

        #endregion

        #region DELETE

        public DeleteOneTimeTaskResponse Any(DeleteOneTimeTask request)
        {
            OneTimeTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Delete(req);
            return new DeleteOneTimeTaskResponse { Result = result.Result };
        }

        public DeleteDailyTaskResponse Any(DeleteDailyTask request)
        {
            DailyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Delete(req);
            return new DeleteDailyTaskResponse { Result = result.Result };
        }

        public DeleteWeeklyTaskResponse Any(DeleteWeeklyTask request)
        {
            WeeklyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Delete(req);
            return new DeleteWeeklyTaskResponse { Result = result.Result };
        }

        public DeleteMonthlyTaskResponse Any(DeleteMonthlyTask request)
        {
            MonthlyTaskRequest req = SoapRequestHelper.CreateRequest(request);
            var result = taskHandler.Delete(req);
            return new DeleteMonthlyTaskResponse { Result = result.Result };
        }

        #endregion
    }
}