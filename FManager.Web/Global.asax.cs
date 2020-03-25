using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FManager.Web;
using FManager.Web.Domain.Modules;
using FManager.Web.Modules;
using System.Web;
using System.Web.SessionState;

namespace FManager
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private Container container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            MapperModule.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            InitializeContainer();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InitializeContainer()
        {
            container = new Container();

            var mixed = Lifestyle.CreateHybrid(new AsyncScopedLifestyle(), new ThreadScopedLifestyle());
            var style = Lifestyle.CreateHybrid(new WebRequestLifestyle(), mixed);

            container.Options.DefaultScopedLifestyle = style;

            RepositoryModule.Initialize(container);
            ServiceModule.Initialize(container);
            FactoryModule.Initialize(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterInjections();
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath?.Contains(@"~/api") ?? false;
        }
    }
}
