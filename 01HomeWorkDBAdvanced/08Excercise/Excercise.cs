namespace _08Excercise
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;

    class Excercise
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(
              @"Server= (localdb)\MSSQLLocalDB; DataBase = MinionsDB; Integrated Security = true");

            connection.Open();
            using (connection)
            {
                List<int> ids = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

                for (int i = 0; i < ids.Count; i++)
                {
                    string query = File.ReadAllText("../../IncrementAge.sql");
                    SqlCommand incrementAge = new SqlCommand(query, connection);
                    SqlParameter param = new SqlParameter("@minionsId", ids[i]);
                    incrementAge.Parameters.Add(param);

                    incrementAge.ExecuteNonQuery();
                }

                string allMinionsQuery = File.ReadAllText("../../AllMinions.sql");
                SqlCommand allMinions = new SqlCommand(allMinionsQuery, connection);

                var reader = allMinions.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]}");
                }
            }
        }
    }
}
