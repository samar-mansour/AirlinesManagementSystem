using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    //Poco class that implement IPoco Interface 
    public class Country: IPoco
    {
        //country data properties 
        public int ID { get; set; }
        public string Name { get; set; }
        public string CodeCountryName { get; set; }

        public Country()
        {

        }

        //operators to check if in any case there's equality between two defferent IDs or null records
        public static bool operator ==(Country country1, Country country2)
        {
            if ((country1 == null) && (country2 == null))
                return true;
            if ((country1 == null) || (country2 == null))
                return false;
            return country1.ID == country2.ID;
        }

        public static bool operator !=(Country country1, Country country2)
        {
            return !(country1 == country2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Country country = obj as Country;
            if (country == null)
                return false;
            return this.ID == country.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }

        //using json to print all the properties of this class
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
