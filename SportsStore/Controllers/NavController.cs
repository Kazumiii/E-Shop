using SportsStore.Models;
using SportStoreDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu(string category)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}