using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using Moq;
using NUnit.Framework;
using ServiceStack.OrmLite;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;
using CH.Tutteli.TaskScheduler.BL;
using ServiceStack.Common.Utils;
using CH.Tutteli.TaskScheduler.DL;
using Funq;
using System.Threading;
using System.Diagnostics;

namespace CH.Tutteli.TaskScheduler.Test
{
    [TestFixture]
    public class TaskSchedulerCallbackTest : AIntegrationTest
    {
        private ITaskHandler taskHandler;

        [SetUp]
        public void SetUp()
        {
            using (var db = appHost.TryResolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<OneTimeTaskRequest>();
                db.DropAndCreateTable<DailyTaskRequest>();
                db.DropAndCreateTable<WeeklyTaskRequest>();
                db.DropAndCreateTable<MonthlyTaskRequest>();
            }
            taskHandler = new TaskHandler(appHost.TryResolve<IScheduler>(),appHost.TryResolve<IRepository>(), appHost.TryResolve<ICallbackVerifier>());
        }
        
        [Test]
        public void PostOneTimeTask_Standard_CallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostMock<OneTimeTaskRequest>();
            var request = new OneTimeTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + "task/one-time/" + id,
                Trigger = DateTime.Now.AddMilliseconds(200)
            };
            
            var response = SendRequest<TaskResponse>(request, client, "POST");
            Thread.Sleep(400);

