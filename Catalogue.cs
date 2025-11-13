using System;
using System.Collections.Generic;


namespace Model { 
    class Catalogue
        {
            private List<Utilisateur> list_utilisateur;

            public Catalogue(List<Utilisateur> list_utilisateur)
            {
                this.list_utilisateur = list_utilisateur;
            }

            // Indexeur : permet d'accéder aux articles par indice
            public void ajouter(Utilisateur user)
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

}