using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
 
using System.Threading.Tasks;

namespace SportStoreDomain.Entities
{
    //This file contains dates my products
    public class Product
    {
        //Each products have attributes, especially  Reguired which force to us fill up place 

        public int ProductID { get; set; }
        [Required(ErrorMessage = "Give product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Give product description")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Give price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Give category")]
        public string Category { get; set; }

        public byte[] ImageData{get;set;}

       
        public string ImageMimeType { get; set; }
    }
}
