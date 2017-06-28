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
    class Authenticate : IDisposable
    {

        //token validity time (in minutes)
        const double defaultValidity = 10;

        MySqlConnection msqlConnection;
        MySqlCommand msqlCommand = new MySqlCommand();

        /// <summary>
        /// public constructor
        /// </summary>
        public Authenticate()
        {
            msqlConnection = new MySqlConnection("server = localhost; user id = root; Password = ; database = mabdd");
            msqlCommand.Connection = msqlConnection;
        }

        public void Dispose()
        {
            msqlConnection.Dispose();
            msqlCommand.Dispose();
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
                    if (string.Compare(username, msqlReader.GetString("user")) == 0)
                    {
                        Console.WriteLine("user found");

                        if (string.Compare(password, msqlReader.GetString("password")) == 0)
                        {
                            Console.WriteLine("password valid");
                            return true;

                        }
                        else { Console.WriteLine("Password invalid"); }
                    }
                    else { Console.WriteLine("user not found"); }
                }
            }
            catch (Exception er) { Console.WriteLine(er.Message); }
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
            if (checkUser(username, password))
            {
                //generate token
                /*
                 TOKEN Format :
                 MD5(
                     MD5( username )
                     MD5( password )
                     MD5( apptoken ) 
                     MD5( current seconds )
                 ) 
                 */
                string hashed_username = GetHashString(username);
                string hashed_password = GetHashString(password);
                string hashed_appToken = GetHashString(appToken);
                string hashed_time = GetHashString(DateTime.Now.Ticks.ToString());

                string hashed_concat = hashed_username + hashed_password + hashed_appToken + hashed_time;
                
                string ConnectionToken = GetHashString(hashed_concat); //finalise hash

                //time that the token will be valid (defaultvalidity is the time in minutes that the token is valid)
                DateTime validity = DateTime.Now.AddMinutes(defaultValidity);
                string validityTime = validity.ToString("yyyy-MM-dd HH:mm:ss"); // convert to MySQL datetime Format

                //we prepare the insert command in case we want to prepend a DELETE command
                string insertCommand = string.Format("INSERT INTO `session`(`user`, `token`, `expires`) VALUES ('{0}','{1}','{2}')", username, ConnectionToken, validityTime);
                
                //check that the user doesn't already have a token in database
                msqlCommand.CommandText = string.Format("SELECT user FROM `session`");
                bool duplicate = false; //if there is a duplicate user token
                try
                {

                    msqlConnection.Open();
                    MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                    while (msqlReader.Read())
                    {
                        //if user is already in base
                        if(string.Compare(msqlReader.GetString("user"), username) == 0)
                        {
                            duplicate = true;
                        }
                    }
                }catch(Exception er ) { Console.WriteLine(er.Message); }
                finally
                {
                    msqlConnection.Close();
                }

                //if there is a duplicate user token we add to the insertion command a delete statement
                if(duplicate)
                {
                    string deleteStatement = string.Format("DELETE FROM `session` WHERE `user` = '{0}'; ", username);
                    insertCommand = deleteStatement + insertCommand;
                    Console.WriteLine(insertCommand);
                }

                //now we insert the token 'session' in the database
                msqlCommand.CommandText = insertCommand;
                try
                {
                    msqlConnection.Open();
                    msqlCommand.ExecuteNonQuery();
                   
                }
                catch (Exception er)
                {
                    //Debug MySQL error message
                    Console.WriteLine("MySQL session token insert error : ");
                    Console.WriteLine(er.Message);
                }
                finally
                {
                    msqlConnection.Close();
                }
                return ConnectionToken;

            }
            else { 
            return "invalid";
            }
        }

        /*
         check if users has a valid connection token
         */
        public bool checkUserToken(string username, string token)
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
                        Console.WriteLine("user is in token table");

                        //check token validity
                        DateTime valid = msqlReader.GetDateTime("expires");
                        if (valid > DateTime.Now)
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
            catch (Exception er) { Console.WriteLine(er.Message); }
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
        Convert hash array into hased string
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
