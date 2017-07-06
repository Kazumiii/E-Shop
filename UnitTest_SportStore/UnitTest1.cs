using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore2.Domain.Abstract;
using Moq;
using SportsStore2.Domain.Entities;
using System.Linq;
using System;
using SportsStore2.WebUI.HtmlHelpers;
using SportsStore2.WebUI.Controllers;
using System.Collections.Generic;
using SportsStore2.WebUI.Models;
using System;

namespace SportStore.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1" },
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                    new Product {ProductID=4,Name="P4" },
                    new Product {ProductID=5,Name="P5" },
                }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Modeles;
            Product[] prodArray = result.Pro.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }


        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo info = new PagingInfo()
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemPerPage = 10,
            };

            Func<int, string> pageUrlDelegate = i => "Strona" + i;
            MvcHtmlString result = myHelper.PageLinks(info, pageUrlDelegate);

            Assert.AreEqual(result.ToString(), @"<a href=""Strona 1"">1<</a>" + @"<a class=""selected""href=""Strona 2"">2</a>" + @"<a href=""Strona 3"">3</a>");
        }


        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1" },
                    new Product {ProductID=2,Name="P2" },
                    new Product {ProductID=3,Name="P3" },
                    new Product {ProductID=4,Name="P4" },
                    new Product {ProductID=5,Name="P5" },
                }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]

            {
                new Product {ProductID=1,Name="P1" },
                new Product {ProductID=2,Name="P2" },
                new Product {ProductID=3,Name="P3" },
                new Product {ProductID=4,Name="P4" },
                new Product {ProductID=5,Name="P5" },
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            Product[] result = ((ProductListViewModel)controller.List("Cat2", 2).Model).Products.ToArray();
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[2].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");

        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1" },
                new Product {ProductID=2,Name="P2" },
                new Product {ProductID=3,Name="P3" },
                new Product {ProductID=4,Name="P4" },
                new Product {ProductID=5,Name="P5" },

            }.AsQueryable());

            NavController target = new NavController(mock.Object);
            string[] result = ((IEnumerable<string>)target.Menu().Model).ToArray();
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Jabłka");
            Assert.AreEqual(result[1], "Pomarańcze");
            Assert.AreEqual(result[2], "Śliwki");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                    new Product {ProductID=1,Name="P1",Category="Jabłka" },
                    new Product {ProductID=2,Name="P2",Category="Pomarańcze" },
                }.AsQueryable());

            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Jabłka";
            string results = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelect, results);
        }

        [TestMethod]
        public void Generate_Cateory_Specific_Product_Count()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1",Category="Cat1" },
                new Product {ProductID=2,Name="P2",Category="Cat2" },
                new Product {ProductID=3,Name="P3",Category="Cat1" },
                new Product {ProductID=4,Name="P4",Category="Cat2" },
                new Product {ProductID=5,Name="P5",Category="Cat3" },
            }.AsQueryable());


            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            int res1 = ((ProductListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductListViewModel)controller.List(null).Mode).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
