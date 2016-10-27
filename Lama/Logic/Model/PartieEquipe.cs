using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class PartieEquipe
    {
        public Equipe Equipe { get; set; }
        public Statistiques LstStats { get; set; }
        public bool? EstGagnant { get; set; }

        public PartieEquipe(Equipe e)
        {
            Equipe = e;
            LstStats = new Statistiques();
        }
    }
}
