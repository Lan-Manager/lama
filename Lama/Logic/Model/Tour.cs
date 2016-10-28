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
        private static int nbTour = 0;

        public string Nom { get; set; }
        public ObservableCollection<Partie> LstParties { get; set; }

        public Tour()
        {
            nbTour++;
            Nom = "Tour " + nbTour;
            LstParties = new ObservableCollection<Partie>();
        }
    }
}
