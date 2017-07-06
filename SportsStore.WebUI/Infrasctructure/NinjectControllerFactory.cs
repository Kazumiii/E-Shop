using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI.Infrasctructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {

        IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {

            ninjectKernel.Bind<IProduktRepository>().To<EFProductRepository>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}