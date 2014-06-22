using System;
using System.Collections.Generic;
using System.Net;
using CH.Tutteli.TaskScheduler;
using CH.Tutteli.TaskScheduler.BL;
using CH.Tutteli.TaskScheduler.DL;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using Funq;
using NUnit.Framework;
using ServiceStack.Common.Utils;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;

/// based on https://github.com/ServiceStack/ServiceStack.Examples
namespace CH.Tutteli.TaskScheduler.Test.Utils
{
	public abstract class AIntegrationTest
	{
		public const string BaseUrl = "http://127.0.0.1:8085/";

        protected AppHostHttpListenerBase appHost;

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			appHost = CreateAppHost();
			appHost.Init();
			appHost.Start(BaseUrl);
		}

        protected abstract AppHostHttpListenerBase CreateAppHost();

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			appHost.Dispose();
		}

        public static TResponse SendRequest<TResponse>(ITaskRequest request, ServiceClientBase client, string httpMethod)
        {
            return SendRequest<TResponse>(request, client, httpMethod, null);
        }

        public static TResponse SendRequest<TResponse>(ITaskRequest request, ServiceClientBase client, string httpMethod, Action<HttpWebResponse> responseFilter)
        {
            using (client)
            {
                client.HttpMethod = httpMethod;
                client.LocalHttpWebResponseFilter = responseFilter;
                return client.Send<TResponse>(request);
            }
        }

        public IEnumerable<ITaskRequest> GetDifferentTaskRequests()
        {
            return new ITaskRequest[]{
                TaskRequestHelper.CreateOneTimeTaskRequest(),
                TaskRequestHelper.CreateDailyTaskRequest(),
                TaskRequestHelper.CreateWeaklyTaskRequest(),
                TaskRequestHelper.CreateMonthlyTaskRequest()
            };
        }

        public IEnumerable<ServiceClientBase> GetDifferentRestClients()
        {
            return new ServiceClientBase[]{
            new XmlServiceClient(BaseUrl),
            new JsonServiceClient(BaseUrl),
            new JsvServiceClient(BaseUrl),
            };
        }

        public IEnumerable<ISyncReplyClient> GetDifferentSaopClients()
        {
            return new ISyncReplyClient[]{
                new CH.Tutteli.TaskScheduler.Test.Soap11.SyncReplyClient("BasicHttpBinding_ISyncReply", BaseUrl+"/soap11"),
                new CH.Tutteli.TaskScheduler.Test.Soap12.SyncReplyClient("WSHttpBinding_ISyncReply", BaseUrl + "/soap12")
            };
        }
	}
}

namespace CH.Tutteli.TaskScheduler.Test.Soap11
{
    public partial class SyncReplyClient : ISyncReplyClient
    {
    }
}

namespace CH.Tutteli.TaskScheduler.Test.Soap12
{
    public partial class SyncReplyClient : ISyncReplyClient
    {
    }
}