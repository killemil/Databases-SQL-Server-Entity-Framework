namespace _09Excercise
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    class Excercise
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(
            @"Server= (localdb)\MSSQLLocalDB;DataBase = MinionsDB; Integrated Security = true");

            connection.Open();
            using (connection)
            {
                int id = int.Parse(Console.ReadLine());

                string increaseAgeQuery = File.ReadAllText("../../IncreaseAge.sql");
                SqlCommand increaseMinionAge = new SqlCommand(increaseAgeQuery, connection);
                SqlParameter idParam = new SqlParameter("@minionId", id);
                increaseMinionAge.Parameters.Add(idParam);

                increaseMinionAge.ExecuteNonQuery();

                string showMinionQuery = File.ReadAllText("../../ShowMinion.sql");
                SqlCommand showMinion = new SqlCommand(showMinionQuery, connection);
                SqlParameter idPara = new SqlParameter("@minionId", id);
                showMinion.Parameters.Add(idPara);

                var reader = showMinion.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]}");
                }
            }
        }
    }
}
