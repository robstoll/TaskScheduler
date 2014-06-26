using System.Collections.Generic;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.DL.Dtos;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using Moq;
using NUnit.Framework;
using ServiceStack.WebHost.Endpoints;
namespace CH.Tutteli.TaskScheduler.Test
{
    [TestFixture]
    class TaskSchedulerSoapServiceMockedRepositoryTest : ASoapIntegrationTest
    {

        #region validation errors

        [Test]
        public void PostOneTimeTask_NameNullOrEmpty_Return400(
            [Values(null, "")] string name,
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient service)
        {
            var request = new PostOneTimeTask{OneTimeTaskRequest =  TaskHelper.CreateOneTimeTaskRequest()};
            request.OneTimeTaskRequest.Name = name;

            var response = service.PostOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentException"));
            Assert.That(response.ResponseStatus.Message, Is.StringContaining("Name"));
        }

        [Test]
        public void PutOneTimeTask_NameNullOrEmpty_Return400(
            [Values(null, "")] string name,
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient service)
        {
            var request = new PutOneTimeTask { OneTimeTaskRequest = TaskHelper.CreateOneTimeTaskRequest() };
            request.OneTimeTaskRequest.Id = 1;
            request.OneTimeTaskRequest.Name = name;

            var response = service.PutOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentException"));
            Assert.That(response.ResponseStatus.Message, Is.StringContaining("Name"));
        }

        //TODO remaining validation tests

        #endregion

        #region Post - create a new task

        [Test]
        public void PostOneTimeTask_Standard_ReturnIdAsDefinedByMock(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient service)
        {
            var id = 10;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.CreateTask(It.IsAny<OneTimeTaskDto>())).Returns(id);
            var request = new PostOneTimeTask { OneTimeTaskRequest = TaskHelper.CreateOneTimeTaskRequest() };

            var response = service.PostOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.TaskResponse.Id, Is.EqualTo(id));
        }

        //TODO remaining tests

        #endregion

        #region Put - update an existing one

        [Test]
        public void PutOneTimeTask_Standard_ReturnIdAsDefinedByMock(
            [ValueSource("GetDifferentSaopClients")] ISyncReplyClient service)
        {
            var id = 10;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.UpdateTask(It.IsAny<OneTimeTaskDto>())).Returns(id);
            var request = new PutOneTimeTask { OneTimeTaskRequest = TaskHelper.CreateOneTimeTaskRequest() };
            request.OneTimeTaskRequest.Id = id;

            var response = service.PutOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.TaskResponse.Id, Is.EqualTo(id));
        }

        //TODO remaining tests

        #endregion

        private Mock<IRepository> GetRepositoryMock()
        {
            return Mock.Get<IRepository>(((MockedRepositoryAppHost)appHost).repository);
        }

        protected override AppHostHttpListenerBase CreateAppHost()
        {
            return new MockedRepositoryAppHost();
        }
    }
}

