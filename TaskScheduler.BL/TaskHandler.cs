using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using System.Reflection;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using CH.Tutteli.TaskScheduler.BLDLMapper.Interfaces;
using TaskScheduler.BLDLMapper.Interfaces;

namespace CH.Tutteli.TaskScheduler.BL
{
    public class TaskHandler : ITaskHandler
    {

        private IScheduler scheduler;
        private ICallbackVerifier callbackVerifier;
        private IBLRepository repository;

        public TaskHandler(
            IScheduler theScheduler,
            ICallbackVerifier theCallbackVerifier,
            IBLRepository theRepository)
        {
            scheduler = theScheduler;
            repository = theRepository;
            callbackVerifier = theCallbackVerifier;
        }

        public TRequest Get<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
        {
            ValidateGetRequest(request);            

            return repository.LoadTask<TRequest>(request.Id);
        }

        private void ValidateGetRequest(ITaskRequest request)
        {
            if (request.Id == default(long))
            {
                throw new ArgumentException("Id was not set, cannot get a resource without knowing its id");
            }
        }

        public TaskResponse Create<TRequest>(TRequest request)
                where TRequest : class, ITaskRequest, new()
        {
            ValidateCreateRequest(request);

            //implicit validation - invariant check
            var trigger = TriggerFactory.Create(request);

            request.Id = repository.CreateTask(request);
            scheduler.AddOrUpdate(GetTriggerId(request), trigger, OnCallback);

            return new TaskResponse
            {
                Result = "Task: " + request.Name + " created.",
                Id = request.Id
            };
        }

        private void ValidateCreateRequest(ITaskRequest request)
        {
            if (request.Id != default(long))
            {
                throw new ArgumentException("Id provided, possible attempt to update a resource but create operation used.");
            }

            ValidateCreateOrUpdateRequest(request);
        }

        private void ValidateCreateOrUpdateRequest(ITaskRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException("Name was null or empty");
            }

            if (string.IsNullOrEmpty(request.CallbackUrl))
            {
                throw new ArgumentException("CallbackUrl was null or empty");
            }

            if (!callbackVerifier.IsSecureCallback(request.CallbackUrl)) { 
                throw new ArgumentException("CallbackUrl has not passed the security check");
            }
        }

        private void OnCallback(string id)
        {
            var split = id.Split('-');
            var requestId = long.Parse(split[1]);
            var type = split[0];

            ITaskRequest request = GetRequest(requestId, type);

            //who knows, maybe the policy has changed over time
            if (callbackVerifier.IsSecureCallback(request.CallbackUrl))
            {
                var webRequest = HttpWebRequest.Create(request.CallbackUrl);
                webRequest.Method = "GET";
                try { 
                    webRequest.GetResponse();
                }
                catch (WebException) {
                    //TODO logging if something goes wrong
                }
            }
            else
            {
                //TODO logging if policy has changed
            }
        }

        private ITaskRequest GetRequest(long requestId, string type)
        {
            ITaskRequest request = null;
            if (type.Equals(typeof(OneTimeTaskRequest).FullName))
            {
                request = repository.LoadTask<OneTimeTaskRequest>(requestId);
            }
            else if (type.Equals(typeof(DailyTaskRequest).FullName))
            {
                request = repository.LoadTask<DailyTaskRequest>(requestId);
            }
            else if (type.Equals(typeof(WeeklyTaskRequest).FullName))
            {
                request = repository.LoadTask<WeeklyTaskRequest>(requestId);
            }
            else if (type.Equals(typeof(MonthlyTaskRequest).FullName))
            {
                request = repository.LoadTask<MonthlyTaskRequest>(requestId);
            }
            return request;
        }

        public TaskResponse Update<TRequest>(TRequest request)
              where TRequest : class, ITaskRequest, new()
        {
            ValidateUpdateRequest(request);

            //implicit validation - invariant check
            var trigger = TriggerFactory.Create(request);

            request.Id = repository.UpdateTask(request);
            scheduler.AddOrUpdate(GetTriggerId(request), trigger, OnCallback);

            return new TaskResponse
            {
                Result = "Task: " + request.Name + " updated.",
                Id = request.Id
            };
        }

        private void ValidateUpdateRequest(ITaskRequest request)
        {
            if (request.Id == default(long))
            {
                throw new ArgumentException("Id not provided, tried to update a resource without providing its id");
            }

            ValidateCreateOrUpdateRequest(request);
        }

        public TaskResponse Delete<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
        {
            ValidateDeleteRequest(request);

            repository.DeleteTask<TRequest>(request.Id);
            scheduler.Remove(GetTriggerId(request));

            return new TaskResponse
            {
                Result = typeof(TRequest).Name + " with id: " + request.Id + " deleted.",
            };
        }

        private string GetTriggerId(ITaskRequest request)
        {
            return request.GetType().FullName + "-" + request.Id;
        }

        private void ValidateDeleteRequest(ITaskRequest request)
        {
            if (request.Id == default(long))
            {
                throw new ArgumentException("Id not provided, tried to delete a resource without providing its id");
            }
        }

    }
}