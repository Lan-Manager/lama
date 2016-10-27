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
