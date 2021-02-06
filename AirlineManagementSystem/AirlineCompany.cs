using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class AirlineCompany: IPoco,IUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }

        public AirlineCompany()
        {

        }

        public static bool operator ==(AirlineCompany airline1, AirlineCompany airline2)
        {
            if ((airline1 == null) && (airline2 == null))
                return true;
            if ((airline1 == null) || (airline2 == null))
                return false;
            return airline1.ID == airline2.ID;
        }

        public static bool operator !=(AirlineCompany airline1, AirlineCompany airline2)
        {
            return !(airline1 == airline2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            AirlineCompany airline = obj as AirlineCompany;
            if (airline == null)
                return false;
            return this.ID == airline.ID;
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
