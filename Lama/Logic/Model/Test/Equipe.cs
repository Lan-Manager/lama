using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Equipe
    {
        public string Nom { get; set; }

        public Equipe(int i)
        {
            Nom = "Équipe " + i;
        }
    }
}
