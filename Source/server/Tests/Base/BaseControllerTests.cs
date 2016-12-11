using System;
using NUnit.Framework;

using Tests;
using System.Web.Http.Hosting;
using System.Web.Http;
using Autofac;

namespace Tests
{
	public abstract class BaseControllerTests<TController> : BaseTestFixture where TController : ApiController
	{
		public TController Controller { get; private set; }

		[SetUp]
		public override void SetUp()
		{
			base.SetUp ();
			Controller = Scope.Resolve<TController> ();
			Controller.Request = new System.Net.Http.HttpRequestMessage ();
			Controller.Request.Properties.Add (HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration ());
		}
	}
}