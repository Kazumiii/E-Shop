using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreDomain.Entities
{
public    class ShippingDetails
    {

        [Required(ErrorMessage = "Give your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Give your adress")]
        public string Line1 { get; set; }

        [Required(ErrorMessage = "Give your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Give your region")]
        public string State { get; set; }


        [Required(ErrorMessage = "Give your country")]
        public string Country { get; set; }

        public string Zip { get; set; }

       public bool GiftWrap { get; set; }
    }
}
