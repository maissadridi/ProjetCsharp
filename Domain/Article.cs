namespace Domain;

public interface IRentable
{
    decimal PrixLocationParJour { get; }
}

public class Article
{
    public string Nom { get; set; } = "";
}

public class ArticleType : Article
{
    public TypeObjet TypeObjet { get; set; }
}
