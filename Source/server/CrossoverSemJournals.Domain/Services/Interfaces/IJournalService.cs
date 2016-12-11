using System;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;

namespace CrossoverSemJournals.Domain.ServiceInterfaces
{
	public interface IJournalService
	{
		IEnumerable<JournalCatalogEntry> GetJournalsCatalog ();
		JournalCatalogEntry GetJournalCatalogById (int id);
		void Update (Journal journal);
		void Delete (int journalId);
		JournalCatalogEntry Create (string name, decimal price);
	}
}

