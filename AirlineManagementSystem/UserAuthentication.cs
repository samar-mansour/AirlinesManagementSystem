using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Authorization class for LoginService
/// check if the user exists or not
/// </summary>
namespace AirlineManagementSystem
{
    public class UserAuthentication
    {
        public static bool IsUserAuthorized(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(AppConfigFile.GetInstance().ConnectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("sp_login", conn))
                {
                    cmd.Parameters.AddWithValue("@username", name);
                    cmd.Parameters.AddWithValue("@password", password);

                    DataSet data = new DataSet();
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    adapter.Fill(data);

                    bool loginSuccessful = ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0));
                    if (loginSuccessful)
                    {
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetString(0)}, {reader.GetString(1)}");
                        }
                        Console.WriteLine("Login Success!");
                        //applay logger everytime it access!
                        return true;
                    }
                    else
                    {
                        return false;
                        throw new WorngCredentialsException("Invalid username or password!");
                        //apply logger evertime it faild!
                    }
                }
            }
        }
    }
}
