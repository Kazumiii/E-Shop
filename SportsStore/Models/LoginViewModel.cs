using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{//Here i can set what kind of dates from clients are necessery
    public class LoginViewModel
    {

        [Required(ErrorMessage ="User name is needed")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Wrong Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}