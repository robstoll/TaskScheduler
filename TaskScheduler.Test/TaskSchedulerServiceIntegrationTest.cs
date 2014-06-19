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

namespace CH.Tutteli.TaskScheduler.Test
{
    [TestFixture]
    public class TaskSchedulerServiceIntegrationTest : AIntegrationTest
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


        #region Create new task
        
        [Test]
        public void PostOneTimeTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();
            var id = 1;

            var response = SendRequest<TaskResponse>(request, client, "POST", filter =>
                 {
                     Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                     Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.ONE_TIME + "/" + id));
                 });

            Assert.That(response.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostDailyTimeTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var request = TaskRequestHelper.CreateDailyTaskRequest();
            var id = 1;

            var response = SendRequest<TaskResponse>(request, client, "POST", filter =>
            {
                Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.DAILY + "/" + id));
            });

            Assert.That(response.Id, Is.EqualTo(id));
        }


        [Test]
        public void PostWeeklyTimeTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            var id = 1;

            var response = SendRequest<TaskResponse>(request, client, "POST", filter =>
            {
                Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.WEEKLY + "/" + id));
            });

            Assert.That(response.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostMonthlyTimeTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();
            var id = 1;

            var response = SendRequest<TaskResponse>(request, client, "POST", filter =>
            {
                Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.MONTHLY + "/" + id));
            });

            Assert.That(response.Id, Is.EqualTo(id));
        }

        #endregion

        #region Create and update a task
        
        [Test]
        public void PostGetPutGetOneTimeTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");

            request.Id = resultId.Id;
            var result = SendRequest<OneTimeTaskRequest>(request, client, "GET");
            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result, request);

            request.Name="dummy";
            request.Description="hm..";
            request.Trigger = DateTime.Now.AddDays(123);
            var responsePut = SendRequest<TaskResponse>(request, client, "PUT");
            result = SendRequest<OneTimeTaskRequest>(request, client, "GET");
            AssertSame.OneTimeTaskRequest(result, request);
            Assert.That(resultId.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostGetPutGetDailyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateDailyTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = resultId.Id;
            var result = SendRequest<DailyTaskRequest>(request, client, "GET");

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result, request);

            request.Name = "dummy";
            request.Description = "hm..";
            request.RecursEveryXDays = 890;
            var responsePut = SendRequest<TaskResponse>(request, client, "PUT");
            result = SendRequest<DailyTaskRequest>(request, client, "GET");
            
            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result, request);
        }


        [Test]
        public void PostGetPutGetWeeklyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = resultId.Id;
            var result = SendRequest<WeeklyTaskRequest>(request, client, "GET");

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result, request);

            request.Name = "dummy";
            request.Description = "hm..";
            request.RecursEveryXWeeks = 890;
            var responsePut = SendRequest<TaskResponse>(request, client, "PUT");
            result = SendRequest<WeeklyTaskRequest>(request, client, "GET");

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result, request);
        }

        [Test]
        public void PostGetPutGetMonthlyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");
            request.Id = resultId.Id;
            var result = SendRequest<MonthlyTaskRequest>(request, client, "GET");

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result, request);

            request.Name = "dummy";
            request.Description = "hm..";
            request.RecursOnMonth = new HashSet<EMonth>{EMonth.May};
            var responsePut = SendRequest<TaskResponse>(request, client, "PUT");
            result = SendRequest<MonthlyTaskRequest>(request, client, "GET");

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result, request);
        }

        #endregion

        #region Create and delete a task

        [Test]
        public void PostGetDeleteGetOneTimeTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");

            request.Id = resultId.Id;
            var result = SendRequest<OneTimeTaskRequest>(request, client, "GET");
            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result, request);

            using (client)
            {
                client.Delete<TaskResponse>(request);
            }

            try { 
                result = SendRequest<OneTimeTaskRequest>(request, client, "GET");
            }
            catch (WebServiceException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
                Assert.That(ex.ErrorCode, Is.EqualTo("ArgumentNullException"));
                Assert.That(ex.ErrorMessage, Is.StringContaining("Id"));
            }
        }

        [Test]
        public void PostGetDeleteGetDailyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateDailyTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");

            request.Id = resultId.Id;
            var result = SendRequest<DailyTaskRequest>(request, client, "GET");
            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result, request);

            using (client)
            {
                client.Delete<TaskResponse>(request);
            }

            try
            {
                result = SendRequest<DailyTaskRequest>(request, client, "GET");
            }
            catch (WebServiceException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
                Assert.That(ex.ErrorCode, Is.EqualTo("ArgumentNullException"));
                Assert.That(ex.ErrorMessage, Is.StringContaining("Id"));
            }
        }

        [Test]
        public void PostGetDeleteGetWeeklyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");

            request.Id = resultId.Id;
            var result = SendRequest<WeeklyTaskRequest>(request, client, "GET");
            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result, request);

            using (client)
            {
                client.Delete<TaskResponse>(request);
            }

            try
            {
                result = SendRequest<WeeklyTaskRequest>(request, client, "GET");
            }
            catch (WebServiceException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
                Assert.That(ex.ErrorCode, Is.EqualTo("ArgumentNullException"));
                Assert.That(ex.ErrorMessage, Is.StringContaining("Id"));
            }
        }

        [Test]
        public void PostGetDeleteGetMonthlyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase client)
        {
            var id = 1;
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();

            var resultId = SendRequest<TaskResponse>(request, client, "POST");

            request.Id = resultId.Id;
            var result = SendRequest<MonthlyTaskRequest>(request, client, "GET");
            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result, request);

            using (client)
            {
                client.Delete<TaskResponse>(request);
            }

            try
            {
                result = SendRequest<MonthlyTaskRequest>(request, client, "GET");
            }
            catch (WebServiceException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
                Assert.That(ex.ErrorCode, Is.EqualTo("ArgumentNullException"));
                Assert.That(ex.ErrorMessage, Is.StringContaining("Id"));
            }
        }

        #endregion

        protected override AppHostHttpListenerBase CreateAppHost()
        {
            return new IntegrationAppHost();
        }

    }
}
