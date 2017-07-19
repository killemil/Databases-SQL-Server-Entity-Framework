namespace _05Excercise
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;

    class Excercise
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(
                @"Server= (localdb)\MSSQLLocalDB; DataBase = MinionsDB; Integrated Security = true");
            connection.Open();
            using (connection)
            {
                var coutryName = Console.ReadLine();

                string query = File.ReadAllText("../../TownsToUpper.sql");
                SqlCommand changeTownNames = new SqlCommand(query, connection);
                SqlParameter coutryNameParam = new SqlParameter("@coutryName", coutryName);
                changeTownNames.Parameters.Add(coutryNameParam);

                int rowAffected = changeTownNames.ExecuteNonQuery();

                string getTownsQuery = File.ReadAllText("../../GetTowns.sql");
                SqlCommand getTowns = new SqlCommand(getTownsQuery, connection);
                SqlParameter coutryParam = new SqlParameter("@coutryName", coutryName);
                getTowns.Parameters.Add(coutryParam);

                var reader = getTowns.ExecuteReader();

                List<string> result = new List<string>();

                while (reader.Read())
                {
                    result.Add((string)reader[0]);
                }

                if (rowAffected == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    Console.WriteLine($"{rowAffected} town names were affected.");
                    Console.WriteLine($"[" + string.Join(", ", result) + "]");
                }
            }
        }
    }
}
