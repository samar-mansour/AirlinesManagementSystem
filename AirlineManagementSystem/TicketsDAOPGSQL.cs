using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class TicketsDAOPGSQL : ConnectionDataInfo, ITicketsDAO
    {
        public void Add(Tickets t)
        {
            var res_sp_add = Run_Sp(m_conn, "sp_add_tickets", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_flight_id", t.FlightID),
                new NpgsqlParameter("_customer_id", t.CustomerID)
            });
            res_sp_add.ForEach(ticket => Console.WriteLine($"New [{ticket}] has added successfully "));
        }

        public Tickets Get(int id)
        {
            List<Tickets> ticketsList = new List<Tickets>();
            Tickets ticket = new Tickets();
            var res_sp_get = Run_Sp(m_conn, "sp_get_ticket_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("t_id", id)
            });
            ticketsList.AddRange((IEnumerable<Tickets>)res_sp_get);
            ticket = (Tickets)ticketsList.Select(a => res_sp_get);
            return ticket;
        }

        public IList<Tickets> GetAll()
        {
            IList<Tickets> ticket = new List<Tickets>();
            Tickets reader = new Tickets();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_tickets", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id", reader.ID),
                new NpgsqlParameter("flightID",reader.FlightID),
                new NpgsqlParameter("customerID",reader.CustomerID)

            });
            ticket = (IList<Tickets>)res_sp_get_all.Select(item => item.Values).ToList();
            return ticket;
        }

        public void Remove(Tickets t)
        {
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_airline_company", new NpgsqlParameter[]
            {
                new NpgsqlParameter("a_id",t.ID)
            });
            Console.WriteLine($"Run_Sp_Remove => {res_sp_remove}");
        }

        public void Update(Tickets t)
        {
            Console.Write("Enter user ID to update information: ");
            int newID = Convert.ToInt32(Console.ReadLine());
            var res_sp_update = Run_Sp(m_conn, "sp_update_ticket", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_flight_id",t.FlightID),
                new NpgsqlParameter("_customer_id", t.CustomerID),
                new NpgsqlParameter("new_id", newID)
            });
            Console.WriteLine($"Successfully Updated users information");
        }
    }
}
