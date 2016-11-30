using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Prix
    {
        #region Propriétés
        public Equipe Gagnant { get; set; }
        private string _nom;
        public string Nom
        {
            get
            {
                return _nom;
            }

            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _nom = value;
                }
            }
        }
        #endregion

        #region Constructeurs
        public Prix(string nom)
        {
            Nom = nom;
        }

        public Prix(string nom, string nomEquipeGagnante)
            :this(nom)
        {
            Gagnant = new Equipe(nomEquipeGagnante);
        }
        #endregion
    }
}
