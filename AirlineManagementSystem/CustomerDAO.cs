using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class CustomerDAO : ConnectionDataInfo, ICustomerDAO
    {
        public void Add(Customer t)
        {
            var res_sp_add = Run_Sp(m_conn, "sp_add_customers", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_firstName", t.FirstName),
                new NpgsqlParameter("_lastName", t.LastName),
                new NpgsqlParameter("_address", t.Address),
                new NpgsqlParameter("_phone", t.PhoneNo),
                new NpgsqlParameter("_creditCard", t.CreditCardNo),
                new NpgsqlParameter("_userId", t.UserId)
            });
            Console.WriteLine($"Added new customer");
        }

        public Customer Get(int id)
        {
            Customer customer = new Customer();
            var res_sp_get = Run_Sp(m_conn, "sp_get_customer_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("c_id", id)
            });
            if (res_sp_get != null)
            {
                customer.ID = id;
            }
            return customer;
        }

        public IList<Customer> GetAll()
        {
            IList<Customer> customer = new List<Customer>();
            Customer reader = new Customer();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_customer", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",reader.ID),
                new NpgsqlParameter("firstName", reader.FirstName),
                new NpgsqlParameter("lastName", reader.LastName),
                new NpgsqlParameter("address", reader.Address),
                new NpgsqlParameter("phone", reader.PhoneNo),
                new NpgsqlParameter("creditCard", reader.CreditCardNo),
                new NpgsqlParameter("userId",reader.UserId)

            });
            customer = (IList<Customer>)res_sp_get_all.Select(item => item.Values).ToList();
            return customer;
        }

        public Customer GetCustomerByUsername(string name)
        {
            Customer customer = new Customer();
            List<Customer> customerList = new List<Customer>();

            var res_sp_get_customer_username = Run_Sp(m_conn, "sp_get_customer_by_username", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username", name)
            });
            customerList.AddRange((IEnumerable<Customer>)res_sp_get_customer_username);
            customer = (Customer)customerList.Select(a => res_sp_get_customer_username);
            return customer;
        }

        public void Remove(Customer t)
        {
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_customer", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_id",t.ID)
            });
            Console.WriteLine($"Run_Sp_Remove => {res_sp_remove}\n{t.FirstName} customer was removed successfully");
        }

        public void Update(Customer t)
        {
            Console.Write("Enter user ID to update information: ");
            int newID = Convert.ToInt32(Console.ReadLine());
            var res_sp_update = Run_Sp(m_conn, "sp_update_customer", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_firstName", t.FirstName),
                new NpgsqlParameter("_lastName", t.LastName),
                new NpgsqlParameter("_address", t.Address),
                new NpgsqlParameter("_phone", t.PhoneNo),
                new NpgsqlParameter("_creditCard", t.CreditCardNo),
                new NpgsqlParameter("_userId",t.UserId),
                new NpgsqlParameter("new_id", newID)
            });
            Console.WriteLine($"Successfully Updated customer information => [{res_sp_update}]");
        }
    }
}
