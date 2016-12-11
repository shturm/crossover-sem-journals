using System;
using System.Linq;
using NHibernate.Linq;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Infrastructure.WebApi.Controllers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Principal;
using Moq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace Tests
{
	public class JournalsControllerTests : BaseControllerTests<JournalsController>
	{
		[Test]
		[Category ("Integration")]
		public void UpdateJournal ()
		{
			var journal = new Journal { 
				Name = "new journal",
				Papers = new List<Paper> {
					new Paper{Name="Paper 1.1"},
					new Paper{Name="Paper 1.2"},
				}
			};
			using (var tx = Session.BeginTransaction ())
			{
				Session.Save (journal);
				tx.Commit ();
			}

			var updatedJournal = new Journal { Name = "updated journal", Id = journal.Id};
			Controller.UpdateJournal (updatedJournal);

			using(var tx = Session.BeginTransaction ())
			{
				var actualJournal = Session.Query<Journal> ().FirstOrDefault ();
				Assert.AreEqual (journal.Name, actualJournal.Name);
				Assert.AreEqual (2, actualJournal.Papers.Count ());
			}
		}

	}
}