using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    public interface ICountryDAO : IBasicDb<Country>
    {
        Users GetUserByUsername(string name);
    }
}
