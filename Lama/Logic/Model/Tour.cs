using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Tour
    {
        public string Nom { get; set; }
        public ObservableCollection<Partie> LstParties { get; set; }

        public Tour(int numTour)
        {
            StringBuilder sb = new StringBuilder("Tour ").Append(numTour);

            Nom = sb.ToString();
            LstParties = new ObservableCollection<Partie>();
        }
    }
}
