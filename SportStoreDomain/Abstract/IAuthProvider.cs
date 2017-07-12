using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStoreDomain.Abstract
{
//Provide authentication metohd in order to log in admin's account
   public interface IAuthProvider
    {

        bool Authenticate(string userName, string password);
    }
}
