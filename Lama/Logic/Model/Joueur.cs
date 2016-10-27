using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lama.Logic.Model
{
    public class Joueur
    {
        public string Matricule { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Rang { get; set; }
        public string Usager { get; set; }
        public Equipe EquipeJoueur { get; set; }

        // Pour les testes et les stats avancées : ne pas toucher
        public BitmapImage Img { get; set; }
        public string Champion { get; set; }
        public Statistiques LstStats { get; set; }
        private static int nbJoueur = 0;
        
        public Joueur(string matricule, string prenom, string nom, string rang, string usager)
        {
            Matricule = matricule;
            Prenom = prenom;
            Nom = nom;
            Rang = rang;
            Usager = usager;

            EquipeJoueur = null;
        }

        /// <summary>
        /// Pour les testes
        /// </summary>
        public Joueur(string n)
        {
            string fullFilePath;

            Nom = n;
            LstStats = new Statistiques();

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
