using System.Configuration;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using CrossoverSemJournals.Infrastructure.WebApi;
using CrossoverSemJournals.Infrastructure;
using CrossoverSemJournals.Domain;
using System;
using System.IO;

public class TestUtils
{
	private static IContainer Container;

	public static ILifetimeScope GetAutofacScope ()
	{
		if (TestUtils.Container == null) {
			InitializeAutofacContainer ();
		}

		return Container.BeginLifetimeScope ();
	}

	public static void InitializeAutofacContainer ()
	{
		ConfigurationManager.ConnectionStrings.Add (
			new ConnectionStringSettings (
				"DefaultConnection",
				"Server=localhost;Database=crossover_sem_journals_test;Uid=uniuser;Pwd=unipass;")
		);

		var builder = new ContainerBuilder ();
		builder.RegisterApiControllers (Assembly.GetAssembly (typeof (Startup)));
		builder.RegisterModule (new AutofacDomainModule ());
		builder.RegisterModule (new AutofacInfrastructureModule(web: false));
		Container = builder.Build ();
	}

	public static byte [] ExtractResource (String filename)
	{
		System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly ();
		using (Stream resFilestream = a.GetManifestResourceStream (filename)) {
			if (resFilestream == null) return null;
			byte [] ba = new byte [resFilestream.Length];
			resFilestream.Read (ba, 0, ba.Length);
			return ba;
		}
	}
}

public static class ExceptionExtensions
{
	public static Exception InnerMostException(this Exception value)
	{
		if (value.InnerException != null)
			return ExceptionExtensions.InnerMostException (value.InnerException);

		return value;
	}
}