using SportStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreDomain.Abstract
{

    //This Interface is responsible for story of my objects .Repository allows us keep persistance logic separte form domain model 
  public  interface IProductRepository
        //IQueryable<T> storing objects without saying anything how or where  our dates are story or ow it will be retirved
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product pro);
        Product DeleteProduct(int ProductID);
    }
}
