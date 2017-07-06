using SportStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace SportsStore.Binders
{
    public class CartModelBinder 
    {
        const string sessionKey = "Cart";

        public  object BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            Cart cart = (Cart)modelBindingExecutionContext.HttpContext.Session["sessionKey"];
          if(cart==null)
            {
                cart = new Cart();
                modelBindingExecutionContext.HttpContext.Session["sessionKey"] = cart;
            }
            return cart;
        }
 
    }
}