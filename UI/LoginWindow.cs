using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Data;
using Domain;

namespace UI;

public partial class LoginWindow : Form
{
    private readonly SqliteRepository _repo;
    private readonly string _dbPath = Data.DbPaths.GetDbPath();

    public LoginWindow()
    {
        InitializeComponent();
        _repo = new SqliteRepository(_dbPath);
        MessageBox.Show("LoginWindow ouvre :\n" + Data.DbPaths.GetDbPath());

    }

    // -------------------------
    // Connexion : pseudo uniquement
    // -------------------------
    private void btnLogin_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        string pseudo = txtPseudo.Text.Trim();
        if (pseudo == "")
        {
            lblError.Text = "Veuillez saisir un pseudo.";
            return;
        }

        try
        {
            var users = _repo.GetUsers();
            var current = users.FirstOrDefault(u => u.Pseudo.Equals(pseudo, StringComparison.OrdinalIgnoreCase));

            if (current == null)
            {
                lblError.Text = "Pseudo introuvable. Crée un compte juste en dessous.";
                return;
            }

            OpenUserWindow(current);
        }
        catch (Exception ex)
        {
            lblError.Text = "Erreur : " + ex.Message;
        }
    }

    // -------------------------
    // Création compte (sans mdp)
    // -------------------------
    private void btnCreate_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        string nom = txtNom.Text.Trim();
        string prenom = txtPrenom.Text.Trim();
        string pseudo = txtPseudoNew.Text.Trim();
        string email = txtEmail.Text.Trim();

        if (nom == "" || prenom == "" || pseudo == "")
        {
            lblError.Text = "Nom, prénom et pseudo sont obligatoires.";
            return;
        }

        try
        {
            // Comme la table a password_hash/salt NOT NULL, on met des valeurs “bidon”.
            // (On ne les utilise plus)
            string dummyHash = Convert.ToBase64String(new byte[32]); // 32 bytes -> base64
            string dummySalt = Convert.ToBase64String(new byte[16]);

            int newId = _repo.AddUser(nom, prenom, pseudo, email, dummyHash, dummySalt);

            // Récupérer l'utilisateur créé via repo (avec ListeObjets etc.)
            var users = _repo.GetUsers();
            var current = users.First(u => u.IdUtilisateur == newId);

            MessageBox.Show("Compte créé ✅", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OpenUserWindow(current);
        }
        catch (SqliteException sqlEx)
        {
            // Pseudo unique -> erreur possible
            lblError.Text = "Erreur SQLite : " + sqlEx.Message;
        }
        catch (Exception ex)
        {
            lblError.Text = "Erreur : " + ex.Message;
        }
    }

    private void OpenUserWindow(Utilisateur current)
    {
        var win = new UserWindow(current, _repo);
        win.FormClosed += (_, __) => this.Show();
        this.Hide();
        win.Show();
    }
}
