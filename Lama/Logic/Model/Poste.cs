using LamaBD.helper;
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
        


        public enum Etats { Prêt, EnAttente, NonRequis, Problème};

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        
        public string AncienEtat { get; set; }
        private string _etat;
        public string Etat
        {
            get
            {
                return _etat;
            }
            set
            {
                if (value != _etat)
                {
                    AncienEtat = Etat;
                    _etat = value;
                    NotifyPropertyChangedExtended("Etat", AncienEtat, _etat);
                }
            }
        }
        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }

        private Volontaire _dernierModificateur;
        public Volontaire DernierModificateur
        {
            get
            {
                return _dernierModificateur;
            }
            set
            {
                if (value != _dernierModificateur)
                {
                    _dernierModificateur = value;
                    NotifyPropertyChanged("DernierModificateur");
                }
            }
        }
        public Poste(int numero, string etat)
        {
            LstEtatPossible = new ObservableCollection<string>();
            LstEtatPossible.Add("En attente");
            LstEtatPossible.Add("Problème");
            LstEtatPossible.Add("Prêt");
            LstEtatPossible.Add("Non requis");
            Numero = numero;
            Etat = etat;
        }

        protected void NotifyPropertyChangedExtended<T>(string nomProp, T ancienneValeur, T nouvelleValeur)
        {
            PropertyChanged(this, new PropertyChangedExtendedEventArgs<T>(nomProp, ancienneValeur, nouvelleValeur));
        }
    }
}
