using SportsStore.Models;
using SportStoreDomain.Abstract;
using SportStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{

    //This conotroller alows  add/edit/remove products in databes
    public class CartController : Controller
    {
        IOrderProcessor order;
        IProductRepository repo;
        public CartController(IProductRepository repo,IOrderProcessor order)
        {
            this.repo = repo;
            this.order = order;
        }

        private Cart GetCard()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
                
            }
            return cart;

        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repo.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                GetCard().Add(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveLine(int productID,string returnUrl)
        {
            Product pro = repo.Products
                .FirstOrDefault(p => p.ProductID == productID);

            if(pro!=null)
            {
                GetCard().Remove(pro);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        // GET: Cart
        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel() { Cart=GetCard(), ReturnUrl=returnUrl});
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(GetCard());
        }


        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            

            if (ModelState.IsValid)
            {
                order.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}