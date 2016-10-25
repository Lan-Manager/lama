using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Administrateur : Volontaire
    {
        // Constructeur par défaut.
        public Administrateur(string nom, string prenom, string courriel, string nomUtilisateur) : base(nom, prenom, courriel, nomUtilisateur)
        {
            EstAdmin = true;
        }
    }
}
