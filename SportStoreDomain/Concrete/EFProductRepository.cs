using SportStoreDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStoreDomain.Entities;

namespace SportStoreDomain.Concrete
{
    //This section is responible for creates repository, Implements IProductRepository interface and uses  EFDContext to retrieve dates form database
  public  class EFProductRepository : IProductRepository
    {

        EFDbContext context = new EFDbContext();
        public IQueryable<Product> Products
        {
            get
            {
               return context.Products;
            }
        }

        public Product DeleteProduct(int ProductID)
        {
            Product dbEntry = context.Products.Find(ProductID);
            if(dbEntry!=null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product pro)
        { 
           
                Product dbentry = context.Products.Find(pro.ProductID);
                if(dbentry!=null)
                {
                    dbentry.Name = pro.Name;
                    dbentry.Description = pro.Description;
                    dbentry.Category = pro.Category;
                    dbentry.Price = pro.Price;
                dbentry.ImageData = pro.ImageData;
                dbentry.ImageMimeType = pro.ImageMimeType;
            }


            

            context.SaveChanges();
        }
    }
}
