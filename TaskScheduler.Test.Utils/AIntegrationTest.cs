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
	}
}