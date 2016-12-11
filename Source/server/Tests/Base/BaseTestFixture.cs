using Autofac;
using System;
using NUnit.Framework;
using NHibernate;

namespace Tests
{
	[TestFixture]
	public abstract class BaseTestFixture
	{
		protected ISession Session;
		protected ILifetimeScope Scope;

		[TestFixtureSetUp]
		public virtual void Init ()
		{
			try {
				Scope = TestUtils.GetAutofacScope ();
				Session = Scope.Resolve<ISession> ();
			} catch (Exception ex) {
				Exception e = ex.InnerException;
				while (e.InnerException != null) e = e.InnerException;
				Console.WriteLine (ex.Message);
				throw (e);
			}
		}

		[SetUp]
		public virtual void SetUp ()
		{
			using (var tx = Session.BeginTransaction ()) {
				Session.CreateSQLQuery ("truncate Journal").List ();
				Session.CreateSQLQuery ("truncate Paper").List ();
				Session.CreateSQLQuery ("truncate PaperPage").List ();
				Session.CreateSQLQuery ("truncate Subscription").List ();
				Session.CreateSQLQuery ("truncate Users").List ();
				tx.Commit ();
			}

			Session.Clear ();
		}
	}
}