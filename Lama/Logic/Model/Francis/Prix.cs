using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Francis
{
    public class Prix
    {
        #region Propriétés
        public string Nom { get; set; }
        #endregion

        #region Constructeurs
        public Prix(string nom)
        {
            Nom = nom;
        }
        #endregion
    }
}
