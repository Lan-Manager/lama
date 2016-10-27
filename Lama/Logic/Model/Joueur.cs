using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lama.Logic.Model
{
    public class Joueur : INotifyPropertyChanged
    {
        public string Matricule { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Rang { get; set; }
        public string Usager { get; set; }
        public Equipe EquipeJoueur { get; set; }

        private Statistiques _lstStats;
        public Statistiques LstStats
        {
            get { return _lstStats; }
            set
            {
                if (value != _lstStats)
                {
                    _lstStats = value;
                    NotifyPropertyChanged("LstStats");
                }
            }
        }

        // TODO : Class association Partie-Joueur-Champion
        public BitmapImage Img { get; set; }
        public string Champion { get; set; }

        public Joueur(string matricule, string prenom, string nom, string rang, string usager)
        {
            Matricule = matricule;
            Prenom = prenom;
            Nom = nom;
            Rang = rang;
            Usager = usager;

            EquipeJoueur = null;

            // TEST DONNÉES STATIQUE
            // TODO : Class association Partie-Joueur-Champion

            string fullFilePath;

            Champion = "Alistar";

            fullFilePath = @"http://ddragon.leagueoflegends.com/cdn/5.2.1/img/champion/" + Champion + ".png";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();

            Img = bitmap;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
    }
}
