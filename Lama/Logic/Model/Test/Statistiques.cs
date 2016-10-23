using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Statistiques
    {
        public Dictionary<string, int> Stats { get; set; }
        public string showStats { get; set; }

        public Statistiques()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();
            stats.Add("Élimination(s)", 3);
            stats.Add("Mort(s)", 5);
            stats.Add("Assistance(s)", 9);
            stats.Add("Batiment(s) détruit(s)", 4);

            Stats = stats;

            StringBuilder s = new StringBuilder();

            foreach (var stat in Stats)
            {
                s.Append(stat.Value);

                if (stat.Key != Stats.Keys.Last())
                    s.Append(" / ");
            }

            showStats = s.ToString();
        }
    }
}
