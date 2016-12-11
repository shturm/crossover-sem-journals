using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using CrossoverSemJournals.Domain.Infrastructure;
using CrossoverSemJournals.Infrastructure.DataAccess;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		public Repository ()
		{
		}

		protected readonly ISession _session;

		public Repository (ISession session)
		{
			_session = session;
		}

		public virtual TEntity Get (int id)
		{
			TEntity result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.Get<TEntity> (id);
				tx.Commit ();
			}
			return result;
		}

		public virtual TEntity Get (Expression<Func<TEntity, bool>> predicate)
		{
			TEntity result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.Query<TEntity> ().Where (predicate).ToList ().FirstOrDefault ();
				tx.Commit ();
			}
			return result;
		}

		public virtual IEnumerable<TEntity> GetAll (Expression<Func<TEntity, bool>> expression)
		{
			return _session.Query<TEntity> ().Where (expression).ToList ();
		}

		public virtual IEnumerable<TEntity> GetAll ()
		{
			IList<TEntity> result;
			using (var tx = _session.BeginTransaction ()) {
				result = _session.CreateCriteria<TEntity> ().List<TEntity> ();
				tx.Commit ();
			}
			return result;
		}

		public virtual void Delete (TEntity entity)
		{
			if (entity == null) {
				return;
			}

			using (var tx = _session.BeginTransaction ()) {
				_session.Delete (entity);
				tx.Commit ();
			}
		}

		public virtual void Delete (IList<TEntity> entity)
		{
			throw new NotImplementedException ();
		}

		public virtual void Insert (TEntity entity)
		{
			using (var tx = _session.BeginTransaction ()) {
				_session.Save (entity);
				tx.Commit ();
			}
		}

		public virtual void Insert (IList<TEntity> entity)
		{
			throw new NotImplementedException ();
		}

		public virtual void Update (TEntity entity)
		{
			using (var tx = _session.BeginTransaction ()) {
				_session.SaveOrUpdate (entity);
				tx.Commit ();
			}
		}

		public virtual void Update (IList<TEntity> entity)
		{
			throw new NotImplementedException ();
		}

		public virtual void Dispose ()
		{
			_session.Dispose ();
		}

	}
}