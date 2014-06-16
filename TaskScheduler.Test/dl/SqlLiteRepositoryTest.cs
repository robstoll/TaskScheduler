using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.Triggers;
using Funq;
using NUnit.Framework;
using ServiceStack.Common.Utils;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;

namespace CH.Tutteli.TaskScheduler.Test.DL
{
    [TestFixture]
    public class SqlLiteRepositoryTest : AIntegrationTest
    {
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
        }

        #region SaveTask new task and nothing defined

        [Test]
        public void SaveOneTimeTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new OneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveDailyTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new DailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveWeeklyTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new WeeklyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveMonthlyTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        #endregion

        #region SaveTask new task one task already created before
        [Test]
        public void SaveOneTimeTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new OneTimeTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new OneTimeTaskRequest());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveDailyTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new DailyTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new DailyTaskRequest());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveWeeklyTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new WeeklyTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new WeeklyTaskRequest());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveMonthlyTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new MonthlyTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new MonthlyTaskRequest());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        #endregion

        #region SaveTask  new tasks - different types independent
        [Test]
        public void SaveTask_AllTypesAndNothignDefinedAndOneTimeFirst_ReturnId1AllTheTime()
        {
            var oneTime = new OneTimeTaskRequest();
            var daily = new DailyTaskRequest();
            var weekly = new WeeklyTaskRequest();
            var monthly = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultOneTime = repository.CreateTask(oneTime);

            var resultDaily = repository.CreateTask(daily);
            var resultWeekly = repository.CreateTask(weekly);
            var resultMonthly = repository.CreateTask(monthly);

            Assert.That(resultOneTime, Is.EqualTo(1));
            Assert.That(resultDaily, Is.EqualTo(1));
            Assert.That(resultWeekly, Is.EqualTo(1));
            Assert.That(resultMonthly, Is.EqualTo(1));
        }

        [Test]
        public void SaveTask_AllTypesAndNothignDefinedAndDailyFirst_ReturnId1AllTheTime()
        {
            var oneTime = new OneTimeTaskRequest();
            var daily = new DailyTaskRequest();
            var weekly = new WeeklyTaskRequest();
            var monthly = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultDaily = repository.CreateTask(daily);

            var resultOneTime = repository.CreateTask(oneTime);
            var resultWeekly = repository.CreateTask(weekly);
            var resultMonthly = repository.CreateTask(monthly);

            Assert.That(resultOneTime, Is.EqualTo(1));
            Assert.That(resultDaily, Is.EqualTo(1));
            Assert.That(resultWeekly, Is.EqualTo(1));
            Assert.That(resultMonthly, Is.EqualTo(1));
        }

        [Test]
        public void SaveTask_AllTypesAndNothignDefinedAndWeeklyFirst_ReturnId1AllTheTime()
        {
            var oneTime = new OneTimeTaskRequest();
            var daily = new DailyTaskRequest();
            var weekly = new WeeklyTaskRequest();
            var monthly = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultWeekly = repository.CreateTask(weekly);

            var resultDaily = repository.CreateTask(daily);
            var resultOneTime = repository.CreateTask(oneTime);
            var resultMonthly = repository.CreateTask(monthly);

            Assert.That(resultOneTime, Is.EqualTo(1));
            Assert.That(resultDaily, Is.EqualTo(1));
            Assert.That(resultWeekly, Is.EqualTo(1));
            Assert.That(resultMonthly, Is.EqualTo(1));
        }

        [Test]
        public void SaveTask_AllTypesAndNothignDefinedAndMonthlyFirst_ReturnId1AllTheTime()
        {
            var oneTime = new OneTimeTaskRequest();
            var daily = new DailyTaskRequest();
            var weekly = new WeeklyTaskRequest();
            var monthly = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultMonthly = repository.CreateTask(monthly);

            var resultWeekly = repository.CreateTask(weekly);
            var resultDaily = repository.CreateTask(daily);
            var resultOneTime = repository.CreateTask(oneTime);

            Assert.That(resultOneTime, Is.EqualTo(1));
            Assert.That(resultDaily, Is.EqualTo(1));
            Assert.That(resultWeekly, Is.EqualTo(1));
            Assert.That(resultMonthly, Is.EqualTo(1));
        }

        #endregion

        #region GetAllTask nothing defined

        [Test]
        public void GetAllOneTimeTaskRequests_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<OneTimeTaskRequest>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllDialyTaskRequests_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<DailyTaskRequest>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllWeeklyTaskRequests_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<WeeklyTaskRequest>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllMonthlyTimeTaskRequests_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<MonthlyTaskRequest>();

            Assert.That(result, Is.Empty);
        }
        #endregion

        #region GetAllTask one defined return list with it

        [Test]
        public void GetAllOneTimeTaskRequests_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateOneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<OneTimeTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.OneTimeTaskRequest(result[0], task);
        }

        [Test]
        public void GetAllDailyTaskRequests_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateDailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.GetAllTasks<DailyTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.DailyTaskRequest(result[0], task);
        }
       
        [Test]
        public void GetAllWeeklyTaskRequests_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateWeaklyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<WeeklyTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.WeeklyTaskRequest(result[0], task);
        }



        [Test]
        public void GetAllMonthlyTaskRequests_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateMonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<MonthlyTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.MonthlyTaskRequest(result[0], task);
        }



        #endregion

        #region LoadTask nothing defined


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadOneTimeTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.LoadTask<OneTimeTaskRequest>(0);

            //assert in attribute
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadDailyTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.LoadTask<DailyTaskRequest>(0);

            //assert in attribute
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadWeeklyTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.LoadTask<WeeklyTaskRequest>(0);

            //assert in attribute
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadMonthlyTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.LoadTask<MonthlyTaskRequest>(0);

            //assert in attribute
        }

        #endregion

        #region LoadTask one defined

        [Test]
        public void LoadOneTimeTask_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateOneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<OneTimeTaskRequest>(id);

            AssertSame.OneTimeTaskRequest(result, task);
        }

        [Test]
        public void LoadDailyTask_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateDailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<DailyTaskRequest>(id);

            AssertSame.DailyTaskRequest(result, task);
        }


        [Test]
        public void LoadWeeklyTask_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateWeaklyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<WeeklyTaskRequest>(id);

            AssertSame.WeeklyTaskRequest(result, task);
        }

        [Test]
        public void LoadMonthlyTask_OneDefined_ReturnIt()
        {
            var task = TaskRequestHelper.CreateMonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<MonthlyTaskRequest>(id);

            AssertSame.MonthlyTaskRequest(result, task);
        }

        #endregion

        #region Update an existing Task - aka SaveTask, LoadTask, change it, SaveTask (again), LoadTask

        [Test]
        public void CreateAndUpdateOneTimeTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new OneTimeTaskRequest { Name = "bla", CallbackUrl = "callback", Trigger = DateTime.Now.AddDays(1) };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<OneTimeTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.OneTimeTaskRequest(result, task);

            task = TaskRequestHelper.CreateOneTimeTaskRequest();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<OneTimeTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.OneTimeTaskRequest(result, task);
        }


        [Test]
        public void CreateAndUpdateDailyTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new DailyTaskRequest { Name = "bla", CallbackUrl = "callback", RecursEveryXDays = 12 };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<DailyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.DailyTaskRequest(result, task);

            task = TaskRequestHelper.CreateDailyTaskRequest();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<DailyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.DailyTaskRequest(result, task);
        }

        [Test]
        public void CreateAndUpdateWeeklyTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new WeeklyTaskRequest { Name = "bla", CallbackUrl = "callback", RecursEveryXWeeks = 98 };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<WeeklyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.WeeklyTaskRequest(result, task);

            task = TaskRequestHelper.CreateWeaklyTaskRequest();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<WeeklyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.WeeklyTaskRequest(result, task);
        }

        [Test]
        public void CreateAndUpdateMonthlyTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new MonthlyTaskRequest { Name = "bla", CallbackUrl = "callback", RecursOnMonth = new HashSet<EMonth> { EMonth.August } };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<MonthlyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.MonthlyTaskRequest(result, task);

            task = TaskRequestHelper.CreateMonthlyTaskRequest();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<MonthlyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.MonthlyTaskRequest(result, task);
        }

        #endregion

        #region Delete an existing Task - aka SaveTask, DeleteTask, make sure it does not exist anymore

        [Test]
        public void CreateAndDeleteOneTimeTask_Standard_CannotLoadTheTaskAnymore()
        {
            var task = TaskRequestHelper.CreateOneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<OneTimeTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.OneTimeTaskRequest(result, task);

            repository.DeleteTask<OneTimeTaskRequest>(resultId);

            try
            {
                repository.LoadTask<OneTimeTaskRequest>(resultId);
                Assert.Fail("Could load the task after it was deleted");
            }
            catch (ArgumentNullException)
            {
                //good, should be like that
            }
        }


        [Test]
        public void CreateAndDeleteDailyTask_Standard_CannotLoadTheTaskAnymore()
        {
            var task = TaskRequestHelper.CreateDailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<DailyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.DailyTaskRequest(result, task);

            repository.DeleteTask<DailyTaskRequest>(resultId);

            try
            {
                repository.LoadTask<DailyTaskRequest>(resultId);
                Assert.Fail("Could load the task after it was deleted");
            }
            catch (ArgumentNullException)
            {
                //good, should be like that
            }
        }

        [Test]
        public void CreateAndDeleteWeeklyTask_Standard_CannotLoadTheTaskAnymore()
        {
            var task = TaskRequestHelper.CreateWeaklyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<WeeklyTaskRequest>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.WeeklyTaskRequest(result, task);

            repository.DeleteTask<WeeklyTaskRequest>(resultId);

            try
            {
                repository.LoadTask<WeeklyTaskRequest>(resultId);
                Assert.Fail("Could load the task after it was deleted");
            }
            catch (ArgumentNullException)
            {
                //good, should be like that
            }
        }

        [Test]
        public void CreateAndDeleteMonthlyTask_Standard_CannotLoadTheTaskAnymore()
        {
            var task = TaskRequestHelper.CreateMonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<MonthlyTaskRequest >(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.MonthlyTaskRequest(result, task);

            repository.DeleteTask<MonthlyTaskRequest>(resultId);

            try
            {
                repository.LoadTask<MonthlyTaskRequest>(resultId);
                Assert.Fail("Could load the task after it was deleted");
            }
            catch (ArgumentNullException)
            {
                //good, should be like that
            }
        }

        #endregion

        protected override AppHostHttpListenerBase CreateAppHost()
        {
            return new SqlLiteAppHost();
        }
    }

    public class SqlLiteAppHost : AppHostHttpListenerBase
    {

        public SqlLiteAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerSoapService).Assembly) { }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
                       new OrmLiteConnectionFactory(PathUtils.MapHostAbsolutePath("~/TaskScheduler-test.sqlite"), SqliteDialect.Provider));
            container.Register<IRepository>(c => new SqlLiteRepository(c.Resolve<IDbConnectionFactory>()));
            container.Register<ITaskHandler>(c => new TaskHandler(c.Resolve<IRepository>()));
        }
    }
}

