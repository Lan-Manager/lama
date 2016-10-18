using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Poste : INotifyPropertyChanged
    {
        public int Numero { get; set; }
        public ObservableCollection<string> LstEtatPossible { get; set; }
        private string _etat;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Etat
        {
            get
            {
                return _etat;
            }
            set
            {
                if (value == _etat)
                {
                    return;
                }
                _etat = value;
                NotifyPropertyChanged("Etat");
            }
        }
        protected void NotifyPropertyChanged(string nomProp)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nomProp));
            }

        }

        public Volontaire DerniereModificationPar { get; set; }
        public Poste(int numero, string etat)
        {
            LstEtatPossible = new ObservableCollection<string>();
            LstEtatPossible.Add("En attente");
            LstEtatPossible.Add("Problème");
            LstEtatPossible.Add("Prêt");
            Numero = numero;
            Etat = etat;
        }



    }
}
