using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Login
{
    public class LoginToken<T> where T : IUser
    {
        public T User { get; set; }

        public static implicit operator LoginToken<T>(LoginToken<Administrators> admin)
        {
            if (admin != null)
            {
                return admin;
            }
            return null;
        }

        public static implicit operator LoginToken<T>(LoginToken<Customer> cutomer)
        {
            if (cutomer != null)
            {
                return cutomer;
            }
            return null;
        }

        public static implicit operator LoginToken<T>(LoginToken<AirlineCompany> airline)
        {
            if (airline != null)
            {
                return airline;
            }
            return null;
        }
    }
}
