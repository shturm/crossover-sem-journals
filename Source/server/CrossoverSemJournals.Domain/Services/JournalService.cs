using System;
using System.Linq;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Domain.ServiceInterfaces;

namespace CrossoverSemJournals.Domain.Services
{
	public class JournalService : IJournalService
	{
		readonly IJournalsRepository _journalRepo;

		public JournalService (IJournalsRepository journalRepo)
		{
			_journalRepo = journalRepo;
		}

		public JournalCatalogEntry Create (string name, decimal price)
		{
			var journal = new Journal () { Name = name, Price = price };
			_journalRepo.Insert (journal);

			return new JournalCatalogEntry { Name = journal.Name, Price = journal.Price, Id = journal.Id };
		}

		public void Delete (int journalId)
		{
			_journalRepo.Delete (journalId);
		}

		public JournalCatalogEntry GetJournalCatalogById (int id)
		{
			return _journalRepo.GetCatalogEntry (id);
		}

		public IEnumerable<JournalCatalogEntry> GetJournalsCatalog ()
		{
			return _journalRepo.GetCatalog ();
		}

		public void Update (Journal journal)
		{
			_journalRepo.Update (journal);
		}
	}
}