
using System;
using System.Collections.Generic;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using CH.Tutteli.TaskScheduler.BL.Triggers;
using Moq;
using NUnit.Framework;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Dtos;
namespace CH.Tutteli.TaskScheduler.Test
{
    [TestFixture]
    class TaskSchedulerSoapServiceIntergationTest : ASoapIntegrationTest
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

        #region POST

        [Test]
        public void PostOneTimeTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request =new PostOneTimeTask{OneTimeTaskRequest= TaskHelper.CreateOneTimeTaskRequest()};

            var response = client.PostOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.TaskResponse.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostDailyTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = new PostDailyTask{DailyTaskRequest = TaskHelper.CreateDailyTaskRequest()};

            var response = client.PostDailyTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.TaskResponse.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostWeeklyTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = new PostWeeklyTask{WeeklyTaskRequest = TaskHelper.CreateWeaklyTaskRequest()};

            var response = client.PostWeeklyTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.TaskResponse.Id, Is.EqualTo(id));
        }

        [Test]
        public void PostMonthlyTask_NothingDefined_ReturnId1(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;
            var request = new PostMonthlyTask{MonthlyTaskRequest= TaskHelper.CreateMonthlyTaskRequest()};

            var response = client.PostMonthlyTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.TaskResponse.Id, Is.EqualTo(id));
        }

        #endregion

        #region Create and update task


        [Test]
        public void PostGetPutGetOneTimeTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostOneTimeTask{OneTimeTaskRequest = TaskHelper.CreateOneTimeTaskRequest()};
            var resultId = client.PostOneTimeTask(postRequest);
            var getRequest = new GetOneTimeTask { Id = resultId.TaskResponse.Id };
            var result = client.GetOneTimeTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result.OneTimeTaskRequest, postRequest.OneTimeTaskRequest);

            var putRequest = new PutOneTimeTask{OneTimeTaskRequest = postRequest.OneTimeTaskRequest};
            putRequest.OneTimeTaskRequest.Id = resultId.TaskResponse.Id;
            putRequest.OneTimeTaskRequest.Name = "dummy";
            putRequest.OneTimeTaskRequest.Description = "hm..";
            putRequest.OneTimeTaskRequest.Trigger = DateTime.Now.AddDays(123);
            var responsePut = client.PutOneTimeTask(putRequest);
            result = client.GetOneTimeTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result.OneTimeTaskRequest, putRequest.OneTimeTaskRequest);
        }

        [Test]
        public void PostGetPutGetDailyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostDailyTask{DailyTaskRequest = TaskHelper.CreateDailyTaskRequest()};
            var resultId = client.PostDailyTask(postRequest);
            var getRequest = new GetDailyTask { Id = resultId.TaskResponse.Id };
            var result = client.GetDailyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result.DailyTaskRequest, postRequest.DailyTaskRequest);

            var putRequest = new PutDailyTask{DailyTaskRequest = postRequest.DailyTaskRequest };
            putRequest.DailyTaskRequest.Id = resultId.TaskResponse.Id;
            putRequest.DailyTaskRequest.Name = "dummy";
            putRequest.DailyTaskRequest.Description = "hm..";
            putRequest.DailyTaskRequest.RecursEveryXDays = 987;
            var responsePut = client.PutDailyTask(putRequest);
            result = client.GetDailyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result.DailyTaskRequest, putRequest.DailyTaskRequest);
        }

        [Test]
        public void PostGetPutGetWeeklyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostWeeklyTask{WeeklyTaskRequest = TaskHelper.CreateWeaklyTaskRequest()};
            var resultId = client.PostWeeklyTask(postRequest);
            var getRequest = new GetWeeklyTask { Id = resultId.TaskResponse.Id };
            var result = client.GetWeeklyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result.WeeklyTaskRequest, postRequest.WeeklyTaskRequest);

            var putRequest = new PutWeeklyTask{WeeklyTaskRequest = postRequest.WeeklyTaskRequest};
            putRequest.WeeklyTaskRequest.Id = resultId.TaskResponse.Id;
            putRequest.WeeklyTaskRequest.Name = "dummy";
            putRequest.WeeklyTaskRequest.Description = "hm..";
            putRequest.WeeklyTaskRequest.RecursEveryXWeeks = 784;
            var responsePut = client.PutWeeklyTask(putRequest);
            result = client.GetWeeklyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result.WeeklyTaskRequest, putRequest.WeeklyTaskRequest);
        }

        [Test]
        public void PostGetPutGetMonthlyTask_Standard_ReturnTwiceTheSameIdAndObject(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostMonthlyTask{MonthlyTaskRequest = TaskHelper.CreateMonthlyTaskRequest()};
            var resultId = client.PostMonthlyTask(postRequest);
            var getRequest = new GetMonthlyTask { Id = resultId.TaskResponse.Id };
            var result = client.GetMonthlyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result.MonthlyTaskRequest, postRequest.MonthlyTaskRequest);

            var putRequest = new PutMonthlyTask {MonthlyTaskRequest = postRequest.MonthlyTaskRequest};
            putRequest.MonthlyTaskRequest.Id = resultId.TaskResponse.Id;
            putRequest.MonthlyTaskRequest.Name = "dummy";
            putRequest.MonthlyTaskRequest.Description = "hm..";
            putRequest.MonthlyTaskRequest.RecursOnMonth = new HashSet<EMonth>{EMonth.April};
            var responsePut = client.PutMonthlyTask(putRequest);
            result = client.GetMonthlyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result.MonthlyTaskRequest, putRequest.MonthlyTaskRequest);
        }

        #endregion

        #region create and delete task

        [Test]
        public void PostGetDeleteGetOneTimeTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostOneTimeTask{OneTimeTaskRequest = TaskHelper.CreateOneTimeTaskRequest()};
            var resultId = client.PostOneTimeTask(postRequest);
            var getRequest = new GetOneTimeTask { Id = resultId.TaskResponse.Id };
            var result = client.GetOneTimeTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.OneTimeTaskRequest(result.OneTimeTaskRequest, postRequest.OneTimeTaskRequest);


            client.DeleteOneTimeTask(new DeleteOneTimeTask { Id = resultId.TaskResponse.Id });
            result = client.GetOneTimeTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }

        [Test]
        public void PostGetDeleteGetDailyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostDailyTask{DailyTaskRequest = TaskHelper.CreateDailyTaskRequest()};
            var resultId = client.PostDailyTask(postRequest);
            var getRequest = new GetDailyTask { Id = resultId.TaskResponse.Id };
            var result = client.GetDailyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.DailyTaskRequest(result.DailyTaskRequest, postRequest.DailyTaskRequest);


            client.DeleteDailyTask(new DeleteDailyTask { Id = resultId.TaskResponse.Id });
            result = client.GetDailyTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }

        [Test]
        public void PostGetDeleteGetWeeklyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest =new PostWeeklyTask{WeeklyTaskRequest=TaskHelper.CreateWeaklyTaskRequest()};
            var resultId = client.PostWeeklyTask(postRequest);
            var getRequest = new GetWeeklyTask { Id = resultId.TaskResponse.Id };
            var result = client.GetWeeklyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.WeeklyTaskRequest(result.WeeklyTaskRequest, postRequest.WeeklyTaskRequest);


            client.DeleteWeeklyTask(new DeleteWeeklyTask { Id = resultId.TaskResponse.Id });
            result = client.GetWeeklyTask(getRequest);

            Assert.That(result.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentNullException"));
        }

        [Test]
        public void PostGetDeleteGetMonthlyTask_Standard_TaskDoesNotExistAnymore(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient client)
        {
            var id = 1;

            var postRequest = new PostMonthlyTask{MonthlyTaskRequest =TaskHelper.CreateMonthlyTaskRequest()};
            var resultId = client.PostMonthlyTask(postRequest);
            var getRequest = new GetMonthlyTask { Id = resultId.TaskResponse.Id };
            var result = client.GetMonthlyTask(getRequest);

            Assert.That(resultId.TaskResponse.Id, Is.EqualTo(id));
            AssertSame.MonthlyTaskRequest(result.MonthlyTaskRequest, postRequest.MonthlyTaskRequest);


            client.DeleteMonthlyTask(new DeleteMonthlyTask { Id = resultId.TaskResponse.Id });
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



