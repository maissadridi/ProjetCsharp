namespace Domain;

public class Utilisateur
{
    public int IdUtilisateur { get; set; }
    public string Nom { get; set; } = "";
    public string Prenom { get; set; } = "";
    public string Pseudo { get; set; } = "";
    public int Points { get; set; }

    public List<Objet> ListeObjets { get; set; } = new();
}
