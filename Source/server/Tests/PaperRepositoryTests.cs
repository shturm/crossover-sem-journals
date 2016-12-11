using System;
using System.Linq;
using System.Collections.Generic;
using Autofac;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Tests;

namespace Tests
{
	[TestFixture]
	public class PaperRepositoryTests : BaseTestFixture
	{

		[Test]
		[Category ("Integration")]
		public void GetPapersByJournalId ()
		{
			var sut = Scope.Resolve<IPapersRepository> ();
			var journal = new Journal {
				Name = "Pediatrics",
				Papers = new List<Paper> {
						new Paper() {Name="Paper 1"},
						new Paper() {Name="Paper 2"}
					}
			};
			using(var tx = Session.BeginTransaction ())
			{
				Session.Save (journal);
				tx.Commit ();
			}

			var actual = sut.GetPaperCatalogEntriesByJournalId (journal.Id);

			Assert.AreEqual (2, actual.Count ());
		}

	}
}