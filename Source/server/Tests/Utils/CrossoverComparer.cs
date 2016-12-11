using System;
using System.Collections;
using CrossoverSemJournals.Domain.Entities;

namespace Tests
{
	class CrossoverComparer : IEqualityComparer
	{
		public new bool Equals (object x, object y)
		{
			if (x == null || y == null) return false;

			if (x is DateTime && y is DateTime) {
				return CompareAsDates (x, y);
			}

			if (x is BaseEntity && y is BaseEntity)
			{
				return ((BaseEntity)x).Id == ((BaseEntity)y).Id;
			}

			// User maps IdentityUser which does not inherit BaseEntity
			if (x is User && y is User) {
				return ((User)x).Id == ((User)y).Id;
			}

			return x.Equals (y);
		}

		bool CompareAsDates (object x, object y)
		{
			DateTime xDate = (DateTime)x;
			DateTime yDate = (DateTime)y;
			return xDate.ToString () == yDate.ToString ();
		}

		public int GetHashCode (object obj)
		{
			throw new NotImplementedException ();
		}
	}
}