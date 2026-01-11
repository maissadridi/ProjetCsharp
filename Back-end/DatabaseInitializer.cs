using Microsoft.Data.Sqlite;
using BackEnd;
using System.IO;

namespace Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            var dbPath = DbPaths.GetDbPath();
            


            using var connection = new SqliteConnection($"Data Source={dbPath}");
            connection.Open();

            Request.CreateTables(connection);
            Request.ClearData(connection);
        }
    }
}
