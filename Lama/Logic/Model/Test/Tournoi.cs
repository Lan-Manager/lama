using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Tournoi
    {
        public string Nom { get; set; }
        public string Etat { get { return "Tournoi en cours"; } }
        public Equipe Vainqueur { get { return new Equipe(); } set { } }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Prix> lstPrix { get; set; }
        public Statistiques lstStatistiques { get; set; }

        public Tournoi(string nom)
        {
            Nom = nom;
            Tours = new ObservableCollection<Tour> { new Tour(), new Tour(), new Tour(), new Tour() };
            lstPrix = new ObservableCollection<Prix> { new Prix("25$ carte cadeau à la cafétéria"), new Prix() };
            lstStatistiques = new Statistiques();
        }       
    }
}
