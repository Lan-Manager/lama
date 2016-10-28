using Lama.UI.Model;
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
        public TrulyObservableCollection<Joueur> LstJoueurs { get; set; }
        public bool EstElimine { get; set; }
        public Statistiques LstStats { get; set; }
        #endregion

        #region Constructeurs
        public Equipe(string nom)
        {
            Nom = nom;

            LstJoueurs = new TrulyObservableCollection<Joueur>();

            EstElimine = false;
            LstStats = new Statistiques();

            // TODO: SELEUMENT POUR MONTRER EN DÉMO
            LstJoueurs.Add(new Joueur("123123", "12312", "123123", "123", "maxeber"));
            LstJoueurs.Add(new Joueur("123123", "12312", "123123", "123", "savariiia"));
            LstJoueurs.Add(new Joueur("123123", "12312", "123123", "123", "antonee"));
            LstJoueurs.Add(new Joueur("123123", "12312", "123123", "123", "franciska"));
            LstJoueurs.Add(new Joueur("123123", "12312", "123123", "123", "l'autre dude3"));
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
