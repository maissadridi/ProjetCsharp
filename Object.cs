using System;
using System.Collections.Generic;


namespace Model {
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

        }


    enum TypeEtat
    {
        Alimentaire,
        Droguerie,
        Habillement,
        Loisir,
        notype
    };

}