using NHibernate;
using Autofac;
using Autofac.Integration.WebApi;
using CrossoverSemJournals.Infrastructure.DataAccess;
using CrossoverSemJournals.Domain.Infrastructure;
using System.Reflection;
using System;
using Microsoft.AspNet.Identity;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Infrastructure
{

	public class AutofacInfrastructureModule : Autofac.Module
	{
		readonly bool _web;

		public AutofacInfrastructureModule (bool web = false)
		{
			_web = web;
		}

		protected override void Load (ContainerBuilder builder)
		{
			if (_web)
			{
				builder.Register (c => FNHibernateConfiguration.OpenSession ()).As<ISession> ().InstancePerRequest ();
				builder.RegisterType<UserManager> ().AsSelf ().AsImplementedInterfaces().InstancePerRequest ();
				builder.RegisterType<UserStore> ().AsSelf ().As<IUserStore<IdentityUser>>().AsImplementedInterfaces ().InstancePerRequest ();
				builder.RegisterType<MySQLDatabase> ().AsSelf ().InstancePerRequest ();

				builder.RegisterGeneric (typeof (Repository<>)).As (typeof (IRepository<>));

			} else {
				ISession nhSession = null;
				builder.Register (c => {
					if (nhSession == null || !nhSession.IsOpen) {
						nhSession = FNHibernateConfiguration.OpenSession ();
						Console.WriteLine ("Session initiated ");
					}
					return nhSession;
				}).As<ISession> ();

				builder.RegisterType<UserManager> ().AsSelf ().InstancePerLifetimeScope ();
				builder.RegisterType<UserStore> ().AsSelf ()
											      .As<IUserStore<IdentityUser>> ().InstancePerLifetimeScope ();
				builder.RegisterType<MySQLDatabase> ().AsSelf ().InstancePerLifetimeScope ();
			}

			builder.RegisterGeneric (typeof (Repository<>)).As (typeof (IRepository<>));
			builder.RegisterType<JournalsRepository> ().As<IRepository<Journal>> ()
												       .AsSelf ()
												       .As <IJournalsRepository>();
			builder.RegisterType<PapersRepository> ().AsImplementedInterfaces ();
			builder.RegisterType<PaperFileConverter> ().AsImplementedInterfaces ();
		}
	}
}