using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.BLDLMapper;
using CH.Tutteli.TaskScheduler.BLDLMapper.Interfaces;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using Funq;
using Moq;
using ServiceStack.WebHost.Endpoints;
using TaskScheduler.BLDLMapper.Interfaces;

namespace CH.Tutteli.TaskScheduler.Test.Utils
{
    public class MockedRepositoryAppHost : AppHostHttpListenerBase
    {

        public IRepository repository;

        public MockedRepositoryAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerSoapService).Assembly) { }

        public override void Configure(Container container)
        {
            repository = Mock.Of<IRepository>();
            container.Register<IRepository>(c => repository);

            container.Register<IScheduler>(c => new ThreadingTimerScheduler());
            container.Register<ICallbackVerifier>(c => new HardCodedCallbackVerifier());
            container.Register<IMapper>(c => new Mapper());
            container.Register<IBLRepository>(c => new BLRepository(c.Resolve<IRepository>(), c.Resolve<IMapper>()));

            container.Register<ITaskHandler>(c => new TaskHandler(
                c.Resolve<IScheduler>(),
                c.Resolve<ICallbackVerifier>(),
                c.Resolve<IBLRepository>()
            ));
        }

    }
}
