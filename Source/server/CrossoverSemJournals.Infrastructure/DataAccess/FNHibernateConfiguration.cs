using System;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Reflection;
using System.Configuration;
using NHibernate.Cfg;
using FluentNHibernate.Automapping;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class FNHibernateConfiguration
	{
		static ISessionFactory _factory;
		public static readonly object lockObject = new object ();
		public static FluentConfiguration FNHConfiguration { get; private set; }

		public static NHibernate.Cfg.Configuration NHConfiguration { get; private set; }


		public static ISessionFactory GetSessionFactory ()
		{
			if (_factory != null) {
				return _factory;
			}

			lock (lockObject) {
				var connectionString = ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString;
				FNHConfiguration = Fluently.Configure ()
                    .Database (MySQLConfiguration.Standard.ConnectionString (connectionString).ShowSql ())
					.Mappings (x => x.FluentMappings.AddFromAssembly (Assembly.GetExecutingAssembly ()))
					.ExposeConfiguration (SetNHConfiguration)
                    .ExposeConfiguration (conf => {});
				try {
					_factory = FNHConfiguration.BuildSessionFactory ();
				} catch (Exception ex) {
					Exception e = ex.InnerException;
					while (e.InnerException != null) e = e.InnerException;
					Console.WriteLine (ex.Message);
					throw e;
				}

			}

			return _factory;
		}

		public static ISession OpenSession ()
		{
			var factory = GetSessionFactory ();
			var session = factory.OpenSession ();

			return session;
		}

		static void SetNHConfiguration (NHibernate.Cfg.Configuration config)
		{
			NHConfiguration = config;
		}
	}
}