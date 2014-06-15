using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;

namespace ch.tutteli.taskscheduler.dl
{
	public interface IRepository
	{
		long SaveTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();

        IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskRequest, new();

		TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskRequest, new();

        void DeleteTask<TRequest>(long id) where TRequest : class, ITaskRequest, new();
    }
}
