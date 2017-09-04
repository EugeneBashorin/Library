using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LibraryProject.Extention_Classes
{
    public static class MagazineExtention
    {
        private static string writePath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/magazines.txt";
        private static string writeXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/magazines.xml";
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True";

        public static void GetTxtList(this List<Magazine> list)
        {
            StringBuilder result = new StringBuilder(130);

            if (list.Count > 0)
            {
                foreach (Magazine item in list)
                {
                    result.AppendLine($"Name: {item.Name} Author: {item.Category} Publisher: {item.Publisher} Price: {item.Price.ToString()}");
                }
            }

            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public static void GetXmlList(this List<Magazine> xmlMagazinesList)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Magazine>));

            using (FileStream fs = new FileStream(writeXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlMagazinesList);
            }
        }

        public static void SetMagazineListToDb(this List<Magazine> magazineList)
        {
            string sqlExpression = "INSERT INTO Magazines ([Id], [Name], [Category], [Publisher],[Price]) VALUES";

            foreach (Magazine item in magazineList)
            {
                if (item == magazineList.Last())
                {
                    sqlExpression += $"('{item.Id}','{item.Name}','{item.Category}','{item.Publisher}','{item.Price}');";
                }
                else
                {
                    sqlExpression += $"('{item.Id}','{item.Name}','{item.Category}','{item.Publisher}','{item.Price}'),";
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("DELETE FROM Magazines", con);
                try
                {
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                command = new SqlCommand(sqlExpression, con);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }

    }
}