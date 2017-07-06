using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SportsStore.WebUI.Models
{
    public class ProductListViewModel:IEnumerable<Product>
    {
        public PagingInfo PagingINfo { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}