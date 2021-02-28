using AirlineManagementSystem.BusinessLogic_Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Login
{
    public interface ILoginService
    {
        bool TryLogin(string userName, string password, out LoginToken<IUser> token, out FacadeBase facadeBase);
    }
}
