using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef
{
    public class sql_output
    {
        public static Dictionary<string, string> sql_result(string command)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();


            MySqlConnection connection = null;
            connection = new MySqlConnection("Server=MYSQL5013.Smarterasp.net;Database=db_a094d4_icdb;Uid=a094d4_icdb;Pwd=icdb123!;");
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand myCommand = new MySqlCommand(command, connection);
                connection.Open();
                
                reader = myCommand.ExecuteReader();
                while (reader.Read()) {
                result.Add(reader["items_date"].ToString(), reader["items"].ToString());

                }
                return result;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (connection != null)
                    connection.Close();
            }
            
        }
    }
}