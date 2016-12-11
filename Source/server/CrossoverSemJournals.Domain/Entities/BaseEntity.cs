using System;
namespace CrossoverSemJournals.Domain.Entities
{
	public class BaseEntity
	{
		public BaseEntity ()
		{
			Created = DateTime.Now;
		}

		public virtual int Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime Updated { get; set; }
	}
}