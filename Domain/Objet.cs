namespace Domain;

public class Objet : ArticleType
{
    public int IdObjet { get; set; }
    public EtatObjet Etat { get; set; }
    public bool Disponible { get; set; } = true;

    public int OwnerId { get; set; }
}
