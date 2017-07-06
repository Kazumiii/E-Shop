using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using SportsStore2.WebUI.Controllers;
using SportsStore2.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportStore.UnitTest
{
    [TestClass]
    class CartTest
    {

        [TestMethod]
        public void Cart_Add_New_Lines()
        {
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            CartLine[] results = target.Lines.ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }


        [TestMethod]
        public void Cart_Add_Quantity_For_Existing_Lines()
        {
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Cart_RemoveLine()
        {
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };
            Product p3 = new Product() { ProductID = 3, Name = "P3" };

            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);

            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }


        [TestMethod]
        public void Calculate_Cart_Total()
        {
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };
            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            decimal result = target.ComputeToValue();

            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Cart_Clear_Contest()
        {

            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();

            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product { ProductID=1,Name="P1",Category="Apples"},
                }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object);

            controller.AddToCart(cart, 1, null);
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);

        }

        [TestMethod]
        public void Adding__Product_To_Cart_Goes_To_Cart_Screen()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1",Category="Apples" },
                }.AsQueryable());

            Cart cart = new Cart();
            CartController target = new CartController(mock.Object);
            RedirectToRouteResult resluts = System.Web.Mvc.target.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(resluts.RouteValues["action"], "Index");
            Assert.AreEqual(resluts.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            Cart cart = new Cart();
            CartController controller = new CartController(null);

            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "myUrl").ViewData.Model;

            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");

        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            ShippingDetails shiiping = new ShippingDetails();

            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, shiiping);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);

            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            ShippingDetails shiping = new ShippingDetails();
            CartController target = new CartController(null, mock.Object);
            target.ModelState.AddModelError("error", "error");
            ViewResult result = target.Checkout(cart, shiping);


            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void AdminTest()
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


    }
}
