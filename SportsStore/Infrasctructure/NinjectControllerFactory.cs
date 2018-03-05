using Ninject;
using SportStoreDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using SportStoreDomain.Entities;
using SportStoreDomain.Concrete;
using System.Configuration;

namespace SportsStore.Infrasctructure
{

    // I use Ninject to solve dependencies
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel= new StandardKernel();
            AddBindings();
        }

     

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        public void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product() {Name="Piłka nożna",Price=25 },
                new Product() {Name="Deska surfingowa",Price=179 },
                new Product() {Name="Buty do biegania",Price=95 },

            }.AsQueryable());

          

            // I use both  IProductRepositroy and  EFProductRepositort, thanks these I am independent on intreface IProductRepository 
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            //herein is as the same as above
            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            ninjectKernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }


    }
}
