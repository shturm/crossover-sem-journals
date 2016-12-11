using System;
using System.Linq;
using CrossoverSemJournals.Domain.Entities;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class EntitiesUnitTests
	{
		[Test]
		[Category ("Unit")]
		public void UserSubscribesToJournal ()
		{
			var user = new User ();
			var journal = new Journal ();

			var subscription = user.Subscribe (journal);

			Assert.AreEqual (user, subscription.User);
			Assert.AreEqual (journal, subscription.Journal);
			Assert.True (journal.Subscriptions.Contains (subscription));
			Assert.True (user.Subscriptions.Contains (subscription));
		}

		[Test]
		[Category ("Unit")]
		public void UserUnsubscribesFromJournal ()
		{
			var user = new User ();
			var journal = new Journal ();

			var subscription = new Subscription {
				User = user,
				Journal = journal
			};
			journal.Subscriptions.Add (subscription);
			user.Subscriptions.Add (subscription);

			user.Unsubscribe (journal);

			Assert.IsFalse (journal.Subscriptions.Contains (subscription));
			Assert.IsFalse (user.Subscriptions.Contains (subscription));
		}
	}
}