            mockedTaskHandler.Verify(t => t.Get<OneTimeTaskRequest>(It.Is<OneTimeTaskRequest>(o => o.Id == id)));
        }

        private Mock<ITaskHandler> SetUpPostMock<TRequest>() where TRequest : class, ITaskRequest, new()
        {
            var mockedTaskHandler = GetTaskHandlerMock();
            mockedTaskHandler
                .Setup(t => t.Create<TRequest>(It.IsAny<TRequest>()))
                .Returns<TRequest>(o => taskHandler.Create<TRequest>(o));
            return mockedTaskHandler;
        }

        [Test]
        public void PostDailyTask_Standard_CallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostMock<DailyTaskRequest>();
            var request = new DailyTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                StartDate = DateTime.Now.AddMilliseconds(1000),
                EndDate = DateTime.Now.AddMinutes(2),
                RecursEveryXDays = 1
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            Thread.Sleep(1200);

            mockedTaskHandler.Verify(t => t.Get<OneTimeTaskRequest>(It.Is<OneTimeTaskRequest>(o => o.Id == id)));
        }


        [Test]
        public void PostWeelyTask_Standard_CallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostMock<WeeklyTaskRequest>();
            var request = new WeeklyTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                StartDate = DateTime.Now.AddMilliseconds(1000),
                EndDate = DateTime.Now.AddMinutes(2),
                RecursEveryXWeeks = 1,
                TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DateTime.Now.DayOfWeek }
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            Thread.Sleep(1200);

            mockedTaskHandler.Verify(t => t.Get<OneTimeTaskRequest>(It.Is<OneTimeTaskRequest>(o => o.Id == id)));
        }

        [Test]
        public void PostMonthlyTask_Standard_CallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostMock<MonthlyTaskRequest>();
            var request = new MonthlyTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                StartDate = DateTime.Now.AddMilliseconds(1000),
                EndDate = DateTime.Now.AddMinutes(2),
                RecursOnMonth = new HashSet<EMonth> { (EMonth)DateTime.Now.Month},
                RecursOnDayOfMonth = new HashSet<EDayOfMonth> { (EDayOfMonth) DateTime.Now.Day}
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            Thread.Sleep(1200);

            mockedTaskHandler.Verify(t => t.Get<OneTimeTaskRequest>(It.Is<OneTimeTaskRequest>(o => o.Id == id)));
        }

        [Test]
        public void PostAndPutOneTimeTask_Standard_UpdatedCallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostAndPutMock<OneTimeTaskRequest>();
            var request = new OneTimeTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                Trigger = DateTime.Now.AddSeconds(5)
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = response.Id;
            request.CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.DAILY + "/" + id;
            request.Trigger = DateTime.Now.AddMilliseconds(200);
            SendRequest<TaskResponse>(request, client, "PUT");
            Thread.Sleep(400);

            mockedTaskHandler.Verify(t => t.Get<DailyTaskRequest>(It.Is<DailyTaskRequest>(o => o.Id == id)));
        }

        [Test]
        public void PostAndPutDailyTask_Standard_UpdatedCallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostAndPutMock<DailyTaskRequest>();
            var request = new DailyTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                StartDate = DateTime.Now.AddMilliseconds(5000),
                EndDate = DateTime.Now.AddMinutes(2),
                RecursEveryXDays = 1
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = response.Id;
            request.CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.DAILY + "/" + id;
            request.StartDate = DateTime.Now.AddMilliseconds(1000);
            SendRequest<TaskResponse>(request, client, "PUT");
            Thread.Sleep(1200);

            mockedTaskHandler.Verify(t => t.Get<DailyTaskRequest>(It.Is<DailyTaskRequest>(o => o.Id == id)));
        }


        [Test]
        public void PostAndPutWeeklyTask_Standard_UpdatedCallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostAndPutMock<WeeklyTaskRequest>();
            var request = new WeeklyTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                StartDate = DateTime.Now.AddMilliseconds(5000),
                EndDate = DateTime.Now.AddMinutes(2),
                RecursEveryXWeeks = 1,
                TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DateTime.Now.DayOfWeek }
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = response.Id;
            request.CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.DAILY + "/" + id;
            request.StartDate = DateTime.Now.AddMilliseconds(1000);
            SendRequest<TaskResponse>(request, client, "PUT");
            Thread.Sleep(1200);

            mockedTaskHandler.Verify(t => t.Get<DailyTaskRequest>(It.Is<DailyTaskRequest>(o => o.Id == id)));
        }

        [Test]
        public void PostAndPutMonthlyTask_Standard_UpdatedCallbackWasCalled(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var mockedTaskHandler = SetUpPostAndPutMock<MonthlyTaskRequest>();
            var request = new MonthlyTaskRequest
            {
                Name = "name",
                Description = "descr",
                CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.ONE_TIME + "/" + id,
                StartDate = DateTime.Now.AddMilliseconds(5000),
                EndDate = DateTime.Now.AddMinutes(2),
                RecursOnMonth = new HashSet<EMonth> { (EMonth)DateTime.Now.Month },
                RecursOnDayOfMonth = new HashSet<EDayOfMonth> { (EDayOfMonth)DateTime.Now.Day }
            };

            var response = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = response.Id;
            request.CallbackUrl = BaseUrl + Global.URL_PREFIX + Global.DAILY + "/" + id;
            request.StartDate = DateTime.Now.AddMilliseconds(1000);
            SendRequest<TaskResponse>(request, client, "PUT");
            Thread.Sleep(1200);

            mockedTaskHandler.Verify(t => t.Get<DailyTaskRequest>(It.Is<DailyTaskRequest>(o => o.Id == id)));
        }

        private Mock<ITaskHandler> SetUpPostAndPutMock<TRequest>() where TRequest : class, ITaskRequest, new()
        {
            var mockedTaskHandler = GetTaskHandlerMock();
            mockedTaskHandler
                .Setup(t => t.Create<TRequest>(It.IsAny<TRequest>()))
                .Returns<TRequest>(o => taskHandler.Create<TRequest>(o));
            mockedTaskHandler
                .Setup(t => t.Update<TRequest>(It.IsAny<TRequest>()))
                .Returns<TRequest>(o => taskHandler.Update<TRequest>(o));
            return mockedTaskHandler;
        }

        private Mock<ITaskHandler> GetTaskHandlerMock()
        {
            return Mock.Get<ITaskHandler>(((CallbackAppHost)appHost).taskHandler);
        }

        protected override AppHostHttpListenerBase CreateAppHost()
        {
            return new CallbackAppHost();
        }

        public class CallbackAppHost : AppHostHttpListenerBase
        {
            public ITaskHandler taskHandler;

            public CallbackAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerSoapService).Assembly) { }

            public override void Configure(Container container)
            {
                Routes
                   .Add<OneTimeTaskRequest>(Global.URL_PREFIX + Global.ONE_TIME)
                   .Add<OneTimeTaskRequest>(Global.URL_PREFIX + Global.ONE_TIME + "/{id}")
                   .Add<DailyTaskRequest>(Global.URL_PREFIX + Global.DAILY)
                   .Add<DailyTaskRequest>(Global.URL_PREFIX + Global.DAILY + "/{id}")
                   .Add<WeeklyTaskRequest>(Global.URL_PREFIX + Global.WEEKLY)
                   .Add<WeeklyTaskRequest>(Global.URL_PREFIX + Global.WEEKLY + "/{id}")
                   .Add<MonthlyTaskRequest>(Global.URL_PREFIX + Global.MONTHLY)
                   .Add<MonthlyTaskRequest>(Global.URL_PREFIX + Global.MONTHLY + "/{id}");

                container.Register<IDbConnectionFactory>(
                           new OrmLiteConnectionFactory(PathUtils.MapHostAbsolutePath("~/TaskScheduler-test.sqlite"), SqliteDialect.Provider));
                container.Register<IRepository>(c => new SqlLiteRepository(c.Resolve<IDbConnectionFactory>()));

                var mock = new Mock<ITaskHandler>(MockBehavior.Strict);
                taskHandler = mock.Object;

                var verifier = Mock.Of<ICallbackVerifier>(x => x.IsSecureCallback(It.IsAny<string>()) == true);
                container.Register<IScheduler>(c => new ThreadingTimerScheduler());
                container.Register<ICallbackVerifier>(c => verifier);
                container.Register<ITaskHandler>(c => taskHandler);
            }
        }  
    }
}
