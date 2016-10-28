using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class PartieEquipe : INotifyPropertyChanged
    {
        public Equipe Equipe { get; set; }
        private Statistiques _lstStats;
        public Statistiques LstStats
        {
            get
            {
                return _lstStats;
            }

            set
            {
                if (value != _lstStats)
                {
                    _lstStats = value;
                    NotifyPropertyChanged("LstStats");
                }
            }
        }

        private bool? _estGagnant;
        public bool? EstGagnant
        {
            get
            {
                return _estGagnant;
            }

            set
            {
                if (value != _estGagnant)
                {
                    _estGagnant = value;
                    NotifyPropertyChanged("EstGagnant");
                }
            }
        }

        public PartieEquipe(Equipe e)
        {
            Equipe = e;
            LstStats = new Statistiques();
            LstStats.PropertyChanged += PropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
    }
}
