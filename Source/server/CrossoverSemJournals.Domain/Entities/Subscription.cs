namespace CrossoverSemJournals.Domain.Entities
{
	public class Subscription : BaseEntity
	{
		public virtual User User { get; set;}
		public virtual Journal Journal { get; set;}
	}
}