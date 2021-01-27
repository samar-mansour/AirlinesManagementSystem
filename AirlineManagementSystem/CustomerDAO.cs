using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class CustomerDAO : ConnectionHelper, ICustomerDAO
    {
        public void Add(Customer t)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerByUsername(string name)
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer t)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer t)
        {
            throw new NotImplementedException();
        }
    }
}
