﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public interface ICustomerDAO: IBasicDb<Customer>
    {
        Customer GetCustomerByUsername(string name);
    }
}
