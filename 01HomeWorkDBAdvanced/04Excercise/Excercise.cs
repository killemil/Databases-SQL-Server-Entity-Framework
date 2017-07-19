namespace _04Excercise
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
                @"Server= (localdb)\MSSQLLocalDB; Integrated Security = true");

            Console.Write("Minion: ");
            List<string> input = Console.ReadLine().Split(' ').ToList();
            string minionName = input[0];
            int minionAge = int.Parse(input[1]);
            string minionTown = input[2];
            Console.Write("Villain: ");
            var villainName = Console.ReadLine();

            connection.Open();
            using (connection)
            {
                CheckTownExistence(connection, minionTown);
                CheckVillainExistence(connection, villainName);
                CheckMinionExistence(connection, minionName, minionAge, minionTown);
                DataInsertion(connection, minionName, villainName);
            }
        }

        private static void DataInsertion(SqlConnection connection, string minionName, string villainName)
        {
            string query = File.ReadAllText("../../Insertion.sql");
            SqlCommand insertion = new SqlCommand(query, connection);
            SqlParameter minionNameParam = new SqlParameter("@minionName", minionName);
            SqlParameter villainNameParam = new SqlParameter("@villainName", villainName);
            insertion.Parameters.Add(minionNameParam);
            insertion.Parameters.Add(villainNameParam);

            insertion.ExecuteNonQuery();

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}");

        }

        private static void CheckMinionExistence(SqlConnection connection, string minionName, int minionAge, string minionTown)
        {
            string checkMinionQuery = File.ReadAllText("../../CheckMinion.sql");
            SqlCommand findMinion = new SqlCommand(checkMinionQuery, connection);
            SqlParameter minionParam = new SqlParameter("@minionName", minionName);
            findMinion.Parameters.Add(minionParam);

            var result = findMinion.ExecuteScalar();

            if (result == null)
            {
                string insertMinionQuery = File.ReadAllText("../../InsertMinion.sql");
                SqlCommand insertMinion = new SqlCommand(insertMinionQuery, connection);
                SqlParameter minionNameParam = new SqlParameter("@minionName", minionName);
                SqlParameter minionAgeParam = new SqlParameter("@Age", minionAge);
                SqlParameter minionTownParam = new SqlParameter("@townName", minionTown);
                insertMinion.Parameters.Add(minionNameParam);
                insertMinion.Parameters.Add(minionAgeParam);
                insertMinion.Parameters.Add(minionTownParam);

                insertMinion.ExecuteNonQuery();

                Console.WriteLine($"Minion {minionName} was added to the database.");
            }
        }

        public static void CheckTownExistence(SqlConnection connection, string minionTown)
        {
            string checkTownQuery = File.ReadAllText("../../CheckTown.sql");
            SqlCommand findTown = new SqlCommand(checkTownQuery, connection);
            SqlParameter townParam = new SqlParameter("@townName", minionTown);
            findTown.Parameters.Add(townParam);

            var result = findTown.ExecuteScalar();

            if (result == null)
            {
                string insertTownQuery = File.ReadAllText("../../InsertTown.sql");
                SqlCommand insertTown = new SqlCommand(insertTownQuery, connection);
                SqlParameter townNameParam = new SqlParameter("@townName", minionTown);
                insertTown.Parameters.Add(townNameParam);

                insertTown.ExecuteNonQuery();

                Console.WriteLine($"Town {minionTown} was added to the database.");
            }
        }
        public static void CheckVillainExistence(SqlConnection connection, string villainName)
        {
            string checkVillainQuery = File.ReadAllText("../../CheckVillain.sql");
            SqlCommand findVillain = new SqlCommand(checkVillainQuery, connection);
            SqlParameter villainParam = new SqlParameter("@villainName", villainName);
            findVillain.Parameters.Add(villainParam);

            var result = findVillain.ExecuteScalar();

            if (result == null)
            {
                string insertVillainQuery = File.ReadAllText("../../InsertVillain.sql");
                SqlCommand insertVillain = new SqlCommand(insertVillainQuery, connection);
                SqlParameter villainNameParam = new SqlParameter("@villainName", villainName);
                insertVillain.Parameters.Add(villainNameParam);

                insertVillain.ExecuteNonQuery();

                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
        }
    }
}
