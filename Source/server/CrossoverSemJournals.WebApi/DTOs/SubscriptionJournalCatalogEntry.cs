using System;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Infrastructure.WebApi.Dtos
{
	public class SubscriptionJournalCatalogEntry
	{
		public int Id { get; set; }
		public decimal Price { get; set; }
		public string Name { get; set; }
		public bool Subscribed { get; set;}

		public static SubscriptionJournalCatalogEntry FromCatalogEntry(JournalCatalogEntry entry, bool subscribed = false)
		{
			return new SubscriptionJournalCatalogEntry {
				Name=entry.Name,
				Price = entry.Price,
				Id = entry.Id,
				Subscribed=subscribed
			};
		}
	}
}