using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace ch.tutteli.taskscheduler.dl
{
	public class RedisRepository : IRepository
	{

		private IRedisClientsManager redisClientManager;

		public RedisRepository(IRedisClientsManager theRedisManager)
		{
			redisClientManager = theRedisManager;
		}
        public TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskRequest, new() {
            return redisClientManager.ExecAs<TRequest>(x => x.GetById(id));
        }

        public IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskRequest, new() {
            return redisClientManager.ExecAs<TRequest>(x => x.GetAll());
        }

		public long SaveTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
		{
			using (var redisClient = redisClientManager.GetClient())
			{
                var redis = redisClient.As<TRequest>();

				if (request.Id == default(long))
				{
					request.Id = redis.GetNextSequence();
					request.DateCreated = DateTime.UtcNow;
				}
				else
				{
					request.DateUpdated = DateTime.UtcNow;
				}

				redis.Store(request);
				return request.Id;
			}
		}

        public void DeleteTask<TRequest>(long id) where TRequest : class, ITaskRequest, new()
        {
            using (var redisClient = redisClientManager.GetClient())
			{
                var redis = redisClient.As<TRequest>();
                redis.DeleteById(id);
            }
        }
    }
}