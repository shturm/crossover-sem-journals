using System;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;

namespace CrossoverSemJournals.Domain.ServiceInterfaces
{
	public interface IPaperService
	{
		IEnumerable<PaperCatalogEntry> GetPaperCatalogEntriesByJournalId (int journalId);
		void AddPaperToJournal (string paperName, byte [] paperFileBytes, int journalId);
		void Delete (int paperId);
		PaperCatalogEntry GetPaperCatalogEntry (int paperId);
		byte [] GetPage (int paperId, int pageNumber);
	}
}