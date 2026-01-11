using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Data;
using Domain;

namespace UI
{
    public partial class MainWindow : Form
    {
        private readonly SqliteRepository _repo;

        private List<Utilisateur> _users = new();
        private List<Objet> _objects = new();
        private List<Echange> _exchanges = new();

        public MainWindow()
        {
            InitializeComponent();

            _repo = new SqliteRepository(Data.DbPaths.GetDbPath());

            // Remplir les combos enums (objets)
            comboObjType.DataSource = Enum.GetValues(typeof(TypeObjet));
            comboObjEtat.DataSource = Enum.GetValues(typeof(EtatObjet));

            // Filtre type
            comboTypeFilter.DataSource = Enum.GetValues(typeof(TypeObjet));

            RefreshAll();
        }

        private void RefreshAll()
        {
            _users = _repo.GetUsers();
            _objects = _repo.GetObjects();

            // Owner combo
            comboOwner.DataSource = null;
            comboOwner.DataSource = _users.ToList();
            comboOwner.DisplayMember = "Pseudo";

            // Users combos pour échange
            comboFromUser.DataSource = null;
            comboFromUser.DataSource = _users.ToList();
            comboFromUser.DisplayMember = "Pseudo";

            comboToUser.DataSource = null;
            comboToUser.DataSource = _users.ToList();
            comboToUser.DisplayMember = "Pseudo";

            // Objets dispos pour échange
            comboObjetPropose.DataSource = null;
            comboObjetPropose.DataSource = _objects.Where(o => o.Disponible).ToList();
            comboObjetPropose.DisplayMember = "Nom";

            comboObjetDemande.DataSource = null;
            comboObjetDemande.DataSource = _objects.Where(o => o.Disponible).ToList();
            comboObjetDemande.DisplayMember = "Nom";

            // Construire échanges "riches"
            RebuildExchanges();

            // Bind grids
            gridUsers.DataSource = null;
            gridUsers.DataSource = _users.Select(u => new
            {
                u.IdUtilisateur,
                u.Pseudo,
                u.Nom,
                u.Prenom,
                u.Points
            }).ToList();

            gridObjects.DataSource = null;
            gridObjects.DataSource = _objects.Select(o => new
            {
                o.IdObjet,
                o.Nom,
                o.TypeObjet,
                o.Etat,
                o.Disponible,
                o.OwnerId
            }).ToList();

            gridExchanges.DataSource = null;
            gridExchanges.DataSource = _exchanges.Select(e => new
            {
                e.IdEchange,
                Proposant = e.UtilisateurProposant.Pseudo,
                Receveur = e.UtilisateurReceveur.Pseudo,
                ObjetPropose = e.ObjetPropose.Nom,
                ObjetDemande = e.ObjetDemande?.Nom ?? "(don)",
                e.EtatEchange,
                Date = e.DateCreated
            }).ToList();
        }

        private void RebuildExchanges()
        {
            _exchanges.Clear();
            var raw = _repo.GetExchangesRaw();

            foreach (var x in raw)
            {
                var fromU = _users.FirstOrDefault(u => u.IdUtilisateur == x.fromId);
                var toU = _users.FirstOrDefault(u => u.IdUtilisateur == x.toId);
                var op = _objects.FirstOrDefault(o => o.IdObjet == x.objProposeId);
                var od = x.objDemandeId.HasValue ? _objects.FirstOrDefault(o => o.IdObjet == x.objDemandeId.Value) : null;

                if (fromU == null || toU == null || op == null) continue;

                _exchanges.Add(new Echange
                {
                    IdEchange = x.id,
                    UtilisateurProposant = fromU,
                    UtilisateurReceveur = toU,
                    ObjetPropose = op,
                    ObjetDemande = od,
                    EtatEchange = x.etat,
                    DateCreated = x.date
                });
            }
        }

        // =========================
        // EVENTS - USERS
        // =========================
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = txtNom.Text.Trim();
                string prenom = txtPrenom.Text.Trim();
                string pseudo = txtPseudo.Text.Trim();
                string email = txtEmail.Text.Trim();
                string pass = txtPassword.Text;

