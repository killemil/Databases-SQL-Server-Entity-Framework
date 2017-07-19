namespace _06Excercise
{
    using System;
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
                int id = int.Parse(Console.ReadLine());
                string minionsCountQuery = File.ReadAllText("../../CountMinions.sql");
                SqlCommand minionsCount = new SqlCommand(minionsCountQuery, connection);
                SqlParameter idParam = new SqlParameter("@villainId", id);
                minionsCount.Parameters.Add(idParam);

                int minionsCountResult = (int)minionsCount.ExecuteScalar();

                string villainNameQuery = File.ReadAllText("../../GetVillainName.sql");
                SqlCommand villainName = new SqlCommand(villainNameQuery, connection);
                SqlParameter param = new SqlParameter("@villainId", id);
                villainName.Parameters.Add(param);

                string villainNameResult = (string)villainName.ExecuteScalar();

                string deleteVillainQuery = File.ReadAllText("../../RemoveVillan.sql");
                SqlCommand deleteVillain = new SqlCommand(deleteVillainQuery, connection);
                SqlParameter delParam = new SqlParameter("@villainId", id);
                deleteVillain.Parameters.Add(delParam);

                deleteVillain.ExecuteNonQuery();

                if (villainNameResult == null)
                {
                    Console.WriteLine("No such villain was found");
                }
                else
                {
                    Console.WriteLine($"{villainNameResult} was deleted");
                    Console.WriteLine($"{minionsCountResult} minions released");
                }

            }
        }
    }
}
