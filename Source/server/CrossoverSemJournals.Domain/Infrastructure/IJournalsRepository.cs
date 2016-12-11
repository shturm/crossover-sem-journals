using System;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Domain.Infrastructure
{
	public interface IJournalsRepository : IRepository<Journal>
	{
		IEnumerable<JournalCatalogEntry> GetCatalog ();
		JournalCatalogEntry GetCatalogEntry (int id);
		void Delete (int journalId);
}
}

