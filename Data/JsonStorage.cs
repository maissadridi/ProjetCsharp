using System.Text.Json;
using Domain;

namespace Data;

public static class JsonStorage
{
    private static readonly JsonSerializerOptions Opt = new() { WriteIndented = true };

    public static void SaveAll(string folder, List<Utilisateur> users, List<Objet> objects, List<Echange> exchanges)
    {
        Directory.CreateDirectory(folder);
        File.WriteAllText(Path.Combine(folder, "users.json"), JsonSerializer.Serialize(users, Opt));
        File.WriteAllText(Path.Combine(folder, "objects.json"), JsonSerializer.Serialize(objects, Opt));
        File.WriteAllText(Path.Combine(folder, "exchanges.json"), JsonSerializer.Serialize(exchanges, Opt));
    }
}
