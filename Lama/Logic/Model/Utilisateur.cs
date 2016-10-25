using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Utilisateur: INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _estConnecte;
        public bool EstConnecte
        {
            get
            {
                return _estConnecte;
            }
            set
            {
                if (_estConnecte != value)
                {
                    _estConnecte = value;
                    NotifyPropertyChanged("EstConnecte");
                }
            }
        }
        private string _nomComplet;
        public string NomComplet
        {
            get
            {
                return _nomComplet;
            }
            set
            {
                if (_nomComplet != value)
                {
                    _nomComplet = value;
                    NotifyPropertyChanged("NomComplet");
                }
            }
        }
        public Utilisateur()
        {
            NomComplet = "Invité";
            EstConnecte = false;
        }

        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        public void Connexion()
        {

        }
    }
}
