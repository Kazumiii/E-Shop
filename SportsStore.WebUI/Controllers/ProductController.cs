using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {


        IProduktRepository repo;

        public ProductController(IProduktRepository repozytorium)
        {
            repo = repozytorium;
        }

        public int PageSize = 4;
        // GET: Product
        public ViewResult List(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = repo.Products
                   .OrderBy(p => p.ProductID)
                   .Skip((page - 1) * PageSize)
                   .Take(PageSize),
                PagingINfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repo.Products.Count(),
                }
            };
            return View(model);


        }

    }
}