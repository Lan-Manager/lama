using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Statistique
    {
        public string Key { get; set; }
        public int? Value { get; set; }

        public Statistique() { }

        public Statistique(string key)
        {
            Key = key;
            Value = null;
        }

    }
}
