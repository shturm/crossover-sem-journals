using System.Collections.Generic;

namespace CrossoverSemJournals.Domain.Entities
{
	public class Paper : BaseEntity
	{
		public Paper ()
		{
			Pages = new List<PaperPage> ();
		}

		public virtual string Name { get; set; }
		public virtual Journal Journal {get;set;}
		public virtual byte [] OriginalFile { get; set;}
		public virtual IList<PaperPage> Pages { get; set; }

		public virtual void AddPages(IEnumerable<PaperPage> pages)
		{
			foreach (PaperPage page in pages) {
				Pages.Add (page);
				page.Paper = this;
			}
		}
	}
}