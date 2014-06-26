using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Dtos;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using CH.Tutteli.TaskScheduler.DL.SqLite;
using CH.Tutteli.TaskScheduler.Test.Utils;
using Funq;
using NUnit.Framework;
using ServiceStack.Common.Utils;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;

namespace CH.Tutteli.TaskScheduler.Test.DL
{
    [TestFixture]
    public class SqLiteRepositoryTest : AIntegrationTest
    {
        [SetUp]
        public void SetUp()
        {
            using (var db = appHost.TryResolve<IDbConnectionFactory>().Open())
            {
                db.DropAndCreateTable<OneTimeTaskDto>();
                db.DropAndCreateTable<DailyTaskDto>();
                db.DropAndCreateTable<WeeklyTaskDto>();
                db.DropAndCreateTable<MonthlyTaskDto>();
            }
        }

        #region SaveTask new task and nothing defined

        [Test]
        public void SaveOneTimeTaskDto_NothingDefined_ReturnId1()
        {
            var task = new OneTimeTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveDailyTaskDto_NothingDefined_ReturnId1()
        {
            var task = new DailyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveWeeklyTaskDto_NothingDefined_ReturnId1()
        {
            var task = new WeeklyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveMonthlyTaskDto_NothingDefined_ReturnId1()
        {
            var task = new MonthlyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        #endregion

        #region SaveTask new task one task already created before
        [Test]
        public void SaveOneTimeTaskDto_TwoTasks_ReturnId2()
        {
            var task = new OneTimeTaskDto();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new OneTimeTaskDto());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveDailyTaskDto_TwoTasks_ReturnId2()
        {
            var task = new DailyTaskDto();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new DailyTaskDto());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveWeeklyTaskDto_TwoTasks_ReturnId2()
        {
            var task = new WeeklyTaskDto();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new WeeklyTaskDto());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveMonthlyTaskDto_TwoTasks_ReturnId2()
        {
            var task = new MonthlyTaskDto();


            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(new MonthlyTaskDto());
            var result = repository.CreateTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        #endregion

        #region SaveTask  new tasks - different types independent
        [Test]
        public void SaveTask_AllTypesAndNothignDefinedAndOneTimeFirst_ReturnId1AllTheTime()
        {
            var oneTime = new OneTimeTaskDto();
            var daily = new DailyTaskDto();
            var weekly = new WeeklyTaskDto();
            var monthly = new MonthlyTaskDto();

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
            var oneTime = new OneTimeTaskDto();
            var daily = new DailyTaskDto();
            var weekly = new WeeklyTaskDto();
            var monthly = new MonthlyTaskDto();

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
            var oneTime = new OneTimeTaskDto();
            var daily = new DailyTaskDto();
            var weekly = new WeeklyTaskDto();
            var monthly = new MonthlyTaskDto();

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
            var oneTime = new OneTimeTaskDto();
            var daily = new DailyTaskDto();
            var weekly = new WeeklyTaskDto();
            var monthly = new MonthlyTaskDto();

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
        public void GetAllOneTimeTaskDtos_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<OneTimeTaskDto>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllDialyTaskDtos_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<DailyTaskDto>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllWeeklyTaskDtos_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<WeeklyTaskDto>();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllMonthlyTimeTaskDtos_NothingDefined_ReturnEmptyList()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.GetAllTasks<MonthlyTaskDto>();

            Assert.That(result, Is.Empty);
        }
        #endregion

        #region GetAllTask one defined return list with it

        [Test]
        public void GetAllOneTimeTaskDtos_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateOneTimeTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<OneTimeTaskDto>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.OneTimeTaskDto(result[0], task);
        }

        [Test]
        public void GetAllDailyTaskDtos_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateDailyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<DailyTaskDto>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.DailyTaskDto(result[0], task);
        }
       
        [Test]
        public void GetAllWeeklyTaskDtos_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateWeaklyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<WeeklyTaskDto>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.WeeklyTaskDto(result[0], task);
        }



        [Test]
        public void GetAllMonthlyTaskDtos_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateMonthlyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            repository.CreateTask(task);
            var result = repository.GetAllTasks<MonthlyTaskDto>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSame.MonthlyTaskDto(result[0], task);
        }



        #endregion

        #region LoadTask nothing defined


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadOneTimeTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            repository.LoadTask<OneTimeTaskDto>(0);

            //assert in attribute
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadDailyTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            repository.LoadTask<DailyTaskDto>(0);

            //assert in attribute
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadWeeklyTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            repository.LoadTask<WeeklyTaskDto>(0);

            //assert in attribute
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadMonthlyTask_NothingDefined_ThrowArgumentNullException()
        {
            //no arrange needed

            var repository = appHost.TryResolve<IRepository>();
            repository.LoadTask<MonthlyTaskDto>(0);

            //assert in attribute
        }

        #endregion

        #region LoadTask one defined

        [Test]
        public void LoadOneTimeTask_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateOneTimeTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<OneTimeTaskDto>(id);

            AssertSame.OneTimeTaskDto(result, task);
        }

