using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class Tickets : IPoco
    {
        public int ID { get; set; }
        public int FlightID { get; set; }
        public int CustomerID { get; set; }

        public Tickets()
        {

        }

        public static bool operator ==(Tickets ticket1, Tickets ticket2)
        {
            if ((ticket1 == null) && (ticket2 == null))
                return true;
            if ((ticket1 == null) || (ticket2 == null))
                return false;
            return ticket1.ID == ticket2.ID;
        }

        public static bool operator !=(Tickets ticket1, Tickets ticket2)
        {
            return !(ticket1 == ticket2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Tickets ticket = obj as Tickets;
            if (ticket == null)
                return false;
            return this.ID == ticket.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

        //using json to print all the properties of this class --> from Newtonsoft nuget
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
}
