using System;
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
	public class IntegrationTestAppHost : AppHostHttpListenerBase
	{

		public IntegrationTestAppHost() : base("Task Scheduler Web Services", typeof(TaskSchedulerService).Assembly) { }

		public override void Configure(Container container)
		{
            container.Register<IDbConnectionFactory>(
                       new OrmLiteConnectionFactory(PathUtils.MapHostAbsolutePath("~/TaskScheduler-test.sqlite"), SqliteDialect.Provider));
            container.Register<IRepository>(c => new SqlLiteRepository(c.Resolve<IDbConnectionFactory>()));

            //container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            //container.Register<IRepository>(c => new RedisRepository(c.Resolve<IRedisClientsManager>()));
            
            container.Register<ITaskHandler>(c => new TaskHandler(c.Resolve<IRepository>()));
		}
	}

	public abstract class AIntegrationTest
	{
		private const string BaseUrl = "http://127.0.0.1:8085/";

		protected IntegrationTestAppHost appHost;

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			appHost = new IntegrationTestAppHost();
			appHost.Init();
			appHost.Start(BaseUrl);
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			appHost.Dispose();
		}

		public void SendToEachEndpoint<TRes>(object request, Action<TRes> validate)
		{
			SendToEachEndpoint(request, null, validate);
		}

		/// <summary>
		/// Run the request against each Endpoint
		/// </summary>
		/// <typeparam name="TRes"></typeparam>
		/// <param name="request"></param>
		/// <param name="validate"></param>
		/// <param name="httpMethod"></param>
		public void SendToEachEndpoint<TRes>(object request, string httpMethod, Action<TRes> validate)
		{
			using (var xmlClient = new XmlServiceClient(BaseUrl))
			using (var jsonClient = new JsonServiceClient(BaseUrl))
			using (var jsvClient = new JsvServiceClient(BaseUrl))
			{
				xmlClient.HttpMethod = httpMethod;
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
