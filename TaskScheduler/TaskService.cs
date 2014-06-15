using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ch.tutteli.taskscheduler.bl;
using ch.tutteli.taskscheduler.dl;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;
using ServiceStack.Common.Utils;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace ch.tutteli.taskscheduler
{
    public class TaskSchedulerService : Service
    {
        

        private ITaskHandler taskHandler;

        public TaskSchedulerService(ITaskHandler theTaskHandler)
        {
            taskHandler = theTaskHandler;
        }

        public TaskResponse Get(TasksRequest request)
        {
            return new TaskResponse { Result = "test" };
        }

        public TaskResponse Post(OneTimeTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request),Global.ONE_TIME);
        }

        public TaskResponse Post(DailyTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request),Global.DAILY);
        }

        public TaskResponse Post(WeeklyTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request), Global.WEEKLY);
        }

        public TaskResponse Post(MonthlyTaskRequest request)
        {
            return ReturnCreated(taskHandler.Create(request), Global.MONTHLY );
        }

        private TaskResponse ReturnCreated(TaskResponse taskResponse, string taskType)
        {
            string pathToNewResource = GetAppHost().Config.WebHostUrl + Global.URL_PREFIX + taskType + "/" + taskResponse.Id;
            base.Response.StatusCode = (int)HttpStatusCode.Created;
            base.Response.AddHeader("Location", pathToNewResource);
            return taskResponse;
        }

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
    }
}