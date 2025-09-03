using System;
using System.Collections.Generic;
using Proj;

namespace Proj{
    


class Catalogue
{
    private List<Utilisateur> list_utilisateur;

    public Catalogue(List<Utilisateur> list_utilisateur)
    {
        this.list_utilisateur = list_utilisateur;
    }

    // Indexeur : permet d'accéder aux articles par indice
	public void ajouter (Utilisateur user)
	{
		list_utilisateur.Add(user);
	}
	public void afficher()
	{	
		Console.WriteLine("\n");
		
		foreach (Utilisateur user in list_utilisateur)
		{
			Console.WriteLine(user.Nom);
		}
	}
    public Utilisateur this[int index]
    {
        get
        {
            return list_utilisateur[index];
        }
        set
        {

            list_utilisateur[index] = value;
        }
    }

}

	 enum TypeEtat
    {
        Alimentaire,
        Droguerie,
        Habillement,
        Loisir,
        notype
    };

    class Object
    {
        private string nom;
        private bool disponible;
        private TypeEtat etat;
        private string TypeObjet;

        public Object(string nom, bool disponible, TypeEtat etat, string TypeObjet)
        {
            this.nom = nom;
            this.disponible = disponible;
            this.etat = etat;
            this.TypeObjet = TypeObjet;

        }
		
  
        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }

    };


	 enum TypeEtatEchange
    {
        EnAttente, Accepte, Refuse
        };


    class Echange
    {
        private int IdEchange;
        private Utilisateur UtilisateurProposant;
        private Utilisateur UtilisateurReceveur;
        private Object ObjetPropose;
        private TypeEtatEchange EtatEchange;

        public Echange(int id, Utilisateur UtilisateurProposant, Utilisateur UtilisateurReceveur, Object ObjetPropose, TypeEtatEchange EtatEchange)
        {
            this.IdEchange = id;
            this.UtilisateurProposant = UtilisateurProposant;
            this.UtilisateurReceveur = UtilisateurReceveur;
            this.ObjetPropose = ObjetPropose;
            this.EtatEchange = EtatEchange;
        }

        public void Accepter()
        {
            this.EtatEchange = TypeEtatEchange.Accepte;
            this.ObjetPropose.Disponible = false;
            this.UtilisateurProposant.Point += 1;
            this.UtilisateurProposant.Point += 1;
        }


        public void Refuser()
        {
            this.EtatEchange = TypeEtatEchange.Refuse;
        }

    }
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
	


 public class MonApp
    {
		
		
        // Fonction principale :
        public static void Main()
        {
            // Équivalent de System.Console.WriteLine :
            Console.WriteLine("Hello World!");
			
			Catalogue List_Utilisateur = new Catalogue(new List<Utilisateur> {});
			Utilisateur baptiste= new Utilisateur(0,"baratgin", "baptiste"," bptst",0);
			Utilisateur az= new Utilisateur(0,"zae", "fds"," dsf",0);

			List_Utilisateur.ajouter(baptiste);
			List_Utilisateur.ajouter(az);

			List_Utilisateur.afficher();
			List_Utilisateur.supprimer(az);
			List_Utilisateur.afficher();


			
			
			
		
		}
 }

}

