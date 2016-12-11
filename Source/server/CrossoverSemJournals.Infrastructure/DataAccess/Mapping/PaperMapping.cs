using System;
using CrossoverSemJournals.Domain.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class PaperMapping : SubclassMap<Paper>
	{
		public PaperMapping ()
		{
			Map (x => x.Name);
			Map (x => x.OriginalFile);
			References (x => x.Journal, "JournalId");
			HasMany (x=>x.Pages).Cascade.All ();
		}

	}
}