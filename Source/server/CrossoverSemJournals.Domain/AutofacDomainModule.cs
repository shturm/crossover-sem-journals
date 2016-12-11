using System;
using System.Reflection;
using Autofac;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Domain.Services;

namespace CrossoverSemJournals.Domain
{

	public class AutofacDomainModule : Autofac.Module
	{
		protected override void Load (ContainerBuilder builder)
		{
			builder.RegisterType<SubscriptionService> ().AsImplementedInterfaces ();
			builder.RegisterType<JournalService> ().AsImplementedInterfaces ();
			builder.RegisterType<PaperSerivce> ().AsImplementedInterfaces ();
		}
	}
}