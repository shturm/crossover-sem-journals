using System;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Domain.ServiceInterfaces
{
	public interface ISubscriptionService
	{
		void Subscribe (Guid userId, int journalId);
		void Unsubscribe (Guid userId, int journalId);
		IEnumerable<Subscription> GetSubscriptions (Guid userId);
		bool CheckSubscription (int journalId, Guid userId);
		bool CheckSubscriptionForPaper (int paperId, Guid userId);
	}
}

