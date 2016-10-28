using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Partie
    {
        private static int nbPartie = 0;

        public string Nom { get; set; }
        public ObservableCollection<PartieEquipe> LstEquipes { get; set; }

        public Equipe Gagnant { get; set; }
        
        public Partie(PartieEquipe e1, PartieEquipe e2)
        {
            nbPartie++;
            Nom = "Partie " + nbPartie;
            LstEquipes = new ObservableCollection<PartieEquipe>() { e1, e2 };
        }
    }
}
