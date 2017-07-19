
namespace HomeWorkDBAdvanced
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    class Excercise
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(@"
            Server= (localdb)\MSSQLLocalDB; DataBase= MinionsDB; Integrated Security = true");

            connection.Open();

            using (connection)
            {
                string query = File.ReadAllText("../../MinionsCount.sql");

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        var firstName = (string)reader[0];
                        var minionsCount = (int)reader[1];

                        Console.WriteLine("{0} {1}", firstName, minionsCount);
                    }
                }

            }
        }
    }
}
