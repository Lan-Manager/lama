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

        public Joueur(string matricule, string prenom, string nom, string rang, string usager)
        {
            Matricule = matricule;
            Prenom = prenom;
            Nom = nom;
            Rang = rang;
            Usager = usager;

            EquipeJoueur = null;
        }
    }
}
