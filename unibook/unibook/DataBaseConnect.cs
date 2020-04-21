using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace unibook
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string UNIBOOK = string.Empty;
        public string DatabaseName
        {
            get { return UNIBOOK; }
            set { UNIBOOK = value; }
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
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(UNIBOOK))
                    return false;
                string connstring = string.Format("Server=134.122.59.58; database={0}; UID=root; password=gutterne", UNIBOOK);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
