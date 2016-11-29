using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Classement
    {
        public int Position { get; set; }
        public string Nom { get; set; }
        public int? Value { get; set; }

        public Classement(int p, string n, int? v)
        {
            Position = p;
            Nom = n;
            Value = v;
        }
    }
}
