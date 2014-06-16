using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Tutteli.TaskScheduler.Requests
{
	public interface ITaskRequest
	{
		long Id { get; set; }

		DateTime DateCreated { get; set; }

		DateTime DateUpdated { get; set; }

		string Name { get; set; }

		string Description { get; set; }

        string CallbackUrl { get; set; }
	}
}
