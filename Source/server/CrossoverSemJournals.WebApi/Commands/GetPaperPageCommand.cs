using System;
namespace CrossoverSemJournals.Infrastructure.WebApi
{
	public class GetPaperPageCommand
	{
		public int PaperId { get; set; }
		public int PageNumber { get; set; }
	}
}

