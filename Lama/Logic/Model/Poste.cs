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
        private string _commentaire;
        public string Commentaire
        {
            get
            {
                return _commentaire;
            }
            set
            {
                if (value != _commentaire)
                {
                    _commentaire = value;
                    NotifyPropertyChanged(Commentaire);
                }
            }
        }

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
        /// <summary>
        /// Constructeur d'un poste.
        /// </summary>
        /// <param name="numero">Le numéro du poste.</param>
        /// <param name="etat">Son état initial au lancement de l'application.</param>
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

        #region Interface INotifyPropertyChanged
        /// <summary>
        /// Handler 
        /// </summary>
        /// <param name="nomProp">Nom de la propriété qui a changé.</param>
        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        /// <summary>
        /// Cette fonction est appelé par les setter pour l'état des postes, elle avertie qu'un changement c'est produit.
        /// On garde une trace de l'ancienne valeur de la propriété pour optimiser certains calculs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nomProp">Le nom de la propriété qui a changée</param>
        /// <param name="ancienneValeur">L'ancienne valeur de la propriété.</param>
        /// <param name="nouvelleValeur">La nouvelle valeur de la propriété</param>
        protected void NotifyPropertyChangedExtended<T>(string nomProp, T ancienneValeur, T nouvelleValeur)
        {
            PropertyChanged(this, new PropertyChangedExtendedEventArgs<T>(nomProp, ancienneValeur, nouvelleValeur));
        }
        #endregion
    }
}
