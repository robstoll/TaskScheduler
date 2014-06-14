using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ch.tutteli.taskscheduler.dl;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.triggers;
using NUnit.Framework;
using ServiceStack.Examples.Tests.Integration;
using ServiceStack.OrmLite;

namespace ch.tutteli.taskscheduler.test.dl
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

        #region SaveTask Nothing Defined

        [Test]
        public void SaveOneTimeTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new OneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveDailyTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new DailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveWeeklyTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new OneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SaveMonthlyTaskRequest_NothingDefined_ReturnId1()
        {
            var task = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(1));
        }

        #endregion

        #region SaveTask TwoTasks
        [Test]
        public void SaveOneTimeTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new OneTimeTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(new OneTimeTaskRequest());
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveDailyTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new DailyTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(new DailyTaskRequest());
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveWeeklyTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new WeeklyTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(new WeeklyTaskRequest());
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void SaveMonthlyTaskRequest_TwoTasks_ReturnId2()
        {
            var task = new MonthlyTaskRequest();


            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(new MonthlyTaskRequest());
            var result = repository.SaveTask(task);

            Assert.That(result, Is.EqualTo(2));
        }

        #endregion

        #region SaveTask different types independent
        [Test]
        public void SaveTask_AllTypesAndNothignDefinedAndOneTimeFirst_ReturnId1AllTheTime()
        {
            var oneTime = new OneTimeTaskRequest();
            var daily = new DailyTaskRequest();
            var weekly = new WeeklyTaskRequest();
            var monthly = new MonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var resultOneTime = repository.SaveTask(oneTime);

            var resultDaily = repository.SaveTask(daily);
            var resultWeekly = repository.SaveTask(weekly);
            var resultMonthly = repository.SaveTask(monthly);

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
            var resultDaily = repository.SaveTask(daily);

            var resultOneTime = repository.SaveTask(oneTime);
            var resultWeekly = repository.SaveTask(weekly);
            var resultMonthly = repository.SaveTask(monthly);

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
            var resultWeekly = repository.SaveTask(weekly);

            var resultDaily = repository.SaveTask(daily);
            var resultOneTime = repository.SaveTask(oneTime);
            var resultMonthly = repository.SaveTask(monthly);

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
            var resultMonthly = repository.SaveTask(monthly);

            var resultWeekly = repository.SaveTask(weekly);
            var resultDaily = repository.SaveTask(daily);
            var resultOneTime = repository.SaveTask(oneTime);

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
            var task = CreateOneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(task);
            var result = repository.GetAllTasks<OneTimeTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSameOneTimeTaskRequest(result[0], task);
        }

        private static OneTimeTaskRequest CreateOneTimeTaskRequest()
        {
            var task = new OneTimeTaskRequest { Name = "test", Description = "descr", Trigger = DateTime.Now };
            return task;
        }

        private static void AssertSameOneTimeTaskRequest(OneTimeTaskRequest result, OneTimeTaskRequest task)
        {
            Assert.That(result.Name, Is.EqualTo(task.Name));
            Assert.That(result.Description, Is.EqualTo(task.Description));
            Assert.That(result.Trigger, Is.EqualTo(task.Trigger));
        }

        [Test]
        public void GetAllDailyTaskRequests_OneDefined_ReturnIt()
        {
            var task = CreateDailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.SaveTask(task);
            var result = repository.GetAllTasks<DailyTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSameDailyTaskRequest(result[0], task);
        }

        private static DailyTaskRequest CreateDailyTaskRequest()
        {
            var task = new DailyTaskRequest
            {
                Name = "test",
                Description = "descr",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
                RecursEveryXDays = 10,
                TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday }
            };
            return task;
        }

        private static void AssertSameDailyTaskRequest(DailyTaskRequest result, DailyTaskRequest task)
        {
            Assert.That(result.Name, Is.EqualTo(task.Name));
            Assert.That(result.Description, Is.EqualTo(task.Description));
            Assert.That(result.StartDate, Is.EqualTo(task.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(task.EndDate));
            Assert.That(result.RecursEveryXDays, Is.EqualTo(task.RecursEveryXDays));
            Assert.That(result.TriggerWhenDayOfWeek, Is.EqualTo(task.TriggerWhenDayOfWeek));
        }

        [Test]
        public void GetAllWeeklyTaskRequests_OneDefined_ReturnIt()
        {
            var task = CreateWeaklyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(task);
            var result = repository.GetAllTasks<WeeklyTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSameWeeklyTaskRequest(result[0], task);
        }

        private static WeeklyTaskRequest CreateWeaklyTaskRequest()
        {
            var task = new WeeklyTaskRequest
            {
                Name = "test",
                Description = "descr",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                TriggerWhenDayOfWeek = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Friday },
                RecursEveryXWeeks = 2
            };
            return task;
        }

        private static void AssertSameWeeklyTaskRequest(WeeklyTaskRequest result, WeeklyTaskRequest task)
        {
            Assert.That(result.Name, Is.EqualTo(task.Name));
            Assert.That(result.Description, Is.EqualTo(task.Description));
            Assert.That(result.StartDate, Is.EqualTo(task.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(task.EndDate));
            Assert.That(result.TriggerWhenDayOfWeek, Is.EqualTo(task.TriggerWhenDayOfWeek));
            Assert.That(result.RecursEveryXWeeks, Is.EqualTo(task.RecursEveryXWeeks));
        }

        [Test]
        public void GetAllMonthlyTaskRequests_OneDefined_ReturnIt()
        {
            var task = CreateMonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            repository.SaveTask(task);
            var result = repository.GetAllTasks<MonthlyTaskRequest>();

            Assert.That(result.Count, Is.EqualTo(1));
            AssertSameMonthlyTaskRequest(result[0], task);
        }

        private static MonthlyTaskRequest CreateMonthlyTaskRequest()
        {
            var task = new MonthlyTaskRequest
            {
                Name = "test",
                Description = "descr",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                RecursOnDayOfMonth = new HashSet<EDayOfMonth> { EDayOfMonth.D1, EDayOfMonth.D15 },
                RecursOnMonth = new HashSet<EMonth> { EMonth.March, EMonth.July },
                RecursOnSpecialDayOfMonth = new Dictionary<EMonthlyOn, IList<DayOfWeek>> { { EMonthlyOn.First, new List<DayOfWeek> { DayOfWeek.Tuesday } } }
            };
            return task;
        }

        private static void AssertSameMonthlyTaskRequest(MonthlyTaskRequest result, MonthlyTaskRequest task)
        {
            Assert.That(result.Name, Is.EqualTo(task.Name));
            Assert.That(result.Description, Is.EqualTo(task.Description));
            Assert.That(result.StartDate, Is.EqualTo(task.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(task.EndDate));
            Assert.That(result.RecursOnDayOfMonth, Is.EqualTo(task.RecursOnDayOfMonth));
            Assert.That(result.RecursOnMonth, Is.EqualTo(task.RecursOnMonth));
            Assert.That(result.RecursOnSpecialDayOfMonth, Is.EqualTo(task.RecursOnSpecialDayOfMonth));
        }

        #endregion

        #region LoadTask nothing defined


        [Test]
        public void LoadOneTimeTask_OneDefined_ReturnIt()
        {
            var task = CreateOneTimeTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.SaveTask(task);
            var result = repository.LoadTask<OneTimeTaskRequest>(id);

            AssertSameOneTimeTaskRequest(result, task);
        }

        [Test]
        public void LoadDailyTask_OneDefined_ReturnIt()
        {
            var task = CreateDailyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.SaveTask(task);
            var result = repository.LoadTask<DailyTaskRequest>(id);

            AssertSameDailyTaskRequest(result, task);
        }


        [Test]
        public void LoadWeeklyTask_OneDefined_ReturnIt()
        {
            var task = CreateWeaklyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.SaveTask(task);
            var result = repository.LoadTask<WeeklyTaskRequest>(id);

            AssertSameWeeklyTaskRequest(result, task);
        }

        [Test]
        public void LoadMonthlyTask_OneDefined_ReturnIt()
        {
            var task = CreateMonthlyTaskRequest();

            var repository = appHost.TryResolve<IRepository>();
            var id = repository.SaveTask(task);
            var result = repository.LoadTask<MonthlyTaskRequest>(id);

            AssertSameMonthlyTaskRequest(result, task);
        }

        #endregion

    }
}

