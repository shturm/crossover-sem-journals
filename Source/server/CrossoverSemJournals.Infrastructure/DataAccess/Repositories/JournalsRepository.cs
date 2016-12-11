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
	public class JournalsRepository : Repository<Journal>, IJournalsRepository
	{
		public JournalsRepository (ISession session) : base (session)
		{

		}

		public void Delete (int journalId)
		{
			using (var tx = _session.BeginTransaction ()) {
				var journal = _session.Query<Journal> ()
				                      .Fetch (x=>x.Papers)
				                      .Where (p => p.Id == journalId).ToList ().FirstOrDefault ();
				_session.Delete (journal);
				tx.Commit ();
			}
		}

		/// <summary>
		/// Gets all journals without navigational properties
		/// </summary>
		/// <returns>The catalog.</returns>
		public IEnumerable<JournalCatalogEntry> GetCatalog ()
		{
			IEnumerable<JournalCatalogEntry> result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.CreateQuery ("select Id as Id, Name as Name, Price as Price from Journal")
						.SetResultTransformer (Transformers.AliasToBean<JournalCatalogEntry> ()).List<JournalCatalogEntry> ();
				tx.Commit ();
			}
			return result;
		}

		public JournalCatalogEntry GetCatalogEntry (int id)
		{
			JournalCatalogEntry result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.CreateQuery ("select Id as Id, Name as Name, Price as Price from Journal where Id = ?").SetParameter (0, id)
						.SetResultTransformer (Transformers.AliasToBean<JournalCatalogEntry> ()).List<JournalCatalogEntry> ()
						.FirstOrDefault ();
				tx.Commit ();
			}
			return result;
		}

		public override void Update (Journal entity)
		{
			using (var tx = _session.BeginTransaction ()) {
				var journal = _session.Query<Journal> ().Where (j => j.Id == entity.Id).FirstOrDefault ();
				journal.Name = entity.Name;
				journal.Price = entity.Price;
				_session.Update (journal);
				tx.Commit ();
			}
		}

	}
}