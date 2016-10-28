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
        // Propriété du nom complet de l'utilisateur.
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
        /// <summary>
        /// Constructeur d'utilisateur avec les valeurs qui doivent être chargés par défaut.
        /// </summary>
        public Utilisateur()
        {
            NomComplet = "Invité";
            EstConnecte = false;
            EstAdmin = false;
        }

        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        
    }
}
