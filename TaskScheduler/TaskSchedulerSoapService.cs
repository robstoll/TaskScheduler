using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Utils;
using ServiceStack.ServiceInterface;

namespace CH.Tutteli.TaskScheduler
{
    public class TaskSchedulerSoapService : Service
    {

        private ITaskHandler taskHandler;

        public TaskSchedulerSoapService(ITaskHandler theTaskHandler)
        {
            taskHandler = theTaskHandler;
        }

        #region GET

        public GetOneTimeTaskResponse Any(GetOneTimeTask request)
        {
            var req = new OneTimeTaskRequest { Id = request.Id };
            var result = taskHandler.Get(req);
            return new GetOneTimeTaskResponse { OneTimeTaskRequest = result };
        }

        public GetDailyTaskResponse Any(GetDailyTask request)
        {
            var req = new DailyTaskRequest { Id = request.Id };
            var result = taskHandler.Get(req);
            return new GetDailyTaskResponse { DailyTaskRequest = result };
        }

        public GetWeeklyTaskResponse Any(GetWeeklyTask request)
        {
            var req = new WeeklyTaskRequest { Id = request.Id };
            var result = taskHandler.Get(req);
            return new GetWeeklyTaskResponse { WeeklyTaskRequest = result };
        }
        public GetMonthlyTaskResponse Any(GetMonthlyTask request)
        {
            var req = new MonthlyTaskRequest { Id = request.Id };
            var result = taskHandler.Get(req);
            return new GetMonthlyTaskResponse { MonthlyTaskRequest = result };
        }

        #endregion

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