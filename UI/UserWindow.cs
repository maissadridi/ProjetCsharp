using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Data;
using Domain;

namespace UI;

public partial class UserWindow : Form
{
    private Utilisateur _current;
    private readonly SqliteRepository _repo;

    private List<Utilisateur> _users = new();
    private List<Objet> _objects = new();
    private List<Echange> _myExchanges = new();

    public UserWindow(Utilisateur current, SqliteRepository repo)
    {
        InitializeComponent();
        _current = current;
        _repo = repo;

        comboObjType.DataSource = Enum.GetValues(typeof(TypeObjet));
        comboObjEtat.DataSource = Enum.GetValues(typeof(EtatObjet));

        comboTargetUser.SelectedIndexChanged += (_, __) => RefreshTargetObjects();

        RefreshUserView();
    }

    private void chkDon_CheckedChanged(object sender, EventArgs e)
    {
        RefreshTargetObjects();
    }

    private void btnProposeExchange_Click(object sender, EventArgs e)
    {
        if (comboMyObjectPropose.SelectedItem is not Objet myObj)
        {
            MessageBox.Show("Tu dois avoir au moins un objet disponible pour proposer un échange.");
            return;
        }

        if (comboTargetUser.SelectedItem is not Utilisateur targetUser)
        {
            MessageBox.Show("Sélectionne un utilisateur.");
            return;
        }

        if (targetUser.IdUtilisateur == _current.IdUtilisateur)
        {
            MessageBox.Show("Impossible de proposer un échange à toi-même.");
            return;
        }

        int? targetObjId = null;

        if (!chkDon.Checked)
        {
            if (comboTargetObjectDemande.SelectedItem is not Objet targetObj)
            {
                MessageBox.Show("Sélectionne un objet demandé (ou coche 'Je donne').");
                return;
            }
            targetObjId = targetObj.IdObjet;
        }

        try
        {
            _repo.AddExchange(
                objProposeId: myObj.IdObjet,
                objDemandeId: targetObjId,
                fromUserId: _current.IdUtilisateur,
                toUserId: targetUser.IdUtilisateur
            );

            MessageBox.Show("Proposition envoyée ✅ (échange en attente).", "OK");
            RefreshUserView();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur : " + ex.Message);
        }
    }

    private void RefreshUserView()
    {
        _users = _repo.GetUsers();
        _objects = _repo.GetObjects();

        var refreshed = _users.FirstOrDefault(u => u.IdUtilisateur == _current.IdUtilisateur);
        if (refreshed != null) _current = refreshed;

        lblPseudo.Text = $"Pseudo : {_current.Pseudo}";
        lblNomPrenom.Text = $"Nom : {_current.Nom} {_current.Prenom}";
        lblPoints.Text = $"Points : {_current.Points}";

        var myObjects = _objects.Where(o => o.OwnerId == _current.IdUtilisateur).ToList();
        gridMyObjects.DataSource = null;
        gridMyObjects.DataSource = myObjects.Select(o => new
        {
            o.IdObjet,
            o.Nom,
            o.TypeObjet,
            o.Etat,
            o.Disponible
        }).ToList();

        BuildMyExchanges();
        gridMyExchanges.DataSource = null;
        gridMyExchanges.DataSource = _myExchanges.Select(e => new
        {
            e.IdEchange,
            Proposant = e.UtilisateurProposant.Pseudo,
            Receveur = e.UtilisateurReceveur.Pseudo,
            ObjetPropose = e.ObjetPropose.Nom,
            ObjetDemande = e.ObjetDemande?.Nom ?? "(don)",
            e.EtatEchange,
            Date = e.DateCreated
        }).ToList();

        RefreshProposeSection();
    }

