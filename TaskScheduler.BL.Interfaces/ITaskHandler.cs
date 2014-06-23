using System;
using CH.Tutteli.TaskScheduler.Requests;
namespace CH.Tutteli.TaskScheduler.BL
{
	public interface ITaskHandler
	{
        TRequest Get<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();

        TaskResponse Create<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();

        TaskResponse Update<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();

        TaskResponse Delete<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();

    }
}
