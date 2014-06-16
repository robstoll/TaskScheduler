
using System;
using System.Collections.Generic;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.Triggers;
using Moq;
using NUnit.Framework;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;
namespace CH.Tutteli.TaskScheduler.Test
{
    [TestFixture]
    class TaskSchedulerSoapServiceIntergationTest : AIntegrationTest
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

        #region POST

        [Test]
        public void PostOneTimeTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = TaskRequestHelper.InitOneTimeTaskRequest(new PostOneTimeTask());

            var response = client.PostOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostDailyTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = TaskRequestHelper.InitDailyTaskRequest(new PostDailyTask());

            var response = client.PostDailyTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostWeeklyTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = TaskRequestHelper.InitWeeklyTaskRequest(new PostWeeklyTask());

            var response = client.PostWeeklyTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostMonthlyTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = TaskRequestHelper.InitMonthlyTaskRequest(new PostMonthlyTask());

            var response = client.PostMonthlyTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.Id, Is.EqualTo(id));
        }

        #endregion

        #region Create and update task


        [Test]
        public void PostGetPutGetOneTimeTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitOneTimeTaskRequest(new PostOneTimeTask());
            var resultId = client.PostOneTimeTask(postRequest);
            var getRequest = new GetOneTimeTask { Id = resultId.Id };
            var result = client.GetOneTimeTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result.OneTimeTaskRequest, postRequest);

            var putRequest = TaskRequestHelper.InitOneTimeTaskRequest(new PutOneTimeTask());
            putRequest.Id = resultId.Id;
            putRequest.Name = "dummy";
            putRequest.Description = "hm..";
            putRequest.Trigger = DateTime.Now.AddDays(123);
            var responsePut = client.PutOneTimeTask(putRequest);
            result = client.GetOneTimeTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result.OneTimeTaskRequest, putRequest);
        }

        [Test]
        public void PostGetPutGetDailyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitDailyTaskRequest(new PostDailyTask());
            var resultId = client.PostDailyTask(postRequest);
            var getRequest = new GetDailyTask { Id = resultId.Id };
            var result = client.GetDailyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result.DailyTaskRequest, postRequest);

            var putRequest = TaskRequestHelper.InitDailyTaskRequest(new PutDailyTask());
            putRequest.Id = resultId.Id;
            putRequest.Name = "dummy";
            putRequest.Description = "hm..";
            putRequest.RecursEveryXDays = 987;
            var responsePut = client.PutDailyTask(putRequest);
            result = client.GetDailyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result.DailyTaskRequest, putRequest);
        }

        [Test]
        public void PostGetPutGetWeeklyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitWeeklyTaskRequest(new PostWeeklyTask());
            var resultId = client.PostWeeklyTask(postRequest);
            var getRequest = new GetWeeklyTask { Id = resultId.Id };
            var result = client.GetWeeklyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result.WeeklyTaskRequest, postRequest);

            var putRequest = TaskRequestHelper.InitWeeklyTaskRequest(new PutWeeklyTask());
            putRequest.Id = resultId.Id;
            putRequest.Name = "dummy";
            putRequest.Description = "hm..";
            putRequest.RecursEveryXWeeks = 784;
            var responsePut = client.PutWeeklyTask(putRequest);
            result = client.GetWeeklyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result.WeeklyTaskRequest, putRequest);
        }

        [Test]
        public void PostGetPutGetMonthlyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitMonthlyTaskRequest(new PostMonthlyTask());
            var resultId = client.PostMonthlyTask(postRequest);
            var getRequest = new GetMonthlyTask { Id = resultId.Id };
            var result = client.GetMonthlyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result.MonthlyTaskRequest, postRequest);

            var putRequest = TaskRequestHelper.InitMonthlyTaskRequest(new PutMonthlyTask());
            putRequest.Id = resultId.Id;
            putRequest.Name = "dummy";
            putRequest.Description = "hm..";
            putRequest.RecursOnMonth = new HashSet<EMonth>{EMonth.April};
            var responsePut = client.PutMonthlyTask(putRequest);
            result = client.GetMonthlyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result.MonthlyTaskRequest, putRequest);
        }

        #endregion

        #region create and delete task

        [Test]
        public void PostGetDeleteGetOneTimeTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitOneTimeTaskRequest(new PostOneTimeTask());
            var resultId = client.PostOneTimeTask(postRequest);
            var getRequest = new GetOneTimeTask { Id = resultId.Id };
            var result = client.GetOneTimeTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result.OneTimeTaskRequest, postRequest);


            client.DeleteOneTimeTask(new DeleteOneTimeTask { Id = resultId.Id });
            result = client.GetOneTimeTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }

        [Test]
        public void PostGetDeleteGetDailyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitDailyTaskRequest(new PostDailyTask());
            var resultId = client.PostDailyTask(postRequest);
            var getRequest = new GetDailyTask { Id = resultId.Id };
            var result = client.GetDailyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result.DailyTaskRequest, postRequest);


            client.DeleteDailyTask(new DeleteDailyTask { Id = resultId.Id });
            result = client.GetDailyTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }

        [Test]
        public void PostGetDeleteGetWeeklyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitWeeklyTaskRequest(new PostWeeklyTask());
            var resultId = client.PostWeeklyTask(postRequest);
            var getRequest = new GetWeeklyTask { Id = resultId.Id };
            var result = client.GetWeeklyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result.WeeklyTaskRequest, postRequest);


            client.DeleteWeeklyTask(new DeleteWeeklyTask { Id = resultId.Id });
            result = client.GetWeeklyTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }

        [Test]
        public void PostGetDeleteGetMonthlyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = TaskRequestHelper.InitMonthlyTaskRequest(new PostMonthlyTask());
            var resultId = client.PostMonthlyTask(postRequest);
            var getRequest = new GetMonthlyTask { Id = resultId.Id };
            var result = client.GetMonthlyTask(getRequest);

            Assert.That(resultId.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result.MonthlyTaskRequest, postRequest);


            client.DeleteMonthlyTask(new DeleteMonthlyTask { Id = resultId.Id });
            result = client.GetMonthlyTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }


        #endregion

        protected override AppHostHttpListenerBase CreateAppHost()
        {
            return new IntegrationAppHost();
        }
    }
}



