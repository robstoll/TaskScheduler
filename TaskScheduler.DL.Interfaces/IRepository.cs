using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Common;

namespace CH.Tutteli.TaskScheduler.DL.Interfaces
{
	public interface IRepository
	{
        IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskDto, new();

        TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskDto, new();

        long CreateTask<TRequest>(TRequest request) where TRequest : class, ITaskDto, new();

        long UpdateTask<TRequest>(TRequest request) where TRequest : class, ITaskDto, new();

        void DeleteTask<TRequest>(long id) where TRequest : class, ITaskDto, new();
    }
}
