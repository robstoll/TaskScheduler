using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.tutteli.taskscheduler.bl;
using ch.tutteli.taskscheduler.dl;
using Funq;
using Moq;
using ServiceStack.WebHost.Endpoints;

namespace ch.tutteli.taskscheduler.test.utils
{
    public class MockedRepositoryAppHost : AppHostHttpListenerBase
    {

        public IRepository repository;

        public MockedRepositoryAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerSoapService).Assembly) { }

        public override void Configure(Container container)
        {
            repository = Mock.Of<IRepository>();
            container.Register<IRepository>(c => repository);
            container.Register<ITaskHandler>(c => new TaskHandler(c.Resolve<IRepository>()));
        }

    }
}
