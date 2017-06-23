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
        //token validity time
        const double defaultValidity = 10;

        MySqlConnection msqlConnection;
        MySqlCommand msqlCommand = new MySqlCommand();

        public Authenticate()
        {
            msqlConnection = new MySqlConnection("server = localhost; user id = root; Password = ; database = mabdd");
            msqlCommand.Connection = msqlConnection;
        }

        /*
         Check if user is in db with password
             */
        public bool checkUser(string username,string password)
        {
            msqlCommand.CommandText = "SELECT * FROM `users`;";
            try
            {
                msqlConnection.Open();
                MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                while (msqlReader.Read())
                {
                    //msqlReader.GetString(0); //user
                    //msqlReader.GetString(1); //password
                    //msqlReader.GetString(2); //id

                    if (string.Compare(username, msqlReader.GetString(0)) == 0)
                    {
                        Console.WriteLine("user found");

                        if (string.Compare(password, msqlReader.GetString(1)) == 0)
                        {
                            Console.WriteLine("password valid");
                            return true;

                        }
                        else { Console.WriteLine("Password invalid"); }
                    }
                    else { Console.WriteLine("user not found"); }
                }
            }
            catch (Exception er) { }
            finally
            {
                msqlConnection.Close();
            }
            return false;
        }

        /*
         Connect the user and returns a connection token
         adds this token to the session table with the default validity time
             */
        public string connect(string username, string password, string appToken)
        {
            if (checkUser(username,password))
            {
                //generate token
                string ConnectionToken = "";

                string.Concat(ConnectionToken, GetHashString(username));
                string.Concat(ConnectionToken, GetHashString(password));
                string.Concat(ConnectionToken, GetHashString(appToken));
                string.Concat(ConnectionToken, GetHashString(DateTime.Now.Second.ToString()));

                DateTime validity = DateTime.Now.AddMinutes(defaultValidity);
                string validityTime = validity.ToString("yyyy-MM-dd HH:mm:ss");

                Console.WriteLine(validityTime);
                msqlCommand.CommandText = string.Format("INSERT INTO `session`(`user`, `token`, `expires`) VALUES ('{0}','{1}','{2}')", username, password, validityTime);
                try
                {
                    msqlConnection.Open();
                    msqlCommand.ExecuteNonQuery();
                   
                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                }
                finally
                {
                    msqlConnection.Close();
                }
                return ConnectionToken = GetHashString(ConnectionToken);

            }
            else { 
            return "invalid";
            }
        }

        /*
         check if users has a valid token
         */
        public bool checkUserToken(string username)
        {
            msqlCommand.CommandText = "SELECT * FROM `session`;";
            try
            {
                msqlConnection.Open();
                MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                while (msqlReader.Read())
                {
                    //if user has token
                    if (string.Compare(username, msqlReader.GetString("user")) == 0)
                    {
                        //check token validity
                        DateTime valid = msqlReader.GetDateTime("expires");
                        if(valid > DateTime.Now)
                        {
                            Console.WriteLine("token time is still valid");
                            return true;
                        } else
                        {
                            Console.WriteLine("token time is invalid");
                            return false;
                        }
                    }
                }
            }
            catch (Exception er) { }
            finally
            {
                msqlConnection.Close();
            }

            return false;
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
