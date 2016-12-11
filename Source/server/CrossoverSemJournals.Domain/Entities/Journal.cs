using System;
using System.Collections.Generic;

namespace CrossoverSemJournals.Domain.Entities
{
	public class Journal : BaseEntity
	{
		public Journal ()
		{
			Papers = new List<Paper> ();
			Subscriptions = new List<Subscription> ();
		}

		public virtual string Name { get; set; }
		public virtual decimal Price { get; set; }
		public virtual IList<Paper> Papers { get; set; }
		public virtual IList<Subscription> Subscriptions {get;set;}

		public virtual void AddPaper (Paper paper)
		{
			this.Papers.Add (paper);
			paper.Journal = this;
		}
	}
}