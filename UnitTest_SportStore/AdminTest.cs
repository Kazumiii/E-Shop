using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using SportsStore2.WebUI.Controllers;
using SportStoreDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SportStore.UnitTest
{
    [TestClass]
    class AdminTest
    {
        [TestMethod]
        public void AdminTesta()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID=1,Name="P1"},
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);
            Product[] results = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual("P1", results[0].Name);
            Assert.AreEqual("P2", results[1].Name);
            Assert.AreEqual("P3", results[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1" },
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                }.AsQueryable());

            AdminController target = new AdminController(mock.Object);
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;


            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);

        }

        [TestMethod]
        public void Can_Edit_Nonxeistet_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1" },
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            Product result = (Product)controller.Edit(4).ViewData.Model as Product;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Chages()
        {
            Product product = new Product() { Name = "Test" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Verify(m => m.SaveProducts(product));
            AdminController controller = new AdminController(mock.Object);
            ActionResult result = controller.Edit(product);

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Verify(m => m.SaveProducts(It.IsAny<Product>()), Times.Never);
            Product product = new Product() { Name = "Test" };
            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("error", "error");
            ActionResult result = controller.Edit(product);

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        /*    [TestMethod]
            public void Can_Delte_Valid_Products()
            {
                Product pro = new Product() { ProductID = 2, Name = "P2" };
                Mock<IProductRepository> mock = new Mock<IProductRepository>();
                mock.Setup(m => m.Products).Returns(new Product[]
                    {
                        new Product {ProductID=1,Name="P1" },
                        pro,
                        new Product {ProductID=3,Name="P3" },
                    }.AsQueryable());

                AdminController target = new AdminController(mock.Object);
                target.Delte(pro.ProductID);

                mock.Verify(m => m.DeleteProduct(pro));
            }
            */
    }
}
