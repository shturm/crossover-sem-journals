using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace CrossoverSemJournals.Domain.Infrastructure
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> GetAll (Expression<Func<TEntity, bool>> expression);
		IEnumerable<TEntity> GetAll ();
		TEntity Get (Expression<Func<TEntity, bool>> expression);
		TEntity Get (int id);
		void Insert (TEntity entity);
		void Insert (IList<TEntity> entity);
		void Delete (TEntity entity);
		void Delete (IList<TEntity> entity);
		void Update (TEntity entity);
		void Update (IList<TEntity> entity);
	}
}