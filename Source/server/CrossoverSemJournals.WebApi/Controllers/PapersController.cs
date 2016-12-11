using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Threading.Tasks;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.ServiceInterfaces;
using Microsoft.AspNet.Identity;

namespace CrossoverSemJournals.Infrastructure.WebApi.Controllers
{
	[Authorize]
	public class PapersController : ApiController
	{
		readonly IPaperService _paperService;

		readonly ISubscriptionService _subscriptionService;


		public PapersController (IPaperService paperService, ISubscriptionService subscriptionService)
		{
			_paperService = paperService;
			_subscriptionService = subscriptionService;
		}

		[HttpGet]
		[Route ("api/papers")]
		public IEnumerable<PaperCatalogEntry> GetPaperCatalogEntriesByJournalId ([FromUri]int journalId)
		{
			var result = _paperService.GetPaperCatalogEntriesByJournalId (journalId);
			return result;
		}

		[HttpGet]
		[Route ("api/papers")]
		public PaperCatalogEntry GetPaperCatalogEntry([FromUri]int paperId)
		{
			return _paperService.GetPaperCatalogEntry (paperId);
		}

		[HttpPost]
		[Route ("api/papers/page")]
		public string GetPaperPageBase64 (GetPaperPageCommand command)
		{
			Guid userId = Guid.Parse (User.Identity.GetUserId ());
			bool access = _subscriptionService.CheckSubscriptionForPaper (command.PaperId, userId);
			if (User.IsInRole ("Admin"))
			{
				access = true;
			}

			if (!access) throw new UnauthorizedAccessException ("You are not subscribed to this journal");

			byte[] pageBytes = _paperService.GetPage (command.PaperId, command.PageNumber);
			string base64 = Convert.ToBase64String (pageBytes);

			return base64;
		}

		[HttpPost]
		[Route ("api/papers")]
		[Authorize(Roles = "Admin")]
		public async Task<IHttpActionResult> AddPaperToJournal ()
		{
			if (!Request.Content.IsMimeMultipartContent ()) {
				throw new HttpResponseException (HttpStatusCode.UnsupportedMediaType);
			}

			string root = "tmp";
			if (!Directory.Exists (root)) Directory.CreateDirectory (root);

			var streamProvider = new MultipartFormDataStreamProvider (root);
			try {
				byte [] paperFileBytes;
				var provider = await Request.Content.ReadAsMultipartAsync (streamProvider);


				string paperName = provider.FormData ["paperName"];
				int journalId = int.Parse (provider.FormData ["journalId"]);

				foreach (MultipartFileData f in provider.FileData) {
					paperFileBytes = File.ReadAllBytes (f.LocalFileName);
					_paperService.AddPaperToJournal (paperName, paperFileBytes, journalId);
				}
			} catch (Exception ex) {
				while (ex.InnerException != null) ex = ex.InnerException;
				//Request.CreateErrorResponse (HttpStatusCode.InternalServerError, ex);
				return InternalServerError (ex);
			}

			return Ok ();
		}

		[HttpDelete]
		[Route("api/papers")]
		[Authorize (Roles = "Admin")]
		public void DeletePaperById([FromUri]int paperId)
		{
			_paperService.Delete (paperId);
		}
	}
}