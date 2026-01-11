using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Domain;

namespace Data;

public class SqliteRepository
{
    private readonly string _cs;
    public string DbFullPath { get; }
    public SqliteRepository(string dbPath)
    {
        DbFullPath = Path.GetFullPath(dbPath);
        _cs = $"Data Source={dbPath}";
    }

    private SqliteConnection Open()
    {
        var c = new SqliteConnection(_cs);
        c.Open();
        return c;
    }

    // -------- USERS --------
    public List<Utilisateur> GetUsers()
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, nom, prenom, pseudo, points FROM Users ORDER BY pseudo";

        using var r = cmd.ExecuteReader();
        var list = new List<Utilisateur>();

        while (r.Read())
        {
            list.Add(new Utilisateur
            {
                IdUtilisateur = r.GetInt32(0),
                Nom = r.GetString(1),
                Prenom = r.GetString(2),
                Pseudo = r.GetString(3),
                Points = r.GetInt32(4)
            });
        }

        // charger objets
        foreach (var u in list)
            u.ListeObjets = GetObjectsByOwner(u.IdUtilisateur);

        return list;
    }

    public int AddUser(string nom, string prenom, string pseudo, string email, string hash, string salt)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = @"
        INSERT INTO Users (nom, prenom, pseudo, email, password_hash, password_salt, points)
        VALUES ($nom, $prenom, $pseudo, $email, $hash, $salt, 0);
        SELECT last_insert_rowid();
        ";
        cmd.Parameters.AddWithValue("$nom", nom);
        cmd.Parameters.AddWithValue("$prenom", prenom);
        cmd.Parameters.AddWithValue("$pseudo", pseudo);
        cmd.Parameters.AddWithValue("$email", email);
        cmd.Parameters.AddWithValue("$hash", hash);
        cmd.Parameters.AddWithValue("$salt", salt);

        return Convert.ToInt32((long)cmd.ExecuteScalar());
    }

    public void DeleteUser(int id)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "DELETE FROM Users WHERE id=$id";
        cmd.Parameters.AddWithValue("$id", id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateUserPoints(int id, int points)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "UPDATE Users SET points=$p WHERE id=$id";
        cmd.Parameters.AddWithValue("$id", id);
        cmd.Parameters.AddWithValue("$p", points);
        cmd.ExecuteNonQuery();
    }

    // -------- OBJECTS --------
    public List<Objet> GetObjects()
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, name, type_objet, etat, disponible, owner_id FROM Objects ORDER BY id DESC";
        using var r = cmd.ExecuteReader();

        var list = new List<Objet>();
        while (r.Read())
        {
            list.Add(new Objet
            {
                IdObjet = r.GetInt32(0),
                Nom = r.GetString(1),
                TypeObjet = (TypeObjet)r.GetInt32(2),
                Etat = (EtatObjet)r.GetInt32(3),
                Disponible = r.GetInt32(4) == 1,
                OwnerId = r.GetInt32(5),
            });
        }
        return list;
    }

    public List<Objet> GetObjectsByOwner(int ownerId)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, name, type_objet, etat, disponible, owner_id FROM Objects WHERE owner_id=$o";
        cmd.Parameters.AddWithValue("$o", ownerId);

        using var r = cmd.ExecuteReader();
        var list = new List<Objet>();
        while (r.Read())
        {
            list.Add(new Objet
            {
                IdObjet = r.GetInt32(0),
                Nom = r.GetString(1),
                TypeObjet = (TypeObjet)r.GetInt32(2),
                Etat = (EtatObjet)r.GetInt32(3),
                Disponible = r.GetInt32(4) == 1,
                OwnerId = r.GetInt32(5),
            });
        }
        return list;
    }

    public int AddObject(Objet obj)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = @"
        INSERT INTO Objects (name, type_objet, etat, disponible, owner_id)
        VALUES ($n,$t,$e,$d,$o);
        SELECT last_insert_rowid();
        ";
        cmd.Parameters.AddWithValue("$n", obj.Nom);
        cmd.Parameters.AddWithValue("$t", (int)obj.TypeObjet);
        cmd.Parameters.AddWithValue("$e", (int)obj.Etat);
        cmd.Parameters.AddWithValue("$d", obj.Disponible ? 1 : 0);
        cmd.Parameters.AddWithValue("$o", obj.OwnerId);

        return Convert.ToInt32((long)cmd.ExecuteScalar());
    }

    public void UpdateObject(Objet obj)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = @"
        UPDATE Objects
        SET name=$n, type_objet=$t, etat=$e, disponible=$d, owner_id=$o
        WHERE id=$id";
        cmd.Parameters.AddWithValue("$id", obj.IdObjet);
        cmd.Parameters.AddWithValue("$n", obj.Nom);
        cmd.Parameters.AddWithValue("$t", (int)obj.TypeObjet);
        cmd.Parameters.AddWithValue("$e", (int)obj.Etat);
        cmd.Parameters.AddWithValue("$d", obj.Disponible ? 1 : 0);
        cmd.Parameters.AddWithValue("$o", obj.OwnerId);
        cmd.ExecuteNonQuery();
    }

    public void DeleteObject(int id)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "DELETE FROM Objects WHERE id=$id";
        cmd.Parameters.AddWithValue("$id", id);
        cmd.ExecuteNonQuery();
    }

    // -------- EXCHANGES --------
    public List<(int id, int objProposeId, int? objDemandeId, int fromId, int toId, EtatEchange etat, DateTime date)> GetExchangesRaw()
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, objet_propose_id, objet_demande_id, from_user_id, to_user_id, etat, date_created FROM Exchanges ORDER BY id DESC";
        using var r = cmd.ExecuteReader();

        var list = new List<(int, int, int?, int, int, EtatEchange, DateTime)>();
        while (r.Read())
        {
            int? dem = r.IsDBNull(2) ? null : r.GetInt32(2);
            list.Add((
                r.GetInt32(0),
                r.GetInt32(1),
                dem,
                r.GetInt32(3),
                r.GetInt32(4),
                (EtatEchange)r.GetInt32(5),
                DateTime.Parse(r.GetString(6))
            ));
        }
        return list;
    }

    public int AddExchange(int objProposeId, int? objDemandeId, int fromUserId, int toUserId)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = @"
        INSERT INTO Exchanges (objet_propose_id, objet_demande_id, from_user_id, to_user_id, etat, date_created)
        VALUES ($op, $od, $fu, $tu, $etat, $dt);
        SELECT last_insert_rowid();
        ";
        cmd.Parameters.AddWithValue("$op", objProposeId);
        cmd.Parameters.AddWithValue("$od", (object?)objDemandeId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("$fu", fromUserId);
        cmd.Parameters.AddWithValue("$tu", toUserId);
        cmd.Parameters.AddWithValue("$etat", (int)EtatEchange.EnAttente);
        cmd.Parameters.AddWithValue("$dt", DateTime.UtcNow.ToString("o"));

        return Convert.ToInt32((long)cmd.ExecuteScalar());
    }

    public void UpdateExchangeState(int exchangeId, EtatEchange etat)
    {
        using var c = Open();
        var cmd = c.CreateCommand();
        cmd.CommandText = "UPDATE Exchanges SET etat=$e WHERE id=$id";
        cmd.Parameters.AddWithValue("$id", exchangeId);
        cmd.Parameters.AddWithValue("$e", (int)etat);
        cmd.ExecuteNonQuery();
    }
}
