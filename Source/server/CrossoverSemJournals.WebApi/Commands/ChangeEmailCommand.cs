using System;
using System.ComponentModel.DataAnnotations;

namespace CrossoverSemJournals.Infrastructure.WebApi
{
	public class ChangeEmailCommand
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}

