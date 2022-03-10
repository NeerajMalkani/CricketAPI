using MySqlConnector;
using System.Data;

namespace CricketAPI.Helpers
{
    public class MySQLHelper
    {
        public static MySqlConnection EstablishConnection(IConfiguration configuration) {
            MySqlConnection connection = new MySqlConnection(configuration.GetConnectionString("Default"));
            connection.Open();
            return connection;
        }

        public static MySqlCommand ExecuteCommand(MySqlConnection connection, string? command, CommandType commandType )
        {
            MySqlCommand mySqlCommand = new MySqlCommand(command, connection);
            mySqlCommand.CommandType = commandType;
            return mySqlCommand;
        }
    }
}
