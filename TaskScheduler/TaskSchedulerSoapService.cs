using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.Requests;
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
            return new PostOneTimeTaskResponse
            {
                TaskResponse = taskHandler.Create(request.OneTimeTaskRequest)
            };
        }

        public PostDailyTaskResponse Any(PostDailyTask request)
        {
            return new PostDailyTaskResponse
            {
                TaskResponse = taskHandler.Create(request.DailyTaskRequest)
            };
        }

        public PostWeeklyTaskResponse Any(PostWeeklyTask request)
        {
            return new PostWeeklyTaskResponse
            {
                TaskResponse = taskHandler.Create(request.WeeklyTaskRequest)
            };
        }

        public PostMonthlyTaskResponse Any(PostMonthlyTask request)
        {
            return new PostMonthlyTaskResponse
            {
                TaskResponse = taskHandler.Create(request.MonthlyTaskRequest)
            };
        }

        #endregion

        #region PUT

        public PutOneTimeTaskResponse Any(PutOneTimeTask request)
        {
            return new PutOneTimeTaskResponse
            {
                TaskResponse = taskHandler.Update(request.OneTimeTaskRequest)
            };
        }

        public PutDailyTaskResponse Any(PutDailyTask request)
        {
            return new PutDailyTaskResponse
            {
                TaskResponse = taskHandler.Update(request.DailyTaskRequest)
            };
        }


        public PutWeeklyTaskResponse Any(PutWeeklyTask request)
        {
            return new PutWeeklyTaskResponse
            {
                TaskResponse = taskHandler.Update(request.WeeklyTaskRequest)
            };
        }

        public PutMonthlyTaskResponse Any(PutMonthlyTask request)
        {
            return new PutMonthlyTaskResponse
            {
                TaskResponse = taskHandler.Update(request.MonthlyTaskRequest)
            };
        }

        #endregion

        #region DELETE

        public DeleteOneTimeTaskResponse Any(DeleteOneTimeTask request)
        {
            return new DeleteOneTimeTaskResponse
            {
                TaskResponse = taskHandler.Delete(new OneTimeTaskRequest { Id = request.Id })
            };
        }

        public DeleteDailyTaskResponse Any(DeleteDailyTask request)
        {
            return new DeleteDailyTaskResponse
            {
                TaskResponse = taskHandler.Delete(new DailyTaskRequest { Id = request.Id })
            };
        }

        public DeleteWeeklyTaskResponse Any(DeleteWeeklyTask request)
        {
            return new DeleteWeeklyTaskResponse
            {
                TaskResponse = taskHandler.Delete(new WeeklyTaskRequest { Id = request.Id })
            };
        }

        public DeleteMonthlyTaskResponse Any(DeleteMonthlyTask request)
        {
            return new DeleteMonthlyTaskResponse
            {
                TaskResponse = taskHandler.Delete(new MonthlyTaskRequest { Id = request.Id })
            };
        }

        #endregion
    }
}