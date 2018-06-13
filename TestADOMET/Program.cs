using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestADOMET
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=10.7.0.5;Initial Catalog=SemenFistDB;User ID=test;Password=123456qwerty";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int action = 0;
                do
                {
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("1. Add");
                    Console.WriteLine("2. Show");
                    action = int.Parse(Console.ReadLine());
                    switch(action)
                    {
                        case 1:
                            {
                                Add(connection);
                                break;
                            }
                        case 2:
                            {
                                Show(connection);
                                break;
                            }
                    }
                } while (action!=0);
            }
        }
        static void Show(SqlConnection connection)
        {
            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT Id, Name, Email FROM tblUsers";
            // Create the Command and Parameter objects.
            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}",
                        reader["Id"], reader["Name"], reader["Email"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Add(SqlConnection connection)
        {
            string name, email;
            Console.WriteLine("Enter name: ");
            name = Console.ReadLine();
            Console.WriteLine("Enter email: ");
            email = Console.ReadLine();
            string queryString = $"INSERT INTO tblUsers(Name, Email) " +
                $"VALUES ('{name}','{email}');";
            // Create the Command and Parameter objects.
            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                int result = command.ExecuteNonQuery();
                if(result!=0)
                {
                    Console.WriteLine("Add is completed");
                }
                else
                {
                    Console.WriteLine("Invalid insert to database");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
