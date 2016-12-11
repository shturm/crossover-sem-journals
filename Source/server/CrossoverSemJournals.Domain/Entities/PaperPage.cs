using System;
namespace CrossoverSemJournals.Domain.Entities
{
	public class PaperPage : BaseEntity
	{
		public PaperPage ()
		{
			
		}

		public virtual int PageNumber { get; set;}
		public virtual byte[] Image { get; set;}
		public virtual Paper Paper { get; set;}
	}
}

