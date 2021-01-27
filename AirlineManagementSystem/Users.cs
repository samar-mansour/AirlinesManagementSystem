using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class Users : IPoco
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int UserRole { get; set; }

        public Users()
        {

        }

        public static bool operator ==(Users user1, Users user2)
        {
            if ((user1 == null) && (user2 == null))
                return true;
            if ((user1 == null) || (user2 == null))
                return false;
            return user1.ID == user2.ID;
        }

        public static bool operator !=(Users user1, Users user2)
        {
            return !(user1 == user2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Users user = obj as Users;
            if (user == null)
                return false;
            return this.ID == user.ID;
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
