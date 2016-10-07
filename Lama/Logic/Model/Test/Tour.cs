using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Tour
    {
        private static int nbTour = 0;

        public string Nom { get; set; }
        public ObservableCollection<Partie> Parties { get { return new ObservableCollection<Partie>() { new Partie(1), new Partie(2), new Partie(3) }; } }

        public Tour()
        {
            nbTour++;
            Nom = "Tour " + nbTour;
        }
    }
}
