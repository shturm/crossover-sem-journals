﻿using System;
using System.Web.Http;

namespace CrossoverSemJournals.Infrastructure.WebApi.Controllers
{
	public class SecretController : ApiController
	{
		[Authorize]
		public IHttpActionResult Get ()
		{
			return Ok (new { message = "This is a secret. Use it to test authorization" });
		}
	}
}

