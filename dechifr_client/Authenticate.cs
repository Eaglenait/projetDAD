using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dechifr_client
{
    class Authenticate
    {
        public Authenticate() {}

        /*
         Check if user is in db
             */
        public bool checkUser(string username)
        {
            var dbCon = DBConnection.Instance();
            
            string query = string.Format("SELECT user FROM users", username);

            var cmd = new MySqlCommand(query, dbCon.Connection);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string col_username = reader.GetString(1);

                Console.WriteLine(reader.GetString(0));//id
                Console.WriteLine(reader.GetString(1));//password ?
                Console.WriteLine(reader.GetString(2));//username ?
            }

            return false;
        }

        /*
         Connect the user and return a connection token
             */
        string connect(string username, string password, string appToken)
        {
            //TODO
            return "TODO";
        }
        
        /*
         generate connection (user) token when a valid user is logging in 
         Token = hash( hash(username) + hash(password) + hash(apptoken))
         */
        string getUserToken()
        {
            //TODO
            return "xception";
        }
    }
}
