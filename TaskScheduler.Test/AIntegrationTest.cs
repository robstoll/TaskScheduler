using System;
using System.Net;
using ch.tutteli.taskscheduler;
using ch.tutteli.taskscheduler.bl;
using ch.tutteli.taskscheduler.dl;
using Funq;
using NUnit.Framework;
using ServiceStack.Common.Utils;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.ServiceClient.Web;
using ServiceStack.WebHost.Endpoints;

/// based on https://github.com/ServiceStack/ServiceStack.Examples
namespace ServiceStack.Examples.Tests.Integration
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
