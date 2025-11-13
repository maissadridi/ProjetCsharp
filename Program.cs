using System;
using System.Collections.Generic;
using Model; 

namespace projet
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // Équivalent de System.Console.WriteLine :
            Console.WriteLine("Hello World!");

            Catalogue List_Utilisateur = new Catalogue(new List<Utilisateur> { });
            Utilisateur baptiste = new Utilisateur(0, "baratgin", "baptiste", " bptst", 0);
            Utilisateur az = new Utilisateur(0, "zae", "fds", " dsf", 0);

            List_Utilisateur.ajouter(baptiste);
            List_Utilisateur.ajouter(az);

            List_Utilisateur.afficher();
            List_Utilisateur.supprimer(az);
            List_Utilisateur.afficher();



            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}