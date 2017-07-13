using SportStoreDomain.Abstract;
using SportStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{
    
    public class AdminController : Controller
    {
//Here are CRUD Operations
        IProductRepository repo;
        // GET: Admin

            public AdminController(IProductRepository repo)
        {
            this.repo = repo;
        }
        public ActionResult Index()
        {
            return View(repo.Products);
        }

        [HttpPost]
        public ActionResult Edit(Product pro,HttpPostedFileBase image)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    pro.ImageMimeType = image.ContentType;
                    pro.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(pro.ImageData, 0, image.ContentLength);
                }

                repo.SaveProduct(pro);
                TempData["message"] = string.Format("Saved" + pro.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(pro);
            }
        }

        public ViewResult Edit(int productID)
        {
            Product pro = repo.Products
                .FirstOrDefault(x => x.ProductID == productID);
            return View(pro);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product pro = repo.DeleteProduct(id);
            TempData["message"] = string.Format("Removed" + pro.Name);
            return RedirectToAction("Index");
        }
    }
}
