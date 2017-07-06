using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.Models
{
    //This class is a view model and provides informations regarding:
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int CuretnPage { get; set; }
        public int ItemsPerPage { get; set; }

        public int TotalPages
        {

            get
            {
                return ((int) Math.Ceiling((decimal)TotalItems/ItemsPerPage));

            }

        }
    }
}