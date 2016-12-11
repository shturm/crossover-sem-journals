
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentNHibernate.Testing;
using CrossoverSemJournals.Domain.Entities;
using NHibernate;
using NUnit.Framework;

namespace Tests
{
	[TestFixture ()]
	public class FNHibernateMappingTests : BaseTestFixture
	{

		[Test ()]
		[Category("Integration")]
		public void JournalPersistence_FNH ()
		{
			User user1 = new User ();
			User user2 = new User ();
			using(var tx = Session.BeginTransaction ())
			{
				Session.Save (user1);
				Session.Save (user2);
				tx.Commit ();
			}

			IEnumerable<Paper> papers = new List<Paper> {
				new Paper {Name="Paper 1 JournalPersistance_FNH"},
				new Paper {Name="Paper 2 JournalPersistance_FNH"}
			};

			IEnumerable<Subscription> subscriptions = new List<Subscription> {
				new Subscription {User=user1},
				new Subscription {User=user2}
			};

			new PersistenceSpecification<Journal> (Session, new CrossoverComparer ())
				.CheckProperty (x => x.Name, "Robotics")
				.CheckProperty (x => x.Price, 42.43m)
				.CheckList (x => x.Papers, papers)
				.CheckList (x => x.Subscriptions, subscriptions)
				.VerifyTheMappings ();
		}

		[Test]
		[Category ("Integration")]
		public void PaperPersistence_FNH ()
		{
			Journal journal = new Journal() {Name="Paper FNH journal"};
			using(var tx = Session.BeginTransaction ())
			{
				Session.Save (journal);
				tx.Commit ();
			}

			var pages = new List<PaperPage> () {
				new PaperPage () {Image=new byte[] {1}},
				new PaperPage () {Image=new byte[] {1,2}}
			};

			new PersistenceSpecification<Paper> (Session, new CrossoverComparer ())
				.CheckProperty (x => x.Name, "Paper FNH test name")
				.CheckReference (x => x.Journal, journal)
				.CheckList (x=>x.Pages, pages)
				.VerifyTheMappings();
		}

		[Test]
		[Category ("Integration")]
		public void  SubscriptionPersistence_FNH()
		{
			var user = new User ();
			var journal = new Journal () { Name = "Subscruption FNH test name" };
			using(var tx = Session.BeginTransaction ())
			{
				Session.Save (user);
				Session.Save (journal);
				tx.Commit ();
			}

			new PersistenceSpecification<Subscription> (Session, new CrossoverComparer ())
				.CheckReference (x => x.User, user)
				.CheckReference (x => x.Journal, journal)
				.VerifyTheMappings();
		}

	}
}