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
        public string Nom { get; set; }
        public ObservableCollection<PartieEquipe> LstEquipes { get; set; }

        public Equipe Gagnant { get; set; }
        
        public Partie(PartieEquipe e1, PartieEquipe e2, int numPartie)
        {
            StringBuilder sb = new StringBuilder("Partie ").Append(numPartie);

            Nom = sb.ToString();
            LstEquipes = new ObservableCollection<PartieEquipe>() { e1, e2 };
        }
    }
}
