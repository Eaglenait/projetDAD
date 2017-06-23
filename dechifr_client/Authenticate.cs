using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace dechifr_client
{
    class Authenticate
    {
        MySqlConnection msqlConnection;
        MySqlCommand msqlCommand = new MySqlCommand();

        public Authenticate()
        {
            msqlConnection = new MySqlConnection("server = localhost; user id = root; Password = ; database = mabdd");
            msqlCommand.Connection = msqlConnection;
        }

        /*
         Check if user is in db
             */
        public bool checkUser(string username)
        {
            msqlCommand.CommandText = "SELECT user FROM `users`;";
            try
            {
                msqlConnection.Open();
                MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                while (msqlReader.Read())
                {
                    if (string.Compare(username, msqlReader.GetString(0)) == 0)
                    {
                        Console.WriteLine("user found");
                    }
                    else
                    {
                        Console.WriteLine("user not found");
                    }
                }
            }catch(Exception er) { }
            finally
            {
                msqlConnection.Close();
            }
            return false;
        }

        /*
         Connect the user and return a connection token
             */
        public string connect(string username, string password, string appToken)
        {
            if (checkUser(username))
            {
                string ConnectionToken = "";

                string.Concat(ConnectionToken, GetHashString(username));
                string.Concat(ConnectionToken, GetHashString(password));
                string.Concat(ConnectionToken, GetHashString(appToken));
                string.Concat(ConnectionToken, GetHashString(DateTime.Now.Second.ToString()));

                return ConnectionToken = GetHashString(ConnectionToken);

            }
            else { 
            return "invalid";
            }
        }

        /*
         generate connection (user) token when a valid user is logging in 
         Token = hash( hash(username) + hash(password) + hash(apptoken))
         */
        string getUserToken(string username, string password, string apptoken)
        {
            string hash_username = GetHashString(username);

            //TODO
            return "xception";
        }

        /*
         Method to convert the input string into a hased byte array
             */
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /*
           convert hash array into hased string
             */
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
