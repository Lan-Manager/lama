using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    
    public class Local
    {
        public string Nom { get; set; }

        public ObservableCollection<Poste> LstPoste { get; set; }
       // public ObservableCollection<string> LstVolontaire{ get; set; }
        public int NbPoste { get; set; }
        public string VolontaireAssigne { get; set; }

        public Local(string nom, int nbPoste, string volontaireAssigne)
        {
            Nom = nom;
            VolontaireAssigne = volontaireAssigne;
            LstPoste = new ObservableCollection<Poste>();
        }
    }
}
