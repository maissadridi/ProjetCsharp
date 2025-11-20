using System;
using System.Collections.Generic;

using Model;


namespace Motor
{
    class Echange
    {
        private int IdEchange;
        private Utilisateur UtilisateurProposant;
        private Utilisateur UtilisateurReceveur;
        private ObjetBis ObjetPropose;
        private TypeEtatEchange EtatEchange;

        public Echange(int id, Utilisateur UtilisateurProposant, Utilisateur UtilisateurReceveur, ObjetBis ObjetPropose, TypeEtatEchange EtatEchange)
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

    enum TypeEtatEchange
    {
        EnAttente, Accepte, Refuse
    };

}