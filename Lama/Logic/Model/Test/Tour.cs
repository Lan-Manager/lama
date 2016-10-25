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
        public ObservableCollection<Partie> Parties { get; set; }
        public Statistiques lstStatistiques { get; set; }
        
        public Tour()
        {
            nbTour++;
            Nom = "Tour " + nbTour;
            Parties = new ObservableCollection<Partie>();
            lstStatistiques = new Statistiques();
        }
    }
}
