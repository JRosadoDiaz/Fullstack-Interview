using ClientDashboardAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClientDashboardAPI.Factories
{
    public class MySqlConnectionService : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionService(IConfiguration configuration)
        {
            var server = "database";
            var port = "3306";
            var database = configuration.GetSection("ConnectionStrings")["MYSQL_DATABASE"];
            var user = configuration.GetSection("ConnectionStrings")["MYSQL_USER"];
            var password = configuration.GetSection("ConnectionStrings")["MYSQL_PASSWORD"];

            _connectionString = $"Server={server};Port={port};Database={database};Uid={user};Pwd={password};";
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}
