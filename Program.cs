using System;
using System.Linq;
using System.Windows.Forms;
using UI;
using Data;

namespace Projet_C_sharp
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //  MODE INITIALISATION MANUELLE
            if (args.Contains("init", StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    DatabaseInitializer.Initialize();
         
                    MessageBox.Show("Base initialisée : " + Data.DbPaths.GetDbPath());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Erreur lors de l'initialisation :\n" + ex.Message,
                        "Erreur",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }

              
                return;
            }

            //  MODE NORMAL : LANCEMENT DE L'IHM
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginWindow());
        }
    }
}