                if (nom == "" || prenom == "" || pseudo == "" || pass == "")
                {
                    MessageBox.Show("Veuillez remplir Nom, Prénom, Pseudo et Mot de passe.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var (hash, salt) = BackEnd.Request.HashPassword(pass);
                _repo.AddUser(nom, prenom, pseudo, email, hash, salt);

                MessageBox.Show("Utilisateur créé.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearUserInputs();
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (gridUsers.CurrentRow == null) return;

            // IMPORTANT : comme on bind un objet anonyme, on récupère via la cellule
            int id = Convert.ToInt32(gridUsers.CurrentRow.Cells["IdUtilisateur"].Value);

            var res = MessageBox.Show("Supprimer cet utilisateur ?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            _repo.DeleteUser(id);
            RefreshAll();
        }

        private void ClearUserInputs()
        {
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtPseudo.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
        }

        // =========================
        // EVENTS - OBJECTS
        // =========================
        private void btnAddObject_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboOwner.SelectedItem is not Utilisateur owner)
                {
                    MessageBox.Show("Sélectionne un propriétaire.");
                    return;
                }

                string nomObj = txtObjNom.Text.Trim();
                if (nomObj == "")
                {
                    MessageBox.Show("Nom de l'objet requis.");
                    return;
                }

                var obj = new Objet
                {
                    Nom = nomObj,
                    TypeObjet = (TypeObjet)comboObjType.SelectedItem,
                    Etat = (EtatObjet)comboObjEtat.SelectedItem,
                    Disponible = chkDisponible.Checked,
                    OwnerId = owner.IdUtilisateur
                };

                _repo.AddObject(obj);
                MessageBox.Show("Objet ajouté.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtObjNom.Text = "";
                chkDisponible.Checked = true;
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnDeleteObject_Click(object sender, EventArgs e)
        {
            if (gridObjects.CurrentRow == null) return;

            int id = Convert.ToInt32(gridObjects.CurrentRow.Cells["IdObjet"].Value);

            var res = MessageBox.Show("Supprimer cet objet ?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes) return;

            _repo.DeleteObject(id);
            RefreshAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string pseudo = txtSearchPseudo.Text.Trim();
            var type = (TypeObjet)comboTypeFilter.SelectedItem;

            var filtered = _objects
                .Where(o => o.Disponible)
                .Where(o => o.TypeObjet == type)
                .Where(o =>
                {
                    if (string.IsNullOrWhiteSpace(pseudo)) return true;
                    var owner = _users.FirstOrDefault(u => u.IdUtilisateur == o.OwnerId);
                    return owner != null && owner.Pseudo.Contains(pseudo, StringComparison.OrdinalIgnoreCase);
                })
                .Select(o => new { o.IdObjet, o.Nom, o.TypeObjet, o.Etat, o.Disponible, o.OwnerId })
                .ToList();

            gridObjects.DataSource = null;
            gridObjects.DataSource = filtered;
        }

        // =========================
        // EVENTS - EXCHANGES
        // =========================
        private void btnProposeExchange_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboFromUser.SelectedItem is not Utilisateur fromU) return;
                if (comboToUser.SelectedItem is not Utilisateur toU) return;
                if (comboObjetPropose.SelectedItem is not Objet objP) return;

                // Objet demandé : optionnel
                Objet? objD = comboObjetDemande.SelectedItem as Objet;

                if (fromU.IdUtilisateur == toU.IdUtilisateur)
                {
                    MessageBox.Show("Le receveur doit être différent du proposant.");
                    return;
                }

                _repo.AddExchange(objP.IdObjet, objD?.IdObjet, fromU.IdUtilisateur, toU.IdUtilisateur);
                MessageBox.Show("Échange proposé (en attente).", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnAcceptExchange_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridExchanges.CurrentRow == null) return;

                int id = Convert.ToInt32(gridExchanges.CurrentRow.Cells["IdEchange"].Value);
                var exchange = _exchanges.FirstOrDefault(x => x.IdEchange == id);
                if (exchange == null) return;

                if (exchange.EtatEchange != EtatEchange.EnAttente)
                {
                    MessageBox.Show("Cet échange n'est plus en attente.");
                    return;
                }

                // Appliquer bonus via délégué/lambda
                exchange.Accepter(BonusStrategies.DefaultBonus);

                // Persist DB
                _repo.UpdateExchangeState(exchange.IdEchange, EtatEchange.Accepte);

                _repo.UpdateObject(exchange.ObjetPropose);
                if (exchange.ObjetDemande != null) _repo.UpdateObject(exchange.ObjetDemande);

                _repo.UpdateUserPoints(exchange.UtilisateurProposant.IdUtilisateur, exchange.UtilisateurProposant.Points);
                _repo.UpdateUserPoints(exchange.UtilisateurReceveur.IdUtilisateur, exchange.UtilisateurReceveur.Points);

                MessageBox.Show("Échange accepté ✅", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnRefuseExchange_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridExchanges.CurrentRow == null) return;

                int id = Convert.ToInt32(gridExchanges.CurrentRow.Cells["IdEchange"].Value);
                var exchange = _exchanges.FirstOrDefault(x => x.IdEchange == id);
                if (exchange == null) return;

                exchange.Refuser();
                _repo.UpdateExchangeState(exchange.IdEchange, EtatEchange.Refuse);

                MessageBox.Show("Échange refusé.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            try
            {
                Data.JsonStorage.SaveAll("export_json", _users, _objects, _exchanges);
                MessageBox.Show("Export JSON effectué (dossier export_json).", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
    }
}
