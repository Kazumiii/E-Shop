using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreDomain.Entities
{
   public  class Cart
    {
        List<CartLine> lineCollection = new List<CartLine>();


        public void Add(Product product,int quantities)
        {
            CartLine line=lineCollection
                .Where(x=>x.Product.ProductID==product.ProductID)
                .FirstOrDefault();

            if(line==null)
            {
                lineCollection.Add(new CartLine() { Product = product, Quantities = quantities });
            }
            else
            {
                line.Quantities += quantities;
            }

        }

        public void Remove(Product product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }

        public decimal Price()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantities);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine>Line
        {
            get
            {
                return lineCollection;
            }
        }
    }
}
