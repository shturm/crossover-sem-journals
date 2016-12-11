using System;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Domain.Infrastructure
{
	public interface IPapersRepository : IRepository<Paper>
	{
		IEnumerable<PaperCatalogEntry> GetPaperCatalogEntriesByJournalId (int id);
		void Delete (int paperId);
		PaperCatalogEntry GetPaperCatalogEntry (int paperId);
	}
}