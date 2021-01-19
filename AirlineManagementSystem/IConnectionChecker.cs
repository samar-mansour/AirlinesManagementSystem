using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    //Helper Interface that prevents the duplication use of checking the connection "redundant code"
    public interface IConnectionChecker
    {
        bool GetOpenConnection(string conn);
    }
}
