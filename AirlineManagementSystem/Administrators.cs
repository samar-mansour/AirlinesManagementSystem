using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    //Poco class that inherint IPoco interface
    public class Administrators : IPoco, IUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public long UserID { get; set; }

        public Administrators()
        {

        }

        //operators to check if in any case there's equality between two defferent IDs or null records
        public static bool operator ==(Administrators admin1, Administrators admin2)
        {
            if ((admin1 == null) && (admin2 == null))
                return true;
            if ((admin1 == null) || (admin2 == null))
                return false;
            return admin1.ID == admin2.ID;
        }

        public static bool operator !=(Administrators admin1, Administrators admin2)
        {
            return !(admin1 == admin2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Administrators admin = obj as Administrators;
            if (admin == null)
                return false;
            return this.ID == admin.ID;
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