using SportsStore.Models;
using SportStoreDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.Controllers
{

    //Conttorller dipslays  admin Panel
    public class AccountController : Controller
    {

        IAuthProvider auto;


        public AccountController(IAuthProvider auto)
        {
            this.auto = auto;
        }


        // GET: Account
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (auto.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub niepoprawne hasło.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}