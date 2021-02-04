using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class Flights: IPoco
    {
        public int ID { get; set; }
        public long AirlineCompId { get; set; }
        public int OriginCountryId { get; set; }
        public int DestinationCountryId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }

        public Flights()
        {

        }

        public static bool operator ==(Flights flight1, Flights flight2)
        {
            if ((flight1 == null) && (flight2 == null))
                return true;
            if ((flight1 == null) || (flight2 == null))
                return false;
            return flight1.ID == flight2.ID;
        }

        public static bool operator !=(Flights flight1, Flights flight2)
        {
            return !(flight1 == flight2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Flights flight = obj as Flights;
            if (flight == null)
                return false;
            return this.ID == flight.ID;
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
