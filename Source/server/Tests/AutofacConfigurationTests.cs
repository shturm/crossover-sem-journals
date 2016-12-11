using System;
using Autofac;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Domain.ServiceInterfaces;
using CrossoverSemJournals.Domain.Services;
using CrossoverSemJournals.Infrastructure;
using CrossoverSemJournals.Infrastructure.DataAccess;
using NUnit.Framework;

namespace Tests
{
	public class AutofacConfigurationTests
	{
		public ILifetimeScope Scope { get; private set; }

		[TestFixtureSetUp]
		public void Init()
		{
			Scope = TestUtils.GetAutofacScope ();
		}

		[Test]
		[Category ("Unit")]
		public void  CanInstantinateAllServices()
		{
			Assert.IsInstanceOf<JournalService> (Scope.Resolve<IJournalService> ());
			Assert.IsInstanceOf<PaperSerivce> (Scope.Resolve<IPaperService> ());
			Assert.IsInstanceOf <PapersRepository>(Scope.Resolve<IPapersRepository> ());
		}
	}
}