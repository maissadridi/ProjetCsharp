    using System;
using Tp1;
using Tp2;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Serialization 
{
    public static class Extensions
    {
        public static void afficherTous(this IEnumerable<Article> articles)
        {
            foreach (var a in articles)
            {
                Console.WriteLine(a.publishDetails());
            }
        }
    }
   

    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true, // JSON formaté
            Converters = { new JsonStringEnumConverter() } // pour sérialiser l'enum en texte
        };

        
        public static void SerialiserArticles(string chemin, List<Article> articles)
        {
            string json = JsonSerializer.Serialize(articles, options);
            File.WriteAllText(chemin, json);
            Console.WriteLine($" {articles.Count} articles exportés vers {chemin}");
        }

        // Méthode de désérialisation (chargement)
        public static List<Article> DeserialiserArticles(string chemin)
        {
            if (!File.Exists(chemin))
            {
                Console.WriteLine("Fichier introuvable : " + chemin);
                return new List<Article>();
            }

            string json = File.ReadAllText(chemin);
            var articles = JsonSerializer.Deserialize<List<Article>>(json, options);
            return articles ?? new List<Article>();
        }
    }

}