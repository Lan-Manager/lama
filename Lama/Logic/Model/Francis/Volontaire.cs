using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Francis
{
    public class Volontaire
    {
        #region Propriétés
        public string Matricule { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Courriel { get; set; }
        #endregion

        #region Constructeurs
        public Volontaire(string matricule, string prenom, string nom, string courriel)
        {
            Matricule = matricule;
            Prenom = prenom;
            Nom = nom;
            Courriel = courriel;
        }
        #endregion
    }
}