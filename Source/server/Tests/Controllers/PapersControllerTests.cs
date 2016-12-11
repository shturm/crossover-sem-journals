using System;
using System.IO;
using System.Linq;
using NHibernate.Linq;
using System.Net.Http;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Infrastructure.WebApi.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
	public class PapersControllerTests : BaseControllerTests<PapersController>
	{
		[SetUp]
		public void Cleanup ()
		{
			base.SetUp ();
			var files = Directory.EnumerateFiles ("tmp");
			foreach (var file in files) {
				File.Delete (file);
			}
		}

		[Test]
		[Category ("Integration")]
		public async void AddPaperToJournal()
		{
			Journal journal = new Journal { Name = "upload paper test journal" };
			using (var tx = Session.BeginTransaction ()) {
				Session.Save (journal);
				tx.Commit ();
			}
			var fileBytes = TestUtils.ExtractResource ("Tests.togaf_9_exam_study_guide.pdf");
			var content = new MultipartFormDataContent ();
			content.Add (new ByteArrayContent (fileBytes), "form-field-name-does-not-matter", "togaf_intro_weisman.pdf");
			content.Add (new StringContent ("paper name 1"), "paperName");
			content.Add (new StringContent(journal.Id.ToString ()), "journalId");
			Controller.Request.Content = content;

			await Controller.AddPaperToJournal ();

			var paper = Session.Query<Paper> ().ToList ().FirstOrDefault ();

			Assert.AreEqual (1, journal.Papers.Count);
			Assert.AreEqual ("paper name 1", journal.Papers[0].Name);
			Assert.AreEqual (fileBytes.Length, paper.OriginalFile.Length);
			Assert.AreEqual (5, paper.Pages.Count (), "PaperPages were not converted");
		}
	}
}