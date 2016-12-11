using System;
using System.Collections.Generic;
using System.Data;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Domain.ServiceInterfaces;

namespace CrossoverSemJournals.Domain.Services
{
	public class SubscriptionService : ISubscriptionService
	{
		readonly IRepository<Subscription> _subscriptionsRepository;
		readonly IRepository<Journal> _journalsRepository;
		readonly IRepository<User> _usersRepository;
		readonly IRepository<Paper> _paperRepository;



		public SubscriptionService (IRepository<Subscription> subsRepo,
		                            IRepository<User> usersRepo, 
		                            IRepository<Journal> journalsRepo,
		                            IRepository<Paper> paperRepository)
		{
			_subscriptionsRepository = subsRepo;
			_usersRepository = usersRepo;
			_journalsRepository = journalsRepo;
			_paperRepository = paperRepository;
		}

		public void Subscribe (Guid userId, int journalId)
		{
			var user = _usersRepository.Get (u=>u.Id == userId);

			// TODO: resolve ObjectNotFoundException not being available in this scope
			if (user == null) throw new ArgumentException ($"No user with {userId}");

			var journal = _journalsRepository.Get (j => j.Id == journalId);
			if (journal == null) throw new ArgumentException ($"No journal with {journalId}");

			var subscription = user.Subscribe (journal);

			_subscriptionsRepository.Insert (subscription);
			_usersRepository.Update (user);
			_journalsRepository.Update (journal);
		}

		public void Unsubscribe (Guid userId, int journalId)
		{
			var subscription = _subscriptionsRepository.Get (s => s.User.Id == userId && s.Journal.Id == journalId);
			_subscriptionsRepository.Delete (subscription);
		}

		public IEnumerable<Subscription> GetSubscriptions (Guid userId)
		{
			return _subscriptionsRepository.GetAll (s => s.User.Id == userId);
		}

		public bool CheckSubscription (int journalId, Guid userId)
		{
			var subscription = _subscriptionsRepository.Get (s=>s.Journal.Id==journalId && s.User.Id == userId);
			return subscription == null ? false : true;
		}

		public bool CheckSubscriptionForPaper (int paperId, Guid userId)
		{
			var paper = _paperRepository.Get (p => p.Id == paperId);
			var subscription = _subscriptionsRepository.Get (s => s.Journal.Id == paper.Journal.Id && s.User.Id == userId);

			return subscription == null ? false : true;
		}
	}
}