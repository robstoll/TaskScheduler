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

		public void SendToEachEndpoint<TRes>(object request, Action<TRes> validate)
		{
			SendToEachEndpoint(request, null, validate, null);
		}

		/// <summary>
		/// Run the request against each Endpoint
		/// </summary>
		/// <typeparam name="TRes"></typeparam>
		/// <param name="request"></param>
		/// <param name="validate"></param>
		/// <param name="httpMethod"></param>
        public void SendToEachEndpoint<TRes>(object request, string httpMethod, Action<TRes> validate, Action<HttpWebResponse> responseFilter)
		{
			using (var xmlClient = new XmlServiceClient(BaseUrl))
			using (var jsonClient = new JsonServiceClient(BaseUrl))
			using (var jsvClient = new JsvServiceClient(BaseUrl))
			{
				xmlClient.HttpMethod = httpMethod;
                xmlClient.LocalHttpWebResponseFilter = responseFilter;
				jsonClient.HttpMethod = httpMethod;
				jsvClient.HttpMethod = httpMethod;

				var xmlResponse = xmlClient.Send<TRes>(request);
				if (validate != null) validate(xmlResponse);

				var jsonResponse = jsonClient.Send<TRes>(request);
				if (validate != null) validate(jsonResponse);

				var jsvResponse = jsvClient.Send<TRes>(request);
				if (validate != null) validate(jsvResponse);
			}
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