using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Requests;

namespace CH.Tutteli.TaskScheduler.DL
{
	public interface IRepository
	{
        IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskRequest, new();

        TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskRequest, new();

		long CreateTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();

        long UpdateTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new();
        
        void DeleteTask<TRequest>(long id) where TRequest : class, ITaskRequest, new();
    }
}
