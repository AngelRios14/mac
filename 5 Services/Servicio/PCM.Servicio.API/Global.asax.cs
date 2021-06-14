using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.AspNet.SignalR.Hubs;
using PCM.Servicio.API.App_Start;
using PCM.Servicio.Domain.Implementation;
using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Infraestructure.Implementation;
using PCM.Servicio.Infraestructure.Interface;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using static PCM.Servicio.API.WindsorControllerFactory;

namespace PCM.Servicio.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer __Container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // BootstrapContainer();

            var container = new Container();
            container.Register<IBLServicio, BLServicio>();
            container.Register<IDAServicio, DAServicio>();
            container.Register<IBLEntidad, BLEntidad>();
            container.Register<IDAEntidad, DAEntidad>();
            container.Register<IBLSede, BLSede>();
            container.Register<IDASede, DASede>();
            container.Register<IBLUsuario, BLUsuario>();
            container.Register<IDAUsuario, DAUsuario>();
            container.Register<IBLOperacion, BLOperacion>();
            container.Register<IDAOperacion, DAOperacion>();
            container.Register<IBLParametro, BLParametro>();
            container.Register<IDAParametro, DAParametro>();
            container.Register<IBLAtencion, BLAtencion>();
            container.Register<IDAAtencion, DAAtencion>();
            container.Verify();


            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
        protected void Application_End()
        {
            __Container.Dispose();
        }
        private static void BootstrapContainer()
        {
            var iwisarrInstaller = FromAssembly.Named(ConfigurationManager.AppSettings["AssemblyAplicacionApi"]);
            __Container = new WindsorContainer().Install(iwisarrInstaller);
            var controllerFactory = new WindsorControllerFactory(__Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            __Container.Register(Classes.FromThisAssembly().BasedOn(typeof(IHub)).LifestyleTransient());
            SignalRDependencyResolver signalRDependencyResolver = new SignalRDependencyResolver(__Container);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = signalRDependencyResolver;
        }
    }
   public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)kernel.Resolve(controllerType);
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
