using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebUI.Infrastructure.Abstract
{
   public interface IAuthenticationProvider
    {
        bool Authenticate(string username, string password);
 
    }
}
