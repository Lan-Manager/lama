using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lama.Logic.Model.Test
{
    public class Joueur
    {
        private static int nbJoueur = 0;
        public string Nom { get; set; }
        public Statistiques lstStatistiques { get; set; }
        public BitmapImage Img { get; set; }
        public string Champion { get; set; }

        public Joueur(string n)
        {
            string fullFilePath;

            Nom = n;
            lstStatistiques = new Statistiques();

            switch (nbJoueur)
            {
                case 0:
                    Champion = "Annie";
                    break;
                case 1:
                    Champion = "Jax";
                    break;
                case 2:
                    Champion = "Blitzcrank";
                    break;
                case 3:
                    Champion = "Singed";
                    break;
                case 4:
                    Champion = "Alistar";
                    break;
                case 5:
                    Champion = "Pantheon";
                    break;
                case 6:
                    Champion = "Sivir";
                    break;
                case 7:
                    Champion = "Olaf";
                    break;
                case 8:
                    Champion = "Talon";
                    break;
                default:
                    Champion = "Taric";
                    break;
            }

            fullFilePath = @"http://ddragon.leagueoflegends.com/cdn/5.2.1/img/champion/" + Champion + ".png";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            Img = bitmap;

            nbJoueur++;
        }
    }
}
