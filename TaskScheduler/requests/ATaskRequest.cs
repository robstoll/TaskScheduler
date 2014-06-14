using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace ch.tutteli.taskscheduler.requests
{

	public abstract class ATaskRequest : ITaskRequest
	{
        [PrimaryKey]
        [AutoIncrement]
		public long Id { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime DateUpdated { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

	}

}