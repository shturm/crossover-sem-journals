using System;
using System.ComponentModel.DataAnnotations;

namespace CrossoverSemJournals.Infrastructure.WebApi
{
	public class PromoteDemoteAdminUserCommand
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public bool Flag { get; set; }
	}
}

