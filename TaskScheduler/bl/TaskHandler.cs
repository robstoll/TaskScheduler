using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.dl;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;
using ServiceStack.Common.Web;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace ch.tutteli.taskscheduler.bl
{
    public class TaskHandler : ITaskHandler
    {

        private IRepository repository;

        public TaskHandler(IRepository theRepository)
        {
            repository = theRepository;
        }

        public static TTrigger Create<TRequest, TTrigger>(TRequest request)
            where TTrigger : ITrigger
            where TRequest : class, ITaskRequest, new()
        {
            return default(TTrigger);
        }

        public TaskResponse Create<TRequest>(TRequest request)
                where TRequest : class, ITaskRequest, new()
        {
            Validate(request);
            return CreateTask(request);
        }

        private void Validate<TRequest>(TRequest request)
         where TRequest : class, ITaskRequest, new()
        {
            //implicit validation - happens in the specific trigger (invariant check).
            TriggerFactory.Create(request);
        }

        private TaskResponse CreateTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
        {
            ValidateCreateRequest(request);

            return new TaskResponse
            {
                Result = "Task: " + request.Name + " created.",
                Id = repository.CreateTask(request)
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

            if (string.IsNullOrEmpty(request.CallbackUrl)) {
                throw new ArgumentException("CallbackUrl was null or empty");
            }
        }

        public TaskResponse Update<TRequest>(TRequest request)
              where TRequest : class, ITaskRequest, new()
        {
            Validate(request);
            return UpdateTask(request);
        }

        private TaskResponse UpdateTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
        {
            ValidateUpdateRequest(request);

            return new TaskResponse
            {
                Result = "Task: " + request.Name + " updated.",
                Id = repository.UpdateTask(request)
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
            return new TaskResponse
            {
                Result = typeof(TRequest).Name+" with id: " + request.Id + " deleted.",
            };
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