using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class Customer : IPoco
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreditCardNo { get; set; }
        public long UserId { get; set; }

        public Customer()
        {

        }

        public static bool operator ==(Customer customer1, Customer customer2)
        {
            if ((customer1 == null) && (customer2 == null))
                return true;
            if ((customer1 == null) || (customer2 == null))
                return false;
            return customer1.ID == customer2.ID;
        }

        public static bool operator !=(Customer customer1, Customer customer2)
        {
            return !(customer1 == customer2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Customer customer = obj as Customer;
            if (customer == null)
                return false;
            return this.ID == customer.ID;
        }


        public override int GetHashCode()
        {
            return this.ID;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
