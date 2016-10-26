using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Equipe
    {
        static int nbEquipe = 0;
        public string Nom { get; set; }
        public Statistiques lstStatistiques { get; set; }
        public ObservableCollection<Joueur> Joueurs { get; set; }
        public bool EstElimine { get; set; }
        public bool? EstGagnant { get; set; }

        public Equipe()
        {
            nbEquipe++;

            Nom = "Équipe " + nbEquipe;
            lstStatistiques = new Statistiques();
        }
        public Equipe(ObservableCollection<Joueur> e1)
        {
            nbEquipe++;

            Nom = "Équipe " + nbEquipe;
            lstStatistiques = new Statistiques();
            Joueurs = e1;
        }

        public bool Equals(Equipe other)
        {
            return other.Nom == this.Nom; 
        }
    }
}
