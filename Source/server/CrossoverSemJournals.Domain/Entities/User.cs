using System;
using System.Linq;
using System.Collections.Generic;

namespace CrossoverSemJournals.Domain.Entities
{
	public class User
	{
		public virtual Guid Id { get; set; }
		public virtual IList<Subscription> Subscriptions { get; set; }

		public User ()
		{
			Subscriptions = new List<Subscription> ();
		}

		public virtual Subscription Subscribe (Journal journal)
		{
			var subscription = new Subscription {
				User = this,
				Journal = journal
			};
			journal.Subscriptions.Add (subscription);
			Subscriptions.Add (subscription);

			return subscription;
		}

		public virtual void Unsubscribe (Journal journal)
		{
			var subscription = Subscriptions
				.Where (s => s.Journal == journal).Single ();

			Subscriptions.Remove (subscription);
			journal.Subscriptions.Remove (subscription);
		}
	}
}