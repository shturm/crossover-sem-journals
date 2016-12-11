using System;
using CrossoverSemJournals.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class PaperPageMapping : SubclassMap<PaperPage>
	{
		public PaperPageMapping ()
		{
			Map (x => x.Image);
			Map (x => x.PageNumber);
			References (x=>x.Paper, "PaperId");

		}
	}
}

