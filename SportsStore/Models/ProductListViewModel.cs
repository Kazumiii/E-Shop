using SportStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{
//I use this class to provide the view with details of the product to display on the page
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}