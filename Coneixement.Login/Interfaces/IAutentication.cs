using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Coneixement.Login.Interfaces
{
    interface IAutentication
    {
        bool AuthorizeUser(String UserName, String PassWord);
    }
}
