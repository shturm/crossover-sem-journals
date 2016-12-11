// OWIN / Security cofnig
using System;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

// WebApi config
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using CrossoverSemJournals.Domain;


[assembly: OwinStartup (typeof (CrossoverSemJournals.Infrastructure.WebApi.Startup), "Configure")]
namespace CrossoverSemJournals.Infrastructure.WebApi
{
	public class Startup
	{
		public void Configure (IAppBuilder app)
		{
			ConfigureOAuth (app);
			ConfigureWebApi (app); // OWIN
		}

		void ConfigureOAuth(IAppBuilder app)
		{
			var oauthServerOptions = new OAuthAuthorizationServerOptions () {
				TokenEndpointPath = new PathString ("/token"),
				Provider = new ApplicationOAuthProvider ("self"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays (14),
				// In production mode set AllowInsecureHttp = false
				AllowInsecureHttp = true
			};
			app.UseCors (CorsOptions.AllowAll);
			app.UseOAuthBearerTokens (oauthServerOptions);

		}

		void ConfigureWebApi(IAppBuilder app)
		{
			var config = new HttpConfiguration ();
			ConfigureWebApi (config);

			app.CreatePerOwinContext<UserManager> (UserManager.Create);
			      
			//app.UseWebApi (config); // TODO extensions method not available
		}

		public static void ConfigureWebApi(HttpConfiguration config)
		{
			// Web API configuration and services

			//var cors = new EnableCorsAttribute (
			//	origins: "*",
			//	headers: "*",
			//	methods: "*"
			//);
			//config.EnableCors (cors);

			// Web API routes
			config.MapHttpAttributeRoutes ();

			config.Routes.MapHttpRoute (
				name: "Api",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute (
				name: "Default",
				routeTemplate: "",
				defaults: new { controller = "Default", action = "Get" }
			);

			var builder = new ContainerBuilder ();
			builder.RegisterApiControllers (Assembly.GetExecutingAssembly ()); 

			builder.RegisterModule (new AutofacDomainModule ());
			builder.RegisterModule(new AutofacInfrastructureModule (web: true));
			var container = builder.Build ();

			config.DependencyResolver = new AutofacWebApiDependencyResolver (container);

			//config.Formatters.Clear ();
			//config.Formatters.Add (new XmlMediaTypeFormatter ());
			//config.Formatters.Add (new JsonMediaTypeFormatter());
			//config.Formatters.Add (new FormUrlEncodedMediaTypeFormatter ());

			var settings = config.Formatters.JsonFormatter.SerializerSettings;
			settings.Formatting = Formatting.Indented;
			settings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
		}

		static void ConfigureWebApi (ContainerBuilder builder)
		{
			
		}
}
}