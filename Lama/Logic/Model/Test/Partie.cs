using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Partie
    {
        public string Nom { get; set; }
        public ObservableCollection<Equipe> Equipes { get { return new ObservableCollection<Equipe> { new Equipe(1), new Equipe(2) }; } }

        public Partie(int i)
        {
            Nom = "Partie " + i;
        }
    }
}
