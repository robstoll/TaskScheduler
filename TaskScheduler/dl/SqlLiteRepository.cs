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

		public long SaveTask<TRequest>(TRequest request) where TRequest : class, ITaskRequest, new()
		{
			if (request.Id == default(long))
			{
				request.DateCreated = DateTime.UtcNow;
			}
			else
			{
				request.DateUpdated = DateTime.UtcNow;
			}
			Db.Save(request);
            return db.GetLastInsertId();
		}

        public IList<TRequest> GetAllTasks<TRequest>() where TRequest : class, ITaskRequest, new()
        {
            return Db.Select<TRequest>();
        }

        public TRequest LoadTask<TRequest>(long id) where TRequest : class, ITaskRequest, new()
        {
            return Db.GetById<TRequest>(id);
        }
    }
}