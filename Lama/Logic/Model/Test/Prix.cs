using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Prix
    {
        public string Nom { get; set; }

        public Prix()
        {
            Nom = "...Prix test...";
        }

        public Prix(string n)
        {
            Nom = n;
        }
    }
}
