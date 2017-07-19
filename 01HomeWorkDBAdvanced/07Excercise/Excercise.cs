namespace _07Excercise
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
                string getAllMinionsQuery = File.ReadAllText("../../AllMinions.sql");
                SqlCommand allMinionsNames = new SqlCommand(getAllMinionsQuery, connection);
                var reader = allMinionsNames.ExecuteReader();

                List<string> nameList = new List<string>();

                while (reader.Read())
                {
                    nameList.Add(reader[0].ToString());
                }

                int index = 0;

                for (int i = 0; i < nameList.Count / 2; i++)
                {
                    Console.WriteLine(nameList[index]);
                    Console.WriteLine(nameList[nameList.Count - index - 1]);
                    index++;
                }
            }
        }
    }
}
