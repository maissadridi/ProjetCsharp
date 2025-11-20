using System.Collections.Generic;

namespace Model
{
    public class Utilisateur
    {
        private int id;
        private string nom;
        private string prenom;
        private string pseudo;
        private int point;
        private short bonus;

        // Liste des objets possédés par l'utilisateur
        private List<object> ListeObjets = new List<object>();

        public Utilisateur(int id, string nom, string prenom, string pseudo, int point)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.pseudo = pseudo;
            this.point = point;
        }

        public int Point
        {
            get { return point; }
            set { point = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        // Exemple de méthode pour ajouter un bonus en fonction du type
        public void AddBonus(TypeBonus type)
        {
            // On ajoute la valeur du bonus convertie en int
            point += (int)type;
        }
    }

    public enum TypeBonus
    {
        neuf = 10,
        livre = 20
    }
}
