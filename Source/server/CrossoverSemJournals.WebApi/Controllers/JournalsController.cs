using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using CrossoverSemJournals.Domain;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Domain.ServiceInterfaces;
using Microsoft.AspNet.Identity;
using CrossoverSemJournals.Infrastructure.WebApi.Dtos;

namespace CrossoverSemJournals.Infrastructure.WebApi.Controllers
{
	[Authorize]
	public class JournalsController : ApiController
	{
		readonly IJournalService _journalService;

		readonly ISubscriptionService _subscriptionService;


		public JournalsController (IJournalService journalService, ISubscriptionService subscriptionService)
		{
			_journalService = journalService;
			_subscriptionService = subscriptionService;
		}

		[HttpGet]
		[Route("api/journals")]
		public IEnumerable<JournalCatalogEntry> GetJournals(string term = null)
		{
			return _journalService.GetJournalsCatalog ();
		}

		[HttpGet]
		[Route ("api/journals/subscriptions")]
		public IEnumerable<SubscriptionJournalCatalogEntry> GetJournalsWithSubscriptions (string term = null)
		{
			var userId = Guid.Parse (User.Identity.GetUserId ());

			var allJournals = _journalService.GetJournalsCatalog ();
			var subscriptions = _subscriptionService.GetSubscriptions (userId);

			var query = from journal in allJournals
						join sub in subscriptions
						on journal.Id equals sub.Journal.Id
						into grouping from sub in grouping.DefaultIfEmpty ()
						select new SubscriptionJournalCatalogEntry {
							Id = journal.Id,
							Name = journal.Name,
							Price = journal.Price,
							Subscribed = sub == null ? false : true
						};

			var result = query.ToList ();
			return result;
		}

		[HttpGet]
		[Route ("api/journals/subscription")]
		public SubscriptionJournalCatalogEntry GetJournalWithSubscription ([FromUri]int journalId)
		{
			Guid userId = Guid.Parse(User.Identity.GetUserId ());
			var journal = _journalService.GetJournalCatalogById (journalId);
			bool subscribed = _subscriptionService.CheckSubscription (journalId, userId);

			var result = SubscriptionJournalCatalogEntry.FromCatalogEntry (journal, subscribed);
			return result;
		}

		[HttpGet]
		[Route ("api/journals")]
		public JournalCatalogEntry GetJournal ([FromUri]int id)
		{
			return _journalService.GetJournalCatalogById (id);
		}

		[HttpPut]
		[Route ("api/journals")]
		[Authorize(Roles = "Admin")]
		public IHttpActionResult UpdateJournal (Journal journal)
		{
			try {
				_journalService.Update (journal);
				return Ok ();
			} catch (Exception ex) {
				return InternalServerError (ex);
			}
		}

		[HttpDelete]
		[Route("api/journals")]
		[Authorize (Roles = "Admin")]
		public void DeleteJournalById ([FromUri]int journalId)
		{
			_journalService.Delete (journalId);
		}

		[HttpPost]
		[Route("api/journals")]
		[Authorize (Roles = "Admin")]
		public JournalCatalogEntry CreateJournal (JournalCatalogEntry requestJournal)
		{
			JournalCatalogEntry journalDto = _journalService.Create (requestJournal.Name, requestJournal.Price);
			return journalDto;
		}

		[HttpPost]
		[Route ("api/journals/subscribe")]
		public IHttpActionResult SubscribeToJournal (int journalId)
		{
			Guid userId = Guid.Parse (User.Identity.GetUserId ());

			try {
				_subscriptionService.Subscribe (userId, journalId);
				return Ok ();
			} catch (Exception ex) {
				return InternalServerError (ex);
			}
		}

		[HttpPost]
		[Route ("api/journals/unsubscribe")]
		public IHttpActionResult UnsubscribeFromJournal (int journalId)
		{
			Guid userId = Guid.Parse (User.Identity.GetUserId ());

			try {
				_subscriptionService.Unsubscribe (userId, journalId);
				return Ok ();
			} catch (Exception ex) {
				return InternalServerError (ex);
			}
		}
	}
}