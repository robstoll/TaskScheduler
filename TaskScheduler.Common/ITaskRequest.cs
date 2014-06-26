using System;

namespace CH.Tutteli.TaskScheduler.Common
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
