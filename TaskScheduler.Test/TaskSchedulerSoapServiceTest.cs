
using System.Collections.Generic;
using ch.tutteli.taskscheduler.dl;
using ch.tutteli.taskscheduler.requests;
using ch.tutteli.taskscheduler.test.utils;
using Moq;
using NUnit.Framework;
using ServiceStack.Examples.Tests.Integration;
using ServiceStack.WebHost.Endpoints;
namespace ch.tutteli.taskscheduler.test
{
    [TestFixture]
    class TaskSchedulerSoapServiceTest : AIntegrationTest
    {

        #region validation errors

        [Test]
        public void PostOneTimeTask_NameNullOrEmpty_Return400(
            [Values(null, "")] string name,
            [ValueSource("GetDifferentServices")] ISyncReplyClient service)
        {
            var request = TaskRequestHelper.InitOneTimeTaskRequest(new PostOneTimeTask());
            request.Name = name;

            var response = service.PostOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentException"));
            Assert.That(response.ResponseStatus.Message, Is.StringContaining("Name"));
        }

        [Test]
        public void PutOneTimeTask_NameNullOrEmpty_Return400(
            [Values(null, "")] string name,
            [ValueSource("GetDifferentServices")] ISyncReplyClient service)
        {
            var request = TaskRequestHelper.InitOneTimeTaskRequest(new PutOneTimeTask());
            request.Id = 1;
            request.Name = name;

            var response = service.PutOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.EqualTo("ArgumentException"));
            Assert.That(response.ResponseStatus.Message, Is.StringContaining("Name"));
        }

        //TODO remaining validation tests

        #endregion

        #region Post - create a new task

        [Test]
        public void PostOneTimeTask_Standard_ReturnIdAsDefinedByMock(
            [ValueSource("GetDifferentServices")] ISyncReplyClient service)
        {
            var id = 10;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.CreateTask(It.IsAny<OneTimeTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.InitOneTimeTaskRequest(new PostOneTimeTask());

            var response = service.PostOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.Id, Is.EqualTo(id));
        }

        //TODO remaining tests

        #endregion

        #region Put - update an existing one

        [Test]
        public void PutOneTimeTask_Standard_ReturnIdAsDefinedByMock(
            [ValueSource("GetDifferentServices")] ISyncReplyClient service)
        {
            var id = 10;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.UpdateTask(It.IsAny<OneTimeTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.InitOneTimeTaskRequest(new PutOneTimeTask());
            request.Id = id;

            var response = service.PutOneTimeTask(request);

            Assert.That(response.ResponseStatus.ErrorCode, Is.Null);
            Assert.That(response.ResponseStatus.Message, Is.Null);
            Assert.That(response.Id, Is.EqualTo(id));
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

        public IEnumerable<ISyncReplyClient> GetDifferentServices()
        {
            return new ISyncReplyClient[]{
                new ch.tutteli.taskscheduler.test.Soap11.SyncReplyClient("BasicHttpBinding_ISyncReply", BaseUrl+"/soap11"),
                new ch.tutteli.taskscheduler.test.Soap12.SyncReplyClient("WSHttpBinding_ISyncReply", BaseUrl + "/soap12")
            };
        }
    }
}

namespace ch.tutteli.taskscheduler.test.Soap11
{
    public partial class SyncReplyClient : ISyncReplyClient
    {
    }
}

namespace ch.tutteli.taskscheduler.test.Soap12
{
    public partial class SyncReplyClient : ISyncReplyClient
    {
    }
}

