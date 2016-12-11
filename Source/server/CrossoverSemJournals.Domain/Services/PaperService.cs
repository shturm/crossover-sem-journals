using System;
using System.Linq;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Domain.ServiceInterfaces;

namespace CrossoverSemJournals.Domain.Services
{

	public class PaperSerivce : IPaperService
	{
		readonly IPapersRepository _papersRepository;

		readonly IJournalsRepository _journalsRepository;

		readonly IPaperFileConverter _paperFileConerter;


		public PaperSerivce (IPapersRepository repo, IJournalsRepository journalRepo, IPaperFileConverter paperFileConerter)
		{
			_papersRepository = repo;
			_journalsRepository = journalRepo;
			_paperFileConerter = paperFileConerter;
		}

		public void AddPaperToJournal (string paperName, byte [] paperFileBytes, int journalId)
		{
			var pages = _paperFileConerter.Convert (paperFileBytes)
			                                    .Select ((bytes, index) => new PaperPage {Image = bytes, PageNumber=index});
				
			var paper = new Paper () { OriginalFile = paperFileBytes, Name = paperName};
			var journal = _journalsRepository.Get (journalId);

			paper.AddPages (pages);
			journal.AddPaper (paper);

			_papersRepository.Insert (paper);
			_journalsRepository.Update (journal);
		}

		public IEnumerable<PaperCatalogEntry> GetPaperCatalogEntriesByJournalId (int journalId)
		{
			return _papersRepository.GetPaperCatalogEntriesByJournalId (journalId);
		}

		public void Delete (int paperId)
		{
			_papersRepository.Delete (paperId);
		}

		public PaperCatalogEntry GetPaperCatalogEntry (int paperId)
		{
			return _papersRepository.GetPaperCatalogEntry (paperId);
		}

		public byte [] GetPage (int paperId, int pageNumber)
		{
			var paper = _papersRepository.Get (paperId);
			return paper.Pages [pageNumber].Image;
		}
	}
}