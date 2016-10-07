using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Poste
    {
        public int Numero { get; set; }
        public string Etat { get; set; }
        public string DerniereModificationPar { get; set; }
        public Poste(int numero, string etat, string derniereModificationPar)
        {
            Numero = numero;
            Etat = etat;
            DerniereModificationPar = derniereModificationPar;
        }
    }
}
