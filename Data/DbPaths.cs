using System;
using System.IO;

namespace Data
{
    public static class DbPaths
    {
        public static string GetDbPath()
        {
            // bin\Debug\net8.0-windows\ → remonter à la racine du projet
            string projectDir = Path.GetFullPath(
                Path.Combine(AppContext.BaseDirectory, @"..\..\..\")
            );

            return Path.Combine(projectDir, "base.db");
        }
    }
}
