﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using Funq;
using Moq;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;

namespace CH.Tutteli.TaskScheduler.Test.rest
{
    [TestFixture]
    public class TaskSchedulerServiceMockedRepositoryTest : AIntegrationTest
    {

        #region validation errors
        [Test]
        public void Post_NameNullOrEmpty_Return400(
            [ValueSource("GetDifferentTaskRequests")] ITaskRequest request,
            [Values(null, "")] string name,
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            request.Name = name;
            SendRequest(request, service, "POST", "Name");
        }

        [Test]
        public void Put_NameNullOrEmpty_Return400(
            [ValueSource("GetDifferentTaskRequests")] ITaskRequest request,
            [Values(null, "")] string name,
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            request.Id = 1;
            request.Name = name;
            SendRequest(request, service, "PUT", "Name");
        }

        [Test]
        public void Post_CallbackUrlMissing_Return400(
            [ValueSource("GetDifferentTaskRequests")] ITaskRequest request,
            [Values(null, "")] string callbackUrl,
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            request.CallbackUrl = callbackUrl;
            SendRequest(request, service, "POST", "CallbackUrl");
        }

        [Test]
        public void Put_CallbackUrlMissing_Return400(
            [ValueSource("GetDifferentTaskRequests")] ITaskRequest request,
            [Values(null, "")] string callbackUrl,
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            request.Id = 1;
            request.CallbackUrl = callbackUrl;
            SendRequest(request, service, "PUT", "CallbackUrl");
        }

        [Test]
        public void Post_IdProvided_Return400(
            [ValueSource("GetDifferentTaskRequests")] ITaskRequest request,
            [Values(null, "")] string callbackUrl,
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            request.Id = 1;
            SendRequest(request, service, "POST", "Id provided");
        }

        [Test]
        public void Put_IdNotProvided_Return400(
            [ValueSource("GetDifferentTaskRequests")] ITaskRequest request,
            [Values(null, "")] string callbackUrl,
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            SendRequest(request, service, "PUT", "Id not provided");
        }

        [Test]
        public void Post_OneTimeTaskTriggerNotSet_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();
            request.Trigger = DateTime.MinValue;
            SendRequest(request, service, "POST", "TriggerDate");
        }

