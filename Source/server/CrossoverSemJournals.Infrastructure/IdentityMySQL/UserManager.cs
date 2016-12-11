using System;
using Microsoft.AspNet.Identity;

namespace CrossoverSemJournals.Infrastructure
{
	public class UserManager : UserManager<IdentityUser, string>
	{
		public UserManager (UserStore store)  : base(store)
		{

		}

		public static UserManager Create ()
		{
			return new UserManager (new UserStore ());
		}
	}
}