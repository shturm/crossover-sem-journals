using System;
using System.ComponentModel.DataAnnotations;

namespace CrossoverSemJournals.Infrastructure.WebApi
{
	public class RegisterCommand
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}

