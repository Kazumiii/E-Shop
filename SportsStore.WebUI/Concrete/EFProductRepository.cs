using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Concrete
{
    public class EFProductRepository : IProduktRepository
    {
        EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get
            {
               return context.Produts;
            }
 
        }
    }
}