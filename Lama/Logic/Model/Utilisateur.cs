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
        public bool EstAdmin { get; set; }
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
    
        /// <summary>
        /// Constructeur d'utilisateur avec les valeurs qui doivent être chargés par défaut.
        /// </summary>
        public Utilisateur()
        {
            EstConnecte = false;
            EstAdmin = false;
        }

        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        
    }
}
