using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Equipe
    {
        static int nbEquipe = 0;
        public string Nom { get; set; }

        public Equipe()
        {
            nbEquipe++;
            Nom = "Équipe " + nbEquipe;
        }
    }
}
