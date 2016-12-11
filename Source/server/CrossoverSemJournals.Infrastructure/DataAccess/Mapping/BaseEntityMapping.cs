using System;
using CrossoverSemJournals.Domain.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace CrossoverSemJournals.Infrastructure.DataAccess
{
	public class BaseEntityMapping : ClassMap<BaseEntity>
	{
		public BaseEntityMapping ()
		{
			UseUnionSubclassForInheritanceMapping ();
			Id (x => x.Id).GeneratedBy.Increment ();
			Map (x => x.Created).CustomType<UtcDateTimeType> ();
			Map (x => x.Updated).CustomType<UtcDateTimeType> ();
		}

	}
}