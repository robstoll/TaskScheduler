using System;
using ch.tutteli.taskscheduler.requests;
namespace ch.tutteli.taskscheduler.bl
{
	public interface ITaskHandler
	{
        TaskResponse Create<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();
        TaskResponse Update<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();
	}
}
