using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.BLDLMapper.Interfaces;
using CH.Tutteli.TaskScheduler.DL.Dtos;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using CH.Tutteli.TaskScheduler.Requests;
using TaskScheduler.BLDLMapper.Interfaces;

namespace CH.Tutteli.TaskScheduler.BLDLMapper
{
    public class BLRepository : IBLRepository
    {
        private IRepository repository;
        private IMapper mapper;

        public BLRepository(IRepository theRepository, IMapper theMapper)
        {
            repository = theRepository;
            mapper = theMapper;
        }

        public IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, Common.ITaskRequest, new()
        {
            var type = typeof(TRequest);
            if (type.Equals(typeof(OneTimeTaskRequest)))
            {
                return mapper.ToBL<OneTimeTaskDto, TRequest>(repository.GetAllTasks<OneTimeTaskDto>());
            }
            else if (type.Equals(typeof(DailyTaskRequest)))
            {
                return mapper.ToBL<DailyTaskDto, TRequest>(repository.GetAllTasks<DailyTaskDto>());
            }
            else if (type.Equals(typeof(WeeklyTaskRequest)))
            {
                return mapper.ToBL<WeeklyTaskDto, TRequest>(repository.GetAllTasks<WeeklyTaskDto>());
            }
            else if (type.Equals(typeof(MonthlyTaskRequest)))
            {
                return mapper.ToBL<MonthlyTaskDto, TRequest>(repository.GetAllTasks<MonthlyTaskDto>());
            }
            throw new ArgumentException("Request type " + type.FullName + " is not supported by this repository");
        }

        public TRequest LoadTask<TRequest>(long id) where TRequest : class, Common.ITaskRequest, new()
        {
            var type = typeof(TRequest);
            if (type.Equals(typeof(OneTimeTaskRequest)))
            {
                return mapper.ToBL<OneTimeTaskDto, TRequest>(repository.LoadTask<OneTimeTaskDto>(id));
            }
            else if (type.Equals(typeof(DailyTaskRequest)))
            {
                return mapper.ToBL<DailyTaskDto, TRequest>(repository.LoadTask<DailyTaskDto>(id));
            }
            else if (type.Equals(typeof(WeeklyTaskRequest)))
            {
                return mapper.ToBL<WeeklyTaskDto, TRequest>(repository.LoadTask<WeeklyTaskDto>(id));
            }
            else if (type.Equals(typeof(MonthlyTaskRequest)))
            {
                return mapper.ToBL<MonthlyTaskDto, TRequest>(repository.LoadTask<MonthlyTaskDto>(id));
            }
            throw new ArgumentException("Request type " + type.FullName + " is not supported by this repository");
        }

        public long CreateTask<TRequest>(TRequest request) where TRequest : class, Common.ITaskRequest, new()
        {
            var type = typeof(TRequest);
            if (type.Equals(typeof(OneTimeTaskRequest)))
            {
                return repository.CreateTask<OneTimeTaskDto>(mapper.ToDL<TRequest, OneTimeTaskDto>(request));
            }
            else if (type.Equals(typeof(DailyTaskRequest)))
            {
                return repository.CreateTask<DailyTaskDto>(mapper.ToDL<TRequest, DailyTaskDto>(request));
            }
            else if (type.Equals(typeof(WeeklyTaskRequest)))
            {
                return repository.CreateTask<WeeklyTaskDto>(mapper.ToDL<TRequest, WeeklyTaskDto>(request));
            }
            else if (type.Equals(typeof(MonthlyTaskRequest)))
            {
                return repository.CreateTask<MonthlyTaskDto>(mapper.ToDL<TRequest, MonthlyTaskDto>(request));
            }
            throw new ArgumentException("Request type " + type.FullName + " is not supported by this repository");
        }

        public long UpdateTask<TRequest>(TRequest request) where TRequest : class, Common.ITaskRequest, new()
        {
            var type = typeof(TRequest);
            if (type.Equals(typeof(OneTimeTaskRequest)))
            {
                return repository.UpdateTask<OneTimeTaskDto>(mapper.ToDL<TRequest, OneTimeTaskDto>(request));
            }
            else if (type.Equals(typeof(DailyTaskRequest)))
            {
                return repository.UpdateTask<DailyTaskDto>(mapper.ToDL<TRequest, DailyTaskDto>(request));
            }
            else if (type.Equals(typeof(WeeklyTaskRequest)))
            {
                return repository.UpdateTask<WeeklyTaskDto>(mapper.ToDL<TRequest, WeeklyTaskDto>(request));
            }
            else if (type.Equals(typeof(MonthlyTaskRequest)))
            {
                return repository.UpdateTask<MonthlyTaskDto>(mapper.ToDL<TRequest, MonthlyTaskDto>(request));
            }
            throw new ArgumentException("Request type " + type.FullName + " is not supported by this repository");
        }

        public void DeleteTask<TRequest>(long id) where TRequest : class, Common.ITaskRequest, new()
        {
            var type = typeof(TRequest);
            if (type.Equals(typeof(OneTimeTaskRequest)))
            {
                repository.DeleteTask<OneTimeTaskDto>(id);
            }
            else if (type.Equals(typeof(DailyTaskRequest)))
            {
                repository.DeleteTask<DailyTaskDto>(id);
            }
            else if (type.Equals(typeof(WeeklyTaskRequest)))
            {
                repository.DeleteTask<WeeklyTaskDto>(id);
            }
            else if (type.Equals(typeof(MonthlyTaskRequest)))
            {
                repository.DeleteTask<MonthlyTaskDto>(id);
            }
            else
            {
                throw new ArgumentException("Request type " + type.FullName + " is not supported by this repository");
            }
        }
    }
}
