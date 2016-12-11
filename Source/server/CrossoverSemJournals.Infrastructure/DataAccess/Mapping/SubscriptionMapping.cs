using System;
using CrossoverSemJournals.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CrossoverSemJournals.Infrastructure
{
	public class SubscriptionMapping : SubclassMap<Subscription>
	{
		public SubscriptionMapping ()
		{
			References (x => x.Journal, "JournalId");
			References (x=>x.User, "UserId");
		}
	}
}

