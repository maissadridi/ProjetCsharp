using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;


namespace BackEnd
{

    public class Request
    {

        // ==========================
        // Création des tables SQLite
        // ==========================
        public static void CreateTables(SqliteConnection connection)
        {
            var sql = @"
            CREATE TABLE IF NOT EXISTS Users (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                nom TEXT NOT NULL,
                prenom TEXT NOT NULL,
                pseudo TEXT NOT NULL UNIQUE,
                email TEXT UNIQUE,
                password_hash TEXT NOT NULL,
                password_salt TEXT NOT NULL,
                points INTEGER NOT NULL DEFAULT 0
            );

            CREATE TABLE IF NOT EXISTS Objects (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                type_objet INTEGER NOT NULL,       -- enum TypeObjet
                etat INTEGER NOT NULL,             -- enum EtatObjet
                disponible INTEGER NOT NULL DEFAULT 1,
                owner_id INTEGER NOT NULL,
                FOREIGN KEY (owner_id) REFERENCES Users(id)
            );

            CREATE TABLE IF NOT EXISTS Exchanges (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                objet_propose_id INTEGER NOT NULL,
                objet_demande_id INTEGER,          -- peut être NULL si don
                from_user_id INTEGER NOT NULL,
                to_user_id INTEGER NOT NULL,
                etat INTEGER NOT NULL,             -- enum EtatEchange
                date_created TEXT NOT NULL,
                FOREIGN KEY (objet_propose_id) REFERENCES Objects(id),
                FOREIGN KEY (objet_demande_id) REFERENCES Objects(id),
                FOREIGN KEY (from_user_id) REFERENCES Users(id),
                FOREIGN KEY (to_user_id) REFERENCES Users(id)
            );
            ";

            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }


        // ===============================
        // Compter les utilisateurs
        // ===============================
        public static long CountUsers(SqliteConnection connection)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Users";
            var result = cmd.ExecuteScalar();
            return (result is long l) ? l : Convert.ToInt64(result);
        }

        // =================================
        // Création d'un utilisateur avec hash
        // =================================
        public static void CreateUser(SqliteConnection connection, string username, string email, string password)
        {
            (string hash, string salt) = HashPassword(password);

            var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT OR IGNORE INTO Users (username, email, password_hash, password_salt)
            VALUES ($username, $email, $hash, $salt);
        ";

            command.Parameters.AddWithValue("$username", username);
            command.Parameters.AddWithValue("$email", email);
            command.Parameters.AddWithValue("$hash", hash);
            command.Parameters.AddWithValue("$salt", salt);

            int rows = command.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine($"➡️ Utilisateur '{username}' créé.");
            else
                Console.WriteLine($"ℹ️ Utilisateur '{username}' déjà présent (INSERT ignoré).");
        }

        // ===============================
        // Affiche les utilisateurs en base
        // ===============================
        public static void DisplayUsers(SqliteConnection connection)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT id, username, email FROM Users";

            using var reader = cmd.ExecuteReader();

            Console.WriteLine("\n📌 Utilisateurs enregistrés :");

            int count = 0;

            while (reader.Read())
            {
                count++;
                Console.WriteLine($" - {reader.GetInt32(0)} : {reader.GetString(1)} ({reader.GetString(2)})");
            }

            if (count == 0)
            {
                Console.WriteLine("⚠️ Aucun utilisateur trouvé dans la table Users.");
            }
        }

        // ==========================
        // Hash PBKDF2 sécurisé
        // ==========================
        public static (string hash, string salt) HashPassword(string password)
        {
            // 1. salt aléatoire
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);

            // 2. Hash PBKDF2
            byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                100_000,                 // itérations
                HashAlgorithmName.SHA256,
                32                       // taille du hash
            );

            return (
                Convert.ToBase64String(hashBytes),
                Convert.ToBase64String(saltBytes)
            );
        }




        public static (int id, string nom, string prenom, string pseudo, int points, string hash, string salt)? GetUserAuthByPseudo(SqliteConnection connection, string pseudo)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT id, nom, prenom, pseudo, points, password_hash, password_salt
                        FROM Users WHERE pseudo = $p";
            cmd.Parameters.AddWithValue("$p", pseudo);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return (
                r.GetInt32(0),
                r.GetString(1),
                r.GetString(2),
                r.GetString(3),
                r.GetInt32(4),
                r.GetString(5),
                r.GetString(6)
            );
        }

        public static bool VerifyPassword(string password, string storedHashBase64, string storedSaltBase64)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSaltBase64);

            byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                100_000,
                HashAlgorithmName.SHA256,
                32
            );

            string computedHash = Convert.ToBase64String(hashBytes);
            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(storedHashBase64),
                Convert.FromBase64String(computedHash)
            );
        }





        public static void ClearData(SqliteConnection connection)
        {
            var sql = @"
            PRAGMA foreign_keys = OFF;

            DELETE FROM Exchanges;
            DELETE FROM Objects;
            DELETE FROM Users;

            PRAGMA foreign_keys = ON;
            ";

            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }






    }

}