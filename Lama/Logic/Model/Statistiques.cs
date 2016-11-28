using Lama.UI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Statistiques : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TrulyObservableCollection<Statistique> _stats;
        public TrulyObservableCollection<Statistique> Stats
        {
            get
            {
                return _stats;
            }
            set
            {
                if (value != _stats)
                {
                    _stats = value;
                    NotifyPropertyChanged("Stats");
                    NotifyPropertyChanged("AfficherStats");
                }
            }
        }

        private string _afficherStats;
        public string AfficherStats {
            get
            {
                StringBuilder s = new StringBuilder();

                foreach (var stat in Stats)
                {
                    s.Append(stat.Value);

                    if (stat != Stats.Last())
                        s.Append("/");
                }

                return s.ToString();
            }
            set
            {
                if (value != _afficherStats)
                {
                    _afficherStats = value;
                    NotifyPropertyChanged("AfficherStats");
                }
            }
        }

        public Statistiques()
        {
            int nbStats = 0;

            Stats = new TrulyObservableCollection<Statistique>();
            Stats.Add(new Statistique("Élimination(s)", nbStats++));
            Stats.Add(new Statistique("Mort(s)", nbStats++));
            Stats.Add(new Statistique("Assistance(s)", nbStats++));
            Stats.Add(new Statistique("Batiment(s) détruit(s)", nbStats++));

            Stats.ItemPropertyChanged += PropertyChanged;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();

            foreach (var stat in Stats)
            {
                s.Append(stat.Value);

                if (stat != Stats.Last())
                    s.Append("/");
            }

            return s.ToString();
        }

        public static Statistiques operator +(Statistiques main, Statistiques other)
        {
            for (int i = 0; i < main.Stats.Count; i++)
            {
                main.Stats[i].Value = main.Stats[i].Value + other.Stats[i].Value;
            }

            return main;
        }


        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
    }
}
