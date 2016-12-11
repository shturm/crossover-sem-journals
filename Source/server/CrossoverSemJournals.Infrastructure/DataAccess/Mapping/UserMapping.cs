using System;
using CrossoverSemJournals.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class UserMapping : ClassMap<User>
	{
		public UserMapping ()
		{
			Table ("Users");
			Id (x => x.Id).GeneratedBy.Guid ();
			HasMany (x=>x.Subscriptions).Cascade.All ();
		}
	}
}

