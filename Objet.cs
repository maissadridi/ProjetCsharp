using System;
using Motor;
using Tp2;

namespace Objet
{

    public enum TypeProduit
    {
        alimentaire = 0,
        droguerie = 1,
        habillement = 2,
        loisir = 3,
        tech = 4,
        industriel = 5,
        automobile = 6

    }


    public class Article : CPublication
    {
        public string designation { get; set; }
        public double prix  { get; set; }

        public int quantite { get; set; }

        public Article(string designation, double prix, int quantite)
        {
            this.designation = designation;
            this.prix = prix;
            this.quantite = quantite; 
        }

        public void Acheter()
        {
            Console.WriteLine($"{designation} a été acheté.");
        }

        public override string publishDetails()
        {
            return $"Article: {designation}, Prix: {prix:0.00} €";
        }
        
        
    }


    public class ArticleType : Article
    {
        public TypeProduit type { get; set; }

        public ArticleType(string designation, double prix, int quantite, TypeProduit type) : base(designation, prix, quantite) {
            this.type = type; 
        }
        
    }

    public class Livre : Article, IRentable
    {
        protected int isbn;
        protected int nbPages;

        // L'appel au parent se fait dans l'initialiseur de constructeur avec ": base(...)"
        public Livre(string designation, double prix, int quantite, int isbn, int nbPages)
            : base(designation, prix, quantite)
        {
            this.isbn = isbn;
            this.nbPages = nbPages;
        }

        public double calculateRent(int nbJours)
        {
            return base.prix * 0.05 * nbJours;
        }

        public override string publishDetails()
        {
            return $"Livre: {designation}, Prix: {prix:0.00} €, isbn: {isbn}";
        }

        public double discountStrategy()
        {
            double discount = this.prix switch
            {
                < 100 => 0.05,
                < 1000 => 0.10,
                < 10000 => 0.15,
                < 100000 => 0.20,
                _ => 0
            };

            double nouveauxPrix = this.prix - this.prix * discount;

            return nouveauxPrix;

        }
    }

    public class Poche : Livre
    {
        protected string categorie;

        public Poche(string designation, double prix, int quantite, int isbn, int nbPages, string categorie) : base(designation, prix, quantite, isbn, nbPages)
        {
            this.categorie = categorie;
        }
    }

    public class Broche : Livre
    {

        public Broche(string designation, double prix, int quantite, int isbn, int nbPages) : base(designation, prix, quantite, isbn, nbPages)
        {

        }
    }

    public class Disque : Article
    {
        protected string label;

        public Disque(string designation, double prix, int quantite, string label) : base(designation, prix, quantite)
        {
            this.label = label;
        }

        public void Ecouter()
        {
            Console.WriteLine("Lecture du disque...   , Label: " + this.label);
        }

        public override string publishDetails()
        {
            return $"Disque: {designation}, Prix: {prix:0.00} €, Label: {label}";
        }
    }

    public class Video : Article, IRentable
    {
        protected float duree;

        public Video(string designation, double prix, int quantite, float duree) : base(designation, prix, quantite)
        {
            this.duree = duree;
        }

        public void Afficher()
        {
            Console.WriteLine("lecture de la video...,   durée: " + this.duree);
        }


        public double calculateRent(int nbJours) {
            return base.prix * 0.10 * nbJours;
        }

          public override string publishDetails()
        {
            return $"Video: {designation}, Prix: {prix:0.00} €, duree: {duree}";
        }
    }



    struct Articlestruct
    {

        public string nom;
        public double prix;
        public int quantite;

        public Articlestruct(string nom, double prix, int quantite)
        {
            this.nom = nom;
            this.prix = prix;
            this.quantite = quantite;

        }

        public void Afficher()
        {
            Console.WriteLine($"nom: {nom}, prix: {prix}, quantité: {quantite}");
        }

        public void Ajouter(int quantiteAjouter)
        {
            quantite += quantiteAjouter;
        }

        public void Retirer(int quantiteRetiree)
        {
            quantite -= quantiteRetiree;
        }
    }

    public struct ArticleStructType
    {

        public string nom;
        public double prix;
        public int quantite;

        public TypeProduit type;

        public ArticleStructType(string nom, double prix, int quantite, TypeProduit type)
        {
            this.nom = nom;
            this.prix = prix;
            this.quantite = quantite;
            this.type = type;

        }

        public void Afficher()
        {
            Console.WriteLine($"nom: {nom}, prix: {prix}, quantité: {quantite}, type: {type}");
        }

        public void Ajouter(int quantiteAjouter)
        {
            quantite += quantiteAjouter;
        }

        public void Retirer(int quantiteRetiree)
        {
            quantite -= quantiteRetiree;
        }
    }



}
