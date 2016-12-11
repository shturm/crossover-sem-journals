using System;
using CrossoverSemJournals.Domain.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class JournalMapping : SubclassMap<Journal>
	{
		public JournalMapping ()
		{
			Map (x => x.Name);
			Map (x => x.Price);
			HasMany (x=>x.Papers).Cascade.All ().LazyLoad ();
			HasMany (x=>x.Subscriptions).Cascade.All ().LazyLoad ();
		}

	}
}