using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace unibook.Models
{
    public class UnibookContext
    {
        public string ConnectionString { get; set; }

        public UnibookContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server=127.0.0.1;Port=3306;Database=Unibook;User=root;Pwd=Gutterne2020;Connection Timeout = 120;");
        }
        public List<Books> GetAllBooks()
        {
            List<Books> list = new List<Books>();
             using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Books;", conn); //Instatiates a MySQL command line. 

                using var reader = cmd.ExecuteReader() ;
                {
                    while (reader.Read())
                    {
                        list.Add(new Books()
                        {
                            ISBN = reader["ISBN"].ToString(),
                            Author = reader["Author"].ToString(),
                            Edition = reader["Edition"].ToString(),
                            Title = reader["Title"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}  

