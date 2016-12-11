using System;
using System.Linq;
using NHibernate.Linq;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;
using CrossoverSemJournals.Domain.Infrastructure;
using NHibernate;
using NHibernate.Transform;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class PapersRepository : Repository<Paper>, IPapersRepository
	{
		public PapersRepository (ISession session) : base (session)
		{

		}

		public IEnumerable<PaperCatalogEntry> GetPaperCatalogEntriesByJournalId (int journalId)
		{
			IEnumerable<PaperCatalogEntry> result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.CreateQuery ("select Id as Id, Name as Name from Paper where JournalId = ?").SetParameter (0, journalId)
						.SetResultTransformer (Transformers.AliasToBean<PaperCatalogEntry> ()).List<PaperCatalogEntry> ();
				tx.Commit ();
			}
			return result;
		}

		public void Delete(int paperId)
		{
			using (var tx = _session.BeginTransaction ()) {
				var paper = _session.Query<Paper> ().Where (p => p.Id == paperId).ToList ().FirstOrDefault ();
				_session.Delete (paper);
				tx.Commit ();
			}
		}

		public PaperCatalogEntry GetPaperCatalogEntry (int paperId)
		{
			PaperCatalogEntry result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.CreateQuery ("select Id as Id, Name as Name from Paper where Id = ?").SetParameter (0, paperId)
				                 .SetResultTransformer (Transformers.AliasToBean<PaperCatalogEntry> ())
				                 .List<PaperCatalogEntry> ().FirstOrDefault ();
				tx.Commit ();
			}
			return result;
		}
	}
}