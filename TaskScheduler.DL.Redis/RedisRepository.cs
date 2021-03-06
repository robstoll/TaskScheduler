﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using CH.Tutteli.TaskScheduler.Requests;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace CH.Tutteli.TaskScheduler.DL
{
	public class RedisRepository : IRepository
	{

		private IRedisClientsManager redisClientManager;

		public RedisRepository(IRedisClientsManager theRedisManager)
		{
			redisClientManager = theRedisManager;
		}
        public TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskDto, new() {
            return redisClientManager.ExecAs<TRequest>(x => x.GetById(id));
        }

        public IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskDto, new() {
            return redisClientManager.ExecAs<TRequest>(x => x.GetAll());
        }

		public long CreateTask<TRequest>(TRequest request) where TRequest : class, ITaskDto, new()
		{
			using (var redisClient = redisClientManager.GetClient())
			{
                var redis = redisClient.As<TRequest>();

				if (request.Id == default(long))
				{
					request.Id = redis.GetNextSequence();
					request.DateCreated = DateTime.UtcNow;
                    redis.Store(request);
                    return request.Id;
				}
				else
				{
					throw new ArgumentException("Id provided, when creating a new task.");
				}
			}
		}

        public long UpdateTask<TRequest>(TRequest request) where TRequest : class, ITaskDto, new()
        {
            using (var redisClient = redisClientManager.GetClient())
            {
                var redis = redisClient.As<TRequest>();

                if (request.Id != default(long))
                {
                    var entity = redis.GetById(request.Id) ;
                    if (entity == null) { 
                        throw new ArgumentNullException("Task with given id: "+request.Id+" does not exist.");
                    }
                    request.DateCreated = entity.DateCreated;
                    request.DateUpdated = DateTime.UtcNow;
                    redis.Store(request);
                    return request.Id;
                }
                else
                {
                    throw new ArgumentException("Id not provided, when updating a new task.");
                }
            }
        }

        public void DeleteTask<TRequest>(long id) where TRequest : class, ITaskDto, new()
        {
            using (var redisClient = redisClientManager.GetClient())
			{
                var redis = redisClient.As<TRequest>();
                redis.DeleteById(id);
            }
        }


     
    }
}