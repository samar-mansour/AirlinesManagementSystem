using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Login
{
    public class LoginService : ILoginService
    {
        private IAirlineCompanyDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        private IAdministratorsDAO _adminDAO;

        public bool TryLogin(string userName, string password, out LoginToken<IUser> token)
        {
            throw new NotImplementedException();
        }
    }
}