    private void BuildMyExchanges()
    {
        _myExchanges.Clear();

        var raw = _repo.GetExchangesRaw();
        foreach (var x in raw)
        {
            if (x.fromId != _current.IdUtilisateur && x.toId != _current.IdUtilisateur)
                continue;

            var fromU = _users.FirstOrDefault(u => u.IdUtilisateur == x.fromId);
            var toU = _users.FirstOrDefault(u => u.IdUtilisateur == x.toId);
            var op = _objects.FirstOrDefault(o => o.IdObjet == x.objProposeId);
            var od = x.objDemandeId.HasValue ? _objects.FirstOrDefault(o => o.IdObjet == x.objDemandeId.Value) : null;

            if (fromU == null || toU == null || op == null)
                continue;

            _myExchanges.Add(new Echange
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

    private void btnAddObject_Click(object sender, EventArgs e)
    {
        string name = txtObjNom.Text.Trim();
        if (name == "")
        {
            MessageBox.Show("Nom d'objet requis.");
            return;
        }

        var obj = new Objet
        {
            Nom = name,
            TypeObjet = (TypeObjet)comboObjType.SelectedItem,
            Etat = (EtatObjet)comboObjEtat.SelectedItem,
            Disponible = chkDisponible.Checked,
            OwnerId = _current.IdUtilisateur
        };

        var newId = _repo.AddObject(obj);
        obj.IdObjet = newId;

        MessageBox.Show("Objet ajouté.", "OK");
        RefreshUserView();
    }

    private void btnDeleteObject_Click(object sender, EventArgs e)
    {
        if (gridMyObjects.CurrentRow == null) return;

        int id = (int)gridMyObjects.CurrentRow.Cells["IdObjet"].Value;
        _repo.DeleteObject(id);

        MessageBox.Show("Objet supprimé.", "OK");
        RefreshUserView();
    }

    private void btnToggleDisponibilite_Click(object sender, EventArgs e)
    {
        if (gridMyObjects.CurrentRow == null) return;

        int id = (int)gridMyObjects.CurrentRow.Cells["IdObjet"].Value;
        var obj = _objects.FirstOrDefault(o => o.IdObjet == id);
        if (obj == null) return;

        if (obj.OwnerId != _current.IdUtilisateur) return;

        obj.Disponible = !obj.Disponible;
        _repo.UpdateObject(obj);

        RefreshUserView();
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
        if (gridMyExchanges.CurrentRow == null) return;

        int id = (int)gridMyExchanges.CurrentRow.Cells["IdEchange"].Value;
        var ex = _myExchanges.FirstOrDefault(x => x.IdEchange == id);
        if (ex == null) return;

        if (ex.UtilisateurReceveur.IdUtilisateur != _current.IdUtilisateur)
        {
            MessageBox.Show("Tu peux accepter/refuser uniquement les échanges où tu es receveur.");
            return;
        }

        if (ex.EtatEchange != EtatEchange.EnAttente)
        {
            MessageBox.Show("Échange non en attente.");
            return;
        }

        ex.Accepter(BonusStrategies.DefaultBonus);

        _repo.UpdateExchangeState(ex.IdEchange, EtatEchange.Accepte);

        _repo.UpdateObject(ex.ObjetPropose);
        if (ex.ObjetDemande != null) _repo.UpdateObject(ex.ObjetDemande);

        _repo.UpdateUserPoints(ex.UtilisateurProposant.IdUtilisateur, ex.UtilisateurProposant.Points);
        _repo.UpdateUserPoints(ex.UtilisateurReceveur.IdUtilisateur, ex.UtilisateurReceveur.Points);

        MessageBox.Show("Échange accepté ✅", "OK");
        RefreshUserView();
    }

    private void btnRefuse_Click(object sender, EventArgs e)
    {
        if (gridMyExchanges.CurrentRow == null) return;

        int id = (int)gridMyExchanges.CurrentRow.Cells["IdEchange"].Value;
        var ex = _myExchanges.FirstOrDefault(x => x.IdEchange == id);
        if (ex == null) return;

        if (ex.UtilisateurReceveur.IdUtilisateur != _current.IdUtilisateur)
        {
            MessageBox.Show("Tu peux refuser uniquement les échanges où tu es receveur.");
            return;
        }

        if (ex.EtatEchange != EtatEchange.EnAttente)
        {
            MessageBox.Show("Échange non en attente.");
            return;
        }

        ex.Refuser();
        _repo.UpdateExchangeState(ex.IdEchange, EtatEchange.Refuse);

        MessageBox.Show("Échange refusé.", "OK");
        RefreshUserView();
    }

    private void RefreshProposeSection()
    {
        var myAvailable = _objects
            .Where(o => o.OwnerId == _current.IdUtilisateur && o.Disponible)
            .OrderBy(o => o.Nom)
            .ToList();

        comboMyObjectPropose.DataSource = null;
        comboMyObjectPropose.DataSource = myAvailable;
        comboMyObjectPropose.DisplayMember = "Nom";

        var others = _users
            .Where(u => u.IdUtilisateur != _current.IdUtilisateur)
            .OrderBy(u => u.Pseudo)
            .ToList();

        comboTargetUser.DataSource = null;
        comboTargetUser.DataSource = others;
        comboTargetUser.DisplayMember = "Pseudo";

        comboTargetObjectDemande.Enabled = !chkDon.Checked;

        RefreshTargetObjects();
    }

    private void RefreshTargetObjects()
    {
        if (chkDon.Checked)
        {
            comboTargetObjectDemande.DataSource = null;
            comboTargetObjectDemande.Enabled = false;
            return;
        }

        comboTargetObjectDemande.Enabled = true;

        if (comboTargetUser.SelectedItem is not Utilisateur target)
        {
            comboTargetObjectDemande.DataSource = null;
            return;
        }

        var targetAvailable = _objects
            .Where(o => o.OwnerId == target.IdUtilisateur && o.Disponible)
            .OrderBy(o => o.Nom)
            .ToList();

        comboTargetObjectDemande.DataSource = null;
        comboTargetObjectDemande.DataSource = targetAvailable;
        comboTargetObjectDemande.DisplayMember = "Nom";
    }
}
