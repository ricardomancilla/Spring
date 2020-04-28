using API.IoC;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using System.Web.Http;

namespace API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;
            IContainer container = IoC_Factory.GetContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}