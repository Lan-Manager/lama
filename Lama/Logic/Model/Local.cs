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
        private int _nbPoste;
        public int NbPoste
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
        public int NbPoste_Pret
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
        public int NbPoste_Attente
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
        public int NbPoste_Probleme
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
        int _nbPosteRequis;
        public int NbPoste_Requis
        {
            get
            {
                _nbPosteRequis = NbPoste - NbPoste_Pret;
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
            Nom = nom;
            LstPoste = new TrulyObservableCollection<Poste>();
            LstPoste.ItemPropertyChanged += PropertyChangedHandler;
        }
        /// <summary>
        /// Méthode qui calcule l'état actuel des postes selon le local.
        /// </summary>
        public void CalculerEtat()
        {
            int nbPoste_Pret = 0;
            int nbPoste_Probleme = 0;
            int nbPoste_Attente = 0;

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
            }
            NbPoste_Pret = nbPoste_Pret;
            NbPoste_Probleme = nbPoste_Probleme;
            NbPoste_Attente = nbPoste_Attente;
            NbPoste_Requis = NbPoste - NbPoste_Pret;

        }
        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
            if (nomProp == "NbPoste")
            {
                NbPoste_Requis = _nbPoste - _nbPostePret;
            }
            if (nomProp == "Etat")
            {
                CalculerEtat();
            }
        }
    }
}

