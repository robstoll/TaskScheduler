using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.WebHost.Endpoints;

namespace ch.tutteli.taskscheduler
{
	public class Global : System.Web.HttpApplication
	{

		public class TaskSchedulerAppHost : AppHostBase
		{
			//Tell Service Stack the name of your application and where to find your web services
			public TaskSchedulerAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerService).Assembly) { }

			public override void Configure(Funq.Container container)
			{
				//register any dependencies your services use, e.g:
				//container.Register<ICacheClient>(new MemoryCacheClient());
			}
		}

		//Initialize your application singleton
		protected void Application_Start(object sender, EventArgs e)
		{
			new TaskSchedulerAppHost().Init();
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}