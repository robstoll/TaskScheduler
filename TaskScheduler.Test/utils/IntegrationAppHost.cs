using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.DL;
using Funq;
using ServiceStack.Common.Utils;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;

namespace CH.Tutteli.TaskScheduler.Test.Utils
{
    public class IntegrationAppHost : AppHostHttpListenerBase
    {
        public IntegrationAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerSoapService).Assembly) { }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
                       new OrmLiteConnectionFactory(PathUtils.MapHostAbsolutePath("~/TaskScheduler-test.sqlite"), SqliteDialect.Provider));
            container.Register<IRepository>(c => new SqlLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ITaskHandler>(c => new TaskHandler(c.Resolve<IRepository>()));
        }

    }
}