        [Test]
        public void LoadDailyTask_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateDailyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<DailyTaskDto>(id);

            AssertSame.DailyTaskDto(result, task);
        }


        [Test]
        public void LoadWeeklyTask_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateWeaklyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<WeeklyTaskDto>(id);

            AssertSame.WeeklyTaskDto(result, task);
        }

        [Test]
        public void LoadMonthlyTask_OneDefined_ReturnIt()
        {
            var task = TaskHelper.CreateMonthlyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.CreateTask(task);
            var result = repository.LoadTask<MonthlyTaskDto>(id);

            AssertSame.MonthlyTaskDto(result, task);
        }

        #endregion

        #region Update an existing Task - aka SaveTask, LoadTask, change it, SaveTask (again), LoadTask

        [Test]
        public void CreateAndUpdateOneTimeTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new OneTimeTaskDto { Name = "bla", CallbackUrl = "callback", Trigger = DateTime.Now.AddDays(1) };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<OneTimeTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.OneTimeTaskDto(result, task);

            task = TaskHelper.CreateOneTimeTaskDto();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<OneTimeTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.OneTimeTaskDto(result, task);
        }


        [Test]
        public void CreateAndUpdateDailyTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new DailyTaskDto { Name = "bla", CallbackUrl = "callback", RecursEveryXDays = 12 };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<DailyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.DailyTaskDto(result, task);

            task = TaskHelper.CreateDailyTaskDto();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<DailyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.DailyTaskDto(result, task);
        }

        [Test]
        public void CreateAndUpdateWeeklyTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new WeeklyTaskDto { Name = "bla", CallbackUrl = "callback", RecursEveryXWeeks = 98 };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<WeeklyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.WeeklyTaskDto(result, task);

            task = TaskHelper.CreateWeaklyTaskDto();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<WeeklyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.WeeklyTaskDto(result, task);
        }

        [Test]
        public void CreateAndUpdateMonthlyTask_Standard_ReturnTwiceTheSameIdAndObject()
        {
            var task = new MonthlyTaskDto { Name = "bla", CallbackUrl = "callback", RecursOnMonth = new HashSet<EMonth> { EMonth.August } };

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<MonthlyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.MonthlyTaskDto(result, task);

            task = TaskHelper.CreateMonthlyTaskDto();
            task.Id = resultId;
            resultId = repository.UpdateTask(task);
            result = repository.LoadTask<MonthlyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.MonthlyTaskDto(result, task);
        }

        #endregion

        #region Delete an existing Task - aka SaveTask, DeleteTask, make sure it does not exist anymore

        [Test]
        public void CreateAndDeleteOneTimeTask_Standard_CannotLoadTheTaskAnymore()
        {
            var task = TaskHelper.CreateOneTimeTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<OneTimeTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.OneTimeTaskDto(result, task);

            repository.DeleteTask<OneTimeTaskDto>(resultId);

            try
            {
                repository.LoadTask<OneTimeTaskDto>(resultId);
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
            var task = TaskHelper.CreateDailyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<DailyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.DailyTaskDto(result, task);

            repository.DeleteTask<DailyTaskDto>(resultId);

            try
            {
                repository.LoadTask<DailyTaskDto>(resultId);
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
            var task = TaskHelper.CreateWeaklyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<WeeklyTaskDto>(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.WeeklyTaskDto(result, task);

            repository.DeleteTask<WeeklyTaskDto>(resultId);

            try
            {
                repository.LoadTask<WeeklyTaskDto>(resultId);
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
            var task = TaskHelper.CreateMonthlyTaskDto();

            var repository = appHost.TryResolve<IRepository>();
            var resultId = repository.CreateTask(task);
            var result = repository.LoadTask<MonthlyTaskDto >(resultId);

            Assert.That(resultId, Is.EqualTo(1));
            AssertSame.MonthlyTaskDto(result, task);

            repository.DeleteTask<MonthlyTaskDto>(resultId);

            try
            {
                repository.LoadTask<MonthlyTaskDto>(resultId);
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

        public SqlLiteAppHost() : base("Task Scheduler Web Services", typeof(SqLiteRepository).Assembly) { }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(
                       new OrmLiteConnectionFactory(PathUtils.MapHostAbsolutePath("~/TaskScheduler-test.sqlite"), SqliteDialect.Provider));
            container.Register<IRepository>(c => new SqLiteRepository(c.Resolve<IDbConnectionFactory>()));
        }
    }
}

