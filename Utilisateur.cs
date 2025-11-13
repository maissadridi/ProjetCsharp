

namespace Model  {
    class Utilisateur
    {
        private int id;
        private string nom;
        private string prenom;
        private string pseudo;
        private int point;

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
        private List<Object> ListeObjets = new List<Object>();
    }
    




}