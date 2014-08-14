using Ninject;
using Polling.Domain.Abstract;
using Polling.Domain.Concrete;
using Polling.WebUI.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Polling.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Additional bindings here
            ninjectKernel.Bind<IPollRepository>().To<EFPollRepository>();

            ninjectKernel.Bind<IAuthProvider>().To<AuthProvider>();
        }
    }
}