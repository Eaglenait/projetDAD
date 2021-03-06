﻿using System;
using MySql.Data.MySqlClient;

namespace dechifr_client
{
    public class DBConnection
    {
        private DBConnection() { }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }

        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if(_instance == null)
            {
                new DBConnection();
            }
            return _instance;

        }

        public bool IsConnected()
        {
            bool result = true;
            if(Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    result = true;
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password=", databaseName);
                connection = new MySqlConnection(connstring);
                connection.Open();
                result = true;
            }
            return result;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
