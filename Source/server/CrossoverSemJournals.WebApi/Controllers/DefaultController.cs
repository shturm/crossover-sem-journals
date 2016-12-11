using System;
using System.Web.Http;
using CrossoverSemJournals.Domain;

namespace CrossoverSemJournals.Infrastructure.WebApi.Controllers
{
	public class DefaultController : ApiController
	{

		public DefaultController ()
		{
		}
		public IHttpActionResult Get()
		{
			return Ok (new { message = "Hello WebApi"});
		}
	}
}