        [Test]
        public void Put_OneTimeTaskTriggerNotSet_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();
            request.Id = 1;
            request.Trigger = DateTime.MinValue;
            SendRequest(request, service, "PUT", "TriggerDate");
        }

        [Test]
        public void Post_DailyTaskRecursEveryXDaysInvalid_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateDailyTaskRequest();
            request.RecursEveryXDays = -1;
            SendRequest(request, service, "POST", "RecursEveryXDays");
        }

        [Test]
        public void Put_DailyTaskRecursEveryXDaysInvalid_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateDailyTaskRequest();
            request.RecursEveryXDays = -1;
            SendRequest(request, service, "PUT", "RecursEveryXDays");
        }

        [Test]
        public void Post_WeeklyTaskRecursEveryXDaysInvalid_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.RecursEveryXWeeks = -1;
            SendRequest(request, service, "POST", "RecursEveryXWeeks");
        }

        [Test]
        public void Put_WeeklyTaskRecursEveryXDaysInvalid_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.RecursEveryXWeeks = -1;
            SendRequest(request, service, "PUT", "RecursEveryXWeeks");
        }

        [Test]
        public void Post_WeeklyTaskTriggerWhenDayOfWeekNull_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.TriggerWhenDayOfWeek = null;
            SendRequest(request, service, "POST", "TriggerWhenDayOfWeek");
        }

        [Test]
        public void Post_WeeklyTaskTriggerWhenDayOfWeekEmpty_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.TriggerWhenDayOfWeek = new HashSet<DayOfWeek>();
            SendRequest(request, service, "POST", "TriggerWhenDayOfWeek");
        }

        [Test]
        public void Put_WeeklyTaskTriggerWhenDayOfWeekNull_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.TriggerWhenDayOfWeek = null;
            SendRequest(request, service, "PUT", "TriggerWhenDayOfWeek");
        }

        [Test]
        public void Put_WeeklyTaskTriggerWhenDayOfWeekEmpty_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.TriggerWhenDayOfWeek = new HashSet<DayOfWeek>();
            SendRequest(request, service, "PUT", "TriggerWhenDayOfWeek");
        }

        [Test]
        public void Post_MonthlyTaskRecursOnMonthNull_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();
            request.RecursOnMonth = null;
            SendRequest(request, service, "POST", "RecursOnMonth");
        }

        [Test]
        public void Post_MonthlyTaskRecursOnMonthEmpty_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();
            request.RecursOnMonth = new HashSet<Triggers.EMonth>();
            SendRequest(request, service, "POST", "RecursOnMonth");
        }

        [Test]
        public void Put_MonthlyTaskRecursOnMonthNull_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();
            request.RecursOnMonth = null;
            SendRequest(request, service, "PUT", "RecursOnMonth");
        }

        [Test]
        public void Put_MonthlyTaskRecursOnMonthEmpty_Return400(
            [ValueSource("GetDifferentRestClients")] ServiceClientBase service)
        {
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();
            request.RecursOnMonth = new HashSet<Triggers.EMonth>();
            SendRequest(request, service, "PUT", "RecursOnMonth");
        }

        #endregion

        #region Post - create a new task
        [Test]
        public void PostOneTimeTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 10;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.CreateTask(It.IsAny<OneTimeTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();

            SendToEachEndpoint<TaskResponse>(request, "POST", response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.CreateTask(It.IsAny<OneTimeTaskRequest>()));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                    Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.ONE_TIME + "/" + id));
                });
        }

        [Test]
        public void PostDailyTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 12;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.CreateTask(It.IsAny<DailyTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateDailyTaskRequest();

            SendToEachEndpoint<TaskResponse>(request, "POST", response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.CreateTask(It.IsAny<DailyTaskRequest>()));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                    Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.DAILY + "/" + id));
                });
        }

        [Test]
        public void PostWeeklyTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 13;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.CreateTask(It.IsAny<WeeklyTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();

            SendToEachEndpoint<TaskResponse>(request, "POST", response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.CreateTask(It.IsAny<WeeklyTaskRequest>()));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                    Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.WEEKLY + "/" + id));
                });
        }

        [Test]
        public void PostMonthlyTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 13;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.CreateTask(It.IsAny<MonthlyTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();

            SendToEachEndpoint<TaskResponse>(request, "POST",
                response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.CreateTask(It.IsAny<MonthlyTaskRequest>()));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                    Assert.That(filter.Headers.Get("Location"), Is.EqualTo(Global.URL_PREFIX + Global.MONTHLY + "/" + id));
                });
        }

        #endregion

        #region Put - update an existing one

        [Test]
        public void PutOneTimeTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 10;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.UpdateTask(It.IsAny<OneTimeTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateOneTimeTaskRequest();
            request.Id = id;

            SendToEachEndpoint<TaskResponse>(request, "PUT", response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.UpdateTask(It.Is<OneTimeTaskRequest>(x => x.Id == id)));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(filter.Headers.AllKeys, Is.Not.Contains("Location"));
                });
        }

        [Test]
        public void PutDailyTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 12;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.UpdateTask(It.IsAny<DailyTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateDailyTaskRequest();
            request.Id = id;

            SendToEachEndpoint<TaskResponse>(request, "PUT", response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.UpdateTask(It.Is<DailyTaskRequest>(x => x.Id == id)));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(filter.Headers.AllKeys, Is.Not.Contains("Location"));
                });
        }

        [Test]
        public void PutWeeklyTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 13;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.UpdateTask(It.IsAny<WeeklyTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateWeaklyTaskRequest();
            request.Id = id;

            SendToEachEndpoint<TaskResponse>(request, "PUT", response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.UpdateTask(It.Is<WeeklyTaskRequest>(x => x.Id == id)));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(filter.Headers.AllKeys, Is.Not.Contains("Location"));
                });
        }

        [Test]
        public void PutMonthlyTask_Standard_ReturnIdAsDefinedByMock()
        {
            var id = 13;
            var repository = GetRepositoryMock();
            repository.Setup(r => r.UpdateTask(It.IsAny<MonthlyTaskRequest>())).Returns(id);
            var request = TaskRequestHelper.CreateMonthlyTaskRequest();
            request.Id = id;

            SendToEachEndpoint<TaskResponse>(request, "PUT",
                response =>
                {
                    Assert.That(response.Id, Is.EqualTo(id));
                    repository.Verify(r => r.UpdateTask(It.Is<MonthlyTaskRequest>(x => x.Id == id)));
                },
                filter =>
                {
                    Assert.That(filter.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(filter.Headers.AllKeys, Is.Not.Contains("Location"));
                });
        }

        #endregion


        private static void SendRequest(ITaskRequest request, ServiceClientBase service, string httpMethod, string wrongArgument)
        {
            try
            {
                using (service)
                {
                    service.HttpMethod = httpMethod;
                    service.Send<TaskResponse>(request);
                }
            }
            catch (WebServiceException ex)
            {
                Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
                Assert.That(ex.ErrorCode, Is.EqualTo("ArgumentException"));
                Assert.That(ex.ErrorMessage, Is.StringContaining(wrongArgument));
            }
        }

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