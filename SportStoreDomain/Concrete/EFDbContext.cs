using SportStoreDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreDomain.Concrete
{

    //Here I create context class  associate  my model with database

public  class EFDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
