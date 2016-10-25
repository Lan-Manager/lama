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
        public ObservableCollection<Statistique> Stats { get; set; }

        public string showStats { get; set; }

        public Statistiques()
        {
            ObservableCollection<Statistique> stats = new ObservableCollection<Statistique>();
            stats.Add(new Statistique("Élimination(s)", 0));
            stats.Add(new Statistique("Mort(s)", 0));
            stats.Add(new Statistique("Assistance(s)", 0));
            stats.Add(new Statistique("Batiment(s) détruit(s)", 0));

            Stats = stats;

            StringBuilder s = new StringBuilder();

            foreach (Statistique stat in Stats)
            {
                s.Append(stat.Value);

                if (stat.Key != Stats.Last().Key)
                    s.Append(" / ");
            }

            showStats = s.ToString();
        }
    }
}
