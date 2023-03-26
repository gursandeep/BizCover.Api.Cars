using BizCover.Api.Cars.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http.Dispatcher;
using BizCover.Api.Cars.Interfaces;
using BizCover.Api.Cars.Implementations;

namespace BizCover.Api.Cars
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfig.Register();

            var services = new ServiceCollection();

            services.AddTransient<Controllers.CarsController>();
            services.AddScoped<ICarService, CarService>();

            var provider = services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });

            GlobalConfiguration.Configuration.Services.Replace(
            typeof(IHttpControllerActivator),
            new MsDiHttpControllerActivator(provider));
        }
    }
}
