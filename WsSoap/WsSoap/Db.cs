using System;
using MySql.Data.MySqlClient;

namespace WsSoap
{
    public class Db: IDisposable
    {
        private readonly MySqlConnection _connection;

        public Db()
        {
            var connectionParams = new MySqlConnectionStringBuilder
                {
                    Server = "localhost",
                    Database = "wesite_rating",
                    UserID = "root",
                    Password = "damysql",
                    CharacterSet = "cp1251"
                };

            _connection =
                new MySqlConnection { ConnectionString = connectionParams.ConnectionString };

            _connection.Open();
        }

        public string GetLink()
        {
            var selectLink = _connection.CreateCommand();
            selectLink.CommandText = @"select ";

            return null;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}