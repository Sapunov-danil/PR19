using MySql.Data.MySqlClient;

namespace Kino_Sapunov.Classes
{
    public class Connection
    {
        private static readonly string connectionString = "server=127.0.0.1;port=3306;database=Kino;uid=root;";

        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static MySqlDataReader Query(string sql, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            return command.ExecuteReader();
        }

        public static void CloseConnection(MySqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
                MySqlConnection.ClearPool(connection);
            }
        }
    }
}
