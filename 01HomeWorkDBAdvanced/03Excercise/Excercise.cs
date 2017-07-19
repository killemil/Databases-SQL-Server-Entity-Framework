namespace _03Excercise
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    class Excercise
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(
                @"Server= (localdb)\MSSQLLocalDB; DataBase= MinionsDB; Integrated Security = true");


            connection.Open();
            using (connection)
            {
                var villainId = int.Parse(Console.ReadLine());
                string villainQuery = File.ReadAllText("../../VillainById.sql");

                SqlCommand findVillainById = new SqlCommand(villainQuery, connection);

                SqlParameter villainIdParam = new SqlParameter("@villainId", villainId);
                findVillainById.Parameters.Add(villainIdParam);

                SqlDataReader reader = findVillainById.ExecuteReader();

                if (reader.Read())
                {
                    string villainName = (string)reader[0];
                    Console.WriteLine($"Villain: {villainName}");

                    string minionsQuery = File.ReadAllText("../../MinionsByVillain.sql");
                    SqlCommand findMinionsByVillain = new SqlCommand(minionsQuery, connection);

                    SqlParameter param = new SqlParameter("@villainId", villainId);
                    findMinionsByVillain.Parameters.Add(param);

                    reader.Close();
                    SqlDataReader minionsReader = findMinionsByVillain.ExecuteReader();

                    int index = 1;

                    while (minionsReader.Read())
                    {
                        string minionName = (string)minionsReader[0];
                        int minionAge = (int)minionsReader[1];

                        Console.WriteLine($"{index}. {minionName} {minionAge}");
                        index++;
                    }
                }
                else
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }
            }
        }
    }
}
