namespace Domain;

public delegate int BonusStrategy(Objet obj, int pointsBase);

public class Echange
{
    public int IdEchange { get; set; }

    public Utilisateur UtilisateurProposant { get; set; } = new();
    public Utilisateur UtilisateurReceveur { get; set; } = new();

    public Objet ObjetPropose { get; set; } = new();
    public Objet? ObjetDemande { get; set; }

    public EtatEchange EtatEchange { get; set; } = EtatEchange.EnAttente;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public void Accepter(BonusStrategy bonus)
    {
        if (EtatEchange != EtatEchange.EnAttente) return;

        // Transfert de propriété
        ObjetPropose.OwnerId = UtilisateurReceveur.IdUtilisateur;

        if (ObjetDemande != null)
            ObjetDemande.OwnerId = UtilisateurProposant.IdUtilisateur;

        // Disponibilité après échange (choix conseillé)
        ObjetPropose.Disponible = true;
        if (ObjetDemande != null) ObjetDemande.Disponible = true;

        // Points + bonus
        int basePts = 10;
        int ptsPropose = bonus(ObjetPropose, basePts);
        int ptsDemande = (ObjetDemande != null) ? bonus(ObjetDemande, basePts) : basePts;

        UtilisateurProposant.Points += ptsPropose;
        UtilisateurReceveur.Points += ptsDemande;

        EtatEchange = EtatEchange.Accepte;
    }



    public void Refuser()
    {
        if (EtatEchange != EtatEchange.EnAttente) return;
        EtatEchange = EtatEchange.Refuse;
    }
}
