using Lama.UI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows;

namespace Lama.Logic.Model
{

    public class Local : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom { get; set; }

        public TrulyObservableCollection<Poste> LstPoste { get; set; }


        #region Propriétés concernant le nombre de postes.

        public int NbPoste_Depart { get; set; } // Le nombre de poste ayant été prévu lors de la création du tournoi.

        private int _nbPoste;
        public int NbPoste // Le nombre de poste présentement utilisé pour le tournoi.
        {
            get
            {
                return _nbPoste;
            }
            set
            {
                if (value == _nbPoste)
                {
                    return;
                }

                _nbPoste = value;
                NotifyPropertyChanged("NbPoste");

            }
        }
        int _nbPostePret;
        public int NbPoste_Pret // Le nombre de poste présentement prêt à être utilisé pour le tournoi.
        {
            get
            {
                return _nbPostePret;
            }
            set
            {
                if (value != _nbPostePret)
                {
                    _nbPostePret = value;
                    NotifyPropertyChanged("NbPoste_Pret");

                }

            }
        }
        int _nbPosteAttente;
        public int NbPoste_Attente // Le nombre de poste qui n'ont pas été visité par un volontaire.
        {
            get
            {
                return _nbPosteAttente;
            }
            set
            {
                if (value != _nbPosteAttente)
                {
                    _nbPosteAttente = value;
                    NotifyPropertyChanged("NbPoste_Attente");

                }
            }
        }
        int _nbPosteProbleme;
        public int NbPoste_Probleme // Le nombre de poste ayant rencontré un problème lors de la tournée des volontaires.
        {
            get
            {
                return _nbPosteProbleme;
            }
            set
            {
                if (value != _nbPosteProbleme)
                {
                    _nbPosteProbleme = value;
                    NotifyPropertyChanged("NbPoste_Probleme");

                }
            }
        }
        int _nbPosteNonRequis;
        public int NbPoste_NonRequis // Le nombre de poste qui ne sont plus requis pour le tournoi.
        {
            get
            {
                return _nbPosteNonRequis;
            }
            set
            {
                if (value != _nbPosteNonRequis)
                {
                    _nbPosteNonRequis = value;
                    NotifyPropertyChanged("NbPoste_NonRequis");
                }
            }
        }
        int _nbPosteRequis;
        public int NbPoste_Requis // Le nombre de poste dont l'état n'est pas encore prêt.
        {
            get
            {
                return _nbPosteRequis;
            }
            set
            {
                if (value != _nbPosteRequis)
                {
                    _nbPosteRequis = value;
                    NotifyPropertyChanged("NbPoste_Requis");
                }
            }
        }
        #endregion

        /// <summary>
        /// Constructeur sans paramètre.
        /// </summary>
        public Local()
        {
            LstPoste = new TrulyObservableCollection<Poste>();
            LstPoste.ItemPropertyChanged += PropertyChangedHandler;
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("Etat");
        }
        /// <summary>
        /// Constructeur avec paramètre.
        /// </summary>
        /// <param name="nom">Nom du local Ex: "D125"</param>
        /// <param name="nbPoste">Nombre de postes dans le local</param>
        public Local(string nom, int nbPoste)
        {
            NbPoste = nbPoste;
            NbPoste_Depart = nbPoste;
            Nom = nom;
            LstPoste = new TrulyObservableCollection<Poste>();
            LstPoste.ItemPropertyChanged += PropertyChangedHandler;
            LstPoste.CollectionChanged += LstPoste_CollectionChanged;
        }

        // Si un changement est détecté dans la collection, on calcule les états.
        private void LstPoste_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CalculerEtat();
        }

        /// <summary>
        /// Méthode qui calcule l'état actuel des postes selon le local.
        /// </summary>
        public void CalculerEtat()
        {
            int nbPoste_Pret = 0;
            int nbPoste_Probleme = 0;
            int nbPoste_Attente = 0;
            int nbPoste_NonRequis = 0;
            foreach (Poste unPoste in LstPoste)
            {
                if (unPoste.Etat == "Prêt")
                {
                    nbPoste_Pret++;
                }
                if (unPoste.Etat == "Problème")
                {
                    nbPoste_Probleme++;
                }
                if (unPoste.Etat == "En attente")
                {
                    nbPoste_Attente++;
                }
                if (unPoste.Etat == "Non requis")
                {
                    nbPoste_NonRequis++;
                }
            }
            NbPoste_NonRequis = nbPoste_NonRequis;
            NbPoste_Pret = nbPoste_Pret;
            NbPoste = NbPoste_Depart - NbPoste_NonRequis; // Le nombre de poste est maintenant égal au nombre de poste du départ moins ceux non requis.
            NbPoste_Probleme = nbPoste_Probleme;
            NbPoste_Attente = nbPoste_Attente;
            NbPoste_Requis = NbPoste - NbPoste_Pret;

        }

        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
            // Si l'état d'un poste change, on effectu le calcul des états pour le local.
            if (nomProp == "Etat")
            {
                CalculerEtat();
            }
        }
    }
}

