using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace CH.Tutteli.TaskScheduler
{
    public class TaskSchedulerService : Service
    {

        private ITaskHandler taskHandler;

        public TaskSchedulerService(ITaskHandler theTaskHandler)
        {
            taskHandler = theTaskHandler;
        }

        #region GET

        public OneTimeTaskRequest Get(OneTimeTaskRequest request)
        { 
            return taskHandler.Get(request);
        }

        public DailyTaskRequest Get(DailyTaskRequest request)
        {
            return taskHandler.Get(request);
        }

        public WeeklyTaskRequest Get(WeeklyTaskRequest request)
        {
            return taskHandler.Get(request);
        }

        public MonthlyTaskRequest Get(MonthlyTaskRequest request)
        {
            return taskHandler.Get(request);
        }

        #endregion

        #region POST

        public TaskResponse Post(OneTimeTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request), Global.ONE_TIME);
        }

        public TaskResponse Post(DailyTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request), Global.DAILY);
        }

        public TaskResponse Post(WeeklyTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request), Global.WEEKLY);
        }

        public TaskResponse Post(MonthlyTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request), Global.MONTHLY);
        }

        private TaskResponse ReturnCreated(TaskResponse taskResponse, string taskType)
        {
            string pathToNewResource = GetAppHost().Config.WebHostUrl + Global.URL_PREFIX + taskType + "/" + taskResponse.Id;
            base.Response.StatusCode = (int)HttpStatusCode.Created;
            base.Response.AddHeader("Location", pathToNewResource);
            return taskResponse;
        }

        #endregion

        #region PUT

        public TaskResponse Put(OneTimeTaskRequest request)
        {
            return taskHandler.Update(request);
        }

        public TaskResponse Put(DailyTaskRequest request)
        {
            return taskHandler.Update(request);
        }

        public TaskResponse Put(WeeklyTaskRequest request)
        {
            return taskHandler.Update(request);
        }

        public TaskResponse Put(MonthlyTaskRequest request)
        {
            return taskHandler.Update(request);
        }

        #endregion

        #region DELETE

        public TaskResponse Delete(OneTimeTaskRequest request)
        {
            return taskHandler.Delete(request);
        }

        public TaskResponse Delete(DailyTaskRequest request)
        {
            return taskHandler.Delete(request);
        }

        public TaskResponse Delete(WeeklyTaskRequest request)
        {
            return taskHandler.Delete(request);
        }

        public TaskResponse Delete(MonthlyTaskRequest request)
        {
            return taskHandler.Delete(request);
        }

        #endregion
    }
}