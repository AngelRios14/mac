using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.AspNet.SignalR.Hubs;
using PCM.MacExpress.Presentation.Core.Factory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PCM.MacExpress.Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer __Container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
        }
        protected void Application_End()
        {
            __Container.Dispose();
        }


        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpCookie myCookie = new HttpCookie("CLIENT_SITE_TOKEN");
            myCookie.Value = ConfigurationManager.AppSettings["CLIENT_SITE_TOKEN"];
            myCookie.Expires = DateTime.Now.AddDays(1d);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        private static void BootstrapContainer()
        {
            __Container = new WindsorContainer().Install(FromAssembly.Named(ConfigurationManager.AppSettings["AssemblyAplicacionCore"]));
            var controllerFactory = new WindsorControllerFactory(__Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            __Container.Register(Classes.FromThisAssembly().BasedOn(typeof(IHub)).LifestyleTransient());
            SignalRDependencyResolver signalRDependencyResolver = new SignalRDependencyResolver(__Container);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = signalRDependencyResolver;
        }
    }

    public class SignalRDependencyResolver : Microsoft.AspNet.SignalR.DefaultDependencyResolver
    {
        private readonly IWindsorContainer _container;
        public SignalRDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            return TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            return TryGetAll(serviceType).Concat(base.GetServices(serviceType));
        }

        [System.Diagnostics.DebuggerStepThrough]
        private object TryGet(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private System.Collections.Generic.IEnumerable<object> TryGetAll(Type serviceType)
        {
            try
            {
                var array = _container.ResolveAll(serviceType);
                return array.Cast<object>().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
