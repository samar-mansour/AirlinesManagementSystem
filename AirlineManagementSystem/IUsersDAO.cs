using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public interface IUsersDAO : IBasicDb<Users>
    {
        Users GetUserByUsername(string name);
    }
}
