using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.ServiceInterfaces;
using NHibernate.Linq;
using NUnit.Framework;

namespace Tests
{
	public class JournalServiceTests : BaseTestFixture
	{
		[Test]
		[Category ("Integration")]
		public void Delete_GivenJournalId_WhenNoPapersAndPages_DeletesJournal ()
		{
			var journal = new Journal () { Name = "delete me by id" };
			using (var tx = Session.BeginTransaction ()) {
				Session.Save (journal);
				tx.Commit ();
			}

			var sut = this.Scope.Resolve<IJournalService> ();
			sut.Delete (journal.Id);

			var actual = Session.Query<Journal> ().FirstOrDefault ();
			Assert.IsNull (actual, "Journal is not deleted");
		}

		[Test]
		[Category ("Integration")]
		public void Delete_GivenJournalId_WhenHasPapersAndPages_DeletesAllObjects ()
		{
			var sut = this.Scope.Resolve<IJournalService> ();
			var journal = new Journal () {
				Name = "delete me by id",
				Papers = new List<Paper> {
						new Paper() {
							Name="delete me by cascading 1",
							Pages= new List<PaperPage>() {
								new PaperPage () {Image=new byte[] {1}}, new PaperPage () {Image= new byte[] {2,3}}
							}
						},
						new Paper() {
							Name="delete me by cascading 2",
							Pages= new List<PaperPage>() {
								new PaperPage () {Image=new byte[] {4}}, new PaperPage () {Image= new byte[] {5,6}}
							}
						}
					}
			};
			using (var tx = Session.BeginTransaction ()) {
				Session.Save (journal);
				tx.Commit ();
			}

			sut.Delete (journal.Id);


			Assert.AreEqual (0, Session.Query<Journal> ().ToList ().Count, "Journal is not deleted");
			Assert.AreEqual (0, Session.Query<Paper> ().ToList ().Count);
			Assert.AreEqual (0, Session.Query<PaperPage> ().ToList ().Count);
		}

		[Test]
		[Category ("Unit")]
		public void Create_GivenNameAndPrice_CreatesJournal ()
		{
			var sut = Scope.Resolve<IJournalService> ();
			var actual = sut.Create ("journal 1", 42);

			Assert.IsNotNull (actual);
			Assert.AreNotEqual (0, actual.Id);
			Assert.AreEqual ("journal 1", actual.Name);
			Assert.AreEqual (42, actual.Price);
		}
	}
}