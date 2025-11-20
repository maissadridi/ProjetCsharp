using System;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== DÉBUT PROGRAM SQLITE ===");

        string connectionString = "Data Source=base.db";

        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        Console.WriteLine("➡️ Base ouverte : base.db");

        CreateTables(connection);
        Console.WriteLine("➡️ Tables créées / vérifiées.");

        Console.WriteLine($"👉 Users en base AVANT insertion : {CountUsers(connection)}");

        // On essaie de créer toujours le même utilisateur
        CreateUser(connection, "mohamed", "mohamed@example.com", "monMotDePasse123");

        Console.WriteLine($"👉 Users en base APRÈS insertion : {CountUsers(connection)}");

        DisplayUsers(connection);

        Console.WriteLine("=== FIN PROGRAM SQLITE ===");
        Console.WriteLine("Appuie sur Entrée pour fermer...");
        Console.ReadLine();
    }

    // ==========================
    // Création des tables SQLite
    // ==========================
    static void CreateTables(SqliteConnection connection)
    {
        var sql = @"
        CREATE TABLE IF NOT EXISTS Users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL,
            email TEXT UNIQUE,
            password_hash TEXT NOT NULL,
            password_salt TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Objects (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            description TEXT,
            owner_id INTEGER NOT NULL,
            FOREIGN KEY (owner_id) REFERENCES Users(id)
        );

        CREATE TABLE IF NOT EXISTS Exchanges (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            object_id INTEGER NOT NULL,
            from_user_id INTEGER NOT NULL,
            to_user_id INTEGER NOT NULL,
            date_exchanged TEXT NOT NULL,
            FOREIGN KEY (object_id) REFERENCES Objects(id),
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
    static long CountUsers(SqliteConnection connection)
    {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM Users";
        var result = cmd.ExecuteScalar();
        return (result is long l) ? l : Convert.ToInt64(result);
    }

    // =================================
    // Création d'un utilisateur avec hash
    // =================================
    static void CreateUser(SqliteConnection connection, string username, string email, string password)
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
    static void DisplayUsers(SqliteConnection connection)
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
    static (string hash, string salt) HashPassword(string password)
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
}
