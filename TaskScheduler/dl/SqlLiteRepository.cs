using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ch.tutteli.taskscheduler.requests;
using ServiceStack.OrmLite;

namespace ch.tutteli.taskscheduler.dl
{
    public class SqlLiteRepository : IRepository
    {
        private IDbConnectionFactory dbConnectionFactory;

        private IDbConnection db;
        private IDbConnection Db
        {
            get { return db ?? (db = dbConnectionFactory.Open()); }
        }

        public SqlLiteRepository(IDbConnectionFactory theDbConnectionFactory)
        {
            dbConnectionFactory = theDbConnectionFactory;
        }

        public IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskRequest, new()
        {
            return Db.Select<TRequest>();
        }

        public TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskRequest, new()
        {
            return Db.GetById<TRequest>(id);
        }

        public long CreateTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
        {
            if (request.Id == default(long))
            {
                request.DateCreated = DateTime.UtcNow;
                Db.Save(request);
                return db.GetLastInsertId();
            }
            else
            {
                throw new ArgumentException("Id provided, tried to create a new task.");
            }
        }

        public long UpdateTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
        {
            if (request.Id != default(long))
            {
                var entity = Db.QueryById<TRequest>(request.Id);
                if (entity == null)
                {
                    throw new ArgumentNullException("Task with given id: " + request.Id + " does not exist.");
                }
                request.DateCreated = entity.DateCreated;
                request.DateUpdated = DateTime.UtcNow;
                Db.Update(request);
                return db.GetLastInsertId();
            }
            else
            {
                throw new ArgumentException("Id provided, tried to create a new task.");
            }
        }

        public void DeleteTask<TRequest>(long id) where TRequest : class, ITaskRequest, new()
        {
            Db.DeleteById<TRequest>(id);
        }

    }
}