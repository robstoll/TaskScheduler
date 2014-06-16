using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using ServiceStack.Common.Utils;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.WebHost.Endpoints;

namespace CH.Tutteli.TaskScheduler
{
    public class Global : System.Web.HttpApplication
    {
        public static readonly string URL_PREFIX = "/task/";
        public static readonly string ONE_TIME = "one-time";
        public static readonly string DAILY = "daily";
        public static readonly string WEEKLY = "weekly";
        public static readonly string MONTHLY = "monthly";

        public class TaskSchedulerAppHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public TaskSchedulerAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerService).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                Routes
                    .Add<OneTimeTaskRequest>(URL_PREFIX + ONE_TIME)
                    .Add<OneTimeTaskRequest>(URL_PREFIX + ONE_TIME + "/{id}")
                    .Add<DailyTaskRequest>(URL_PREFIX + DAILY)
                    .Add<DailyTaskRequest>(URL_PREFIX + DAILY + "/{id}")
                    .Add<WeeklyTaskRequest>(URL_PREFIX + WEEKLY)
                    .Add<WeeklyTaskRequest>(URL_PREFIX + WEEKLY + "/{id}")
                    .Add<MonthlyTaskRequest>(URL_PREFIX + MONTHLY)
                    .Add<MonthlyTaskRequest>(URL_PREFIX + MONTHLY + "/{id}");


                //Show StackTrace in Web Service Exceptions
                SetConfig(new EndpointHostConfig
                {
                    DebugMode = true,
                    WsdlServiceNamespace = "http://schemas.tutteli.ch/types",
                });

                container.Register<IDbConnectionFactory>(
                    new OrmLiteConnectionFactory(PathUtils.MapHostAbsolutePath("~/TaskScheduler.sqlite"), SqliteDialect.Provider));
                container.Register<IRepository>(c => new SqlLiteRepository(c.Resolve<IDbConnectionFactory>()));

                //container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
                //container.Register<IRepository>(c => new RedisRepository(c.Resolve<IRedisClientsManager>()));

                container.Register<ITaskHandler>(c => new TaskHandler(c.Resolve<IRepository>()));

                using (var db = container.Resolve<IDbConnectionFactory>().Open())
                {
                    db.DropAndCreateTable<OneTimeTaskRequest>();
                    db.DropAndCreateTable<DailyTaskRequest>();
                    db.DropAndCreateTable<WeeklyTaskRequest>();
                    db.DropAndCreateTable<MonthlyTaskRequest>();
                }
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
            int i=0;
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