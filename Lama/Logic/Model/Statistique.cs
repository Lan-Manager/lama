using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Statistique
    {
        public string Key { get; set; }
        public int Value { get; set; }

        public Statistique()
        {
            Key = "n/a";
            Value = -1;
        }

        public Statistique(string key, int value)
        {
            Key = key;
            Value = value;
        }

    }
}
