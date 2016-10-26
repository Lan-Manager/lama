using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Equipe
    {
        #region Propriétés
        public string Nom { get; set; }
        public Statistiques lstStatistiques { get; set; }
        public ObservableCollection<Joueur> Joueurs { get; set; }
        public bool EstElimine { get; set; }
        #endregion

        #region Constructeurs
        public Equipe(string nom)
        {
            Nom = nom;

            Joueurs = new ObservableCollection<Joueur>();

            EstElimine = false;
        }
        #endregion
    }
}
