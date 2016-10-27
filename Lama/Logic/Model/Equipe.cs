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
        public ObservableCollection<Joueur> Joueurs { get; set; }
        public bool EstElimine { get; set; }

        // TEST
        static int nbEquipe = 0;
        public Statistiques LstStats { get; set; }
        #endregion

        #region Constructeurs
        public Equipe(string nom)
        {
            Nom = nom;

            Joueurs = new ObservableCollection<Joueur>();

            EstElimine = false;
        }

        public Equipe(ObservableCollection<Joueur> e1)
        {
            nbEquipe++;

            Nom = "Équipe " + nbEquipe;
            Joueurs = e1;
            EstElimine = false;
            LstStats = new Statistiques();
        }
        #endregion

        #region Methodes
        public bool Equals(Equipe other)
        {
            return other.Nom == this.Nom;
        }
        #endregion
    }
}
