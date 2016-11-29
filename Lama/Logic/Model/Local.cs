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
        // Le poste sur lequel une modificateur fut apporté.
        public Poste PosteCourant { get; set; }
        // Le numéro du poste.
        public string Numero { get; set; }

        public TrulyObservableCollection<Poste> LstPoste { get; set; }
        public bool EstPret { get; set; }
        // Le volontaire assigné au local.
        private Volontaire _volontaireAssigne;
        public Volontaire VolontaireAssigne
        {
            get
            {
                return _volontaireAssigne;
            }
            set
            {
                if (value != _volontaireAssigne)
                {
                    _volontaireAssigne = value;
                    NotifyPropertyChanged("VolontaireAssigne");
                }
            }
        }
        #region Propriétés concernant le numero de postes.
        private int _nbPosteDepart;
        public int NbPoste_Depart
        {
            get
            {
                return _nbPosteDepart;
            }
            set
            {
                if (value == _nbPosteDepart)
                {
                    return;
                }

                _nbPosteDepart = value;
                NotifyPropertyChanged("NbPoste_Depart");

            }
        } // Le numero de poste ayant été prévu lors de la création du tournoi.

        private int _nbPoste;
        public int NbPoste // Le numero de poste présentement utilisé pour le tournoi.
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
        public int NbPoste_Pret // Le numero de poste présentement prêt à être utilisé pour le tournoi.
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
        public int NbPoste_Attente // Le numero de poste qui n'ont pas été visité par un volontaire.
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
        public int NbPoste_Probleme // Le numero de poste ayant rencontré un problème lors de la tournée des volontaires.
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
        public int NbPoste_NonRequis // Le numero de poste qui ne sont plus requis pour le tournoi.
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
        int _nbPosteRestant;
        public int NbPoste_Restant // Le numero de poste dont l'état n'est pas encore prêt.
        {
            get
            {
                if (_nbPosteRestant < 1)
                {
                    return 0;
                }
                return _nbPosteRestant;
            }
            set
            {
                if (value != _nbPosteRestant)
                {
                    _nbPosteRestant = value;
                    NotifyPropertyChanged("NbPoste_Restant");
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
            VolontaireAssigne = new Volontaire();
        }
        /// <summary>
        /// Constructeur de local contenant le numéro du local.
        /// </summary>
        /// <param name="numero">Numero du local Ex: "D125"</param>
        public Local(string numero)
        {
            Numero = numero;
            LstPoste = new TrulyObservableCollection<Poste>();
            LstPoste.ItemPropertyChanged += PropertyChangedHandler;
            VolontaireAssigne = new Volontaire();
        }

        /// <summary>
        /// Constructeur avec paramètre.
        /// </summary>
        /// <param name="numero">Numero du local Ex: "D125"</param>
        /// <param name="nbPoste">numero de postes dans le local</param>
        public Local(string numero, int nbPoste)
        {
            NbPoste = nbPoste;
            NbPoste_Depart = nbPoste;
            Numero = numero;
            LstPoste = new TrulyObservableCollection<Poste>();
            VolontaireAssigne = new Volontaire();
            for (int i = 0; i < nbPoste; i++)
            {
                LstPoste.Add(new Poste(i + 1, "Non requis"));
            }

            LstPoste.ItemPropertyChanged += PropertyChangedHandler;
        }
        
        /// <summary>
        /// Fonction qui calcul les nouveaux états après un changement.
        /// </summary>
        /// <param name="ancienneValeur">L'ancien état du poste.</param>
        /// <param name="nouvelleValeur">Le nouvel état du poste.</param>
        private void CalculerEtat(string ancienneValeur, string nouvelleValeur)
        {
            switch (ancienneValeur)
            {
                case "Prêt":
                    NbPoste_Pret--;
                    break;
                case "Problème":
                    NbPoste_Probleme--;
                    break;
                case "En attente":
                    NbPoste_Attente--;
                    break;
                case "Non requis":
                    NbPoste_NonRequis--;
                    break;
            }
            switch (nouvelleValeur)
            {
                case "Prêt":
                    NbPoste_Pret++;
                    break;
                case "Problème":
                    NbPoste_Probleme++;
                    break;
                case "En attente":
                    NbPoste_Attente++;
                    break;
                case "Non requis":
                    NbPoste_NonRequis++;
                    break;
            }
            if (NbPoste_Pret == NbPoste)
                EstPret = true;
            else
                EstPret = false;
            NbPoste_Restant = NbPoste - NbPoste_Pret;

        }
        /// <summary>
        /// Fonction qui calcul l'état de tout les postes d'un local. Elle est appelé après le chargement des données de la bd.
        /// </summary>
        public void CalculerEtatDepart()
        {
            NbPoste_Attente = 0;
            NbPoste_NonRequis = 0;
            NbPoste_Pret = 0;
            NbPoste_Probleme = 0;
            foreach (Poste unPoste in LstPoste)
            {
                if (unPoste.Etat == "Prêt")
                {
                    NbPoste_Pret++;
                }
                if (unPoste.Etat == "Problème")
                {
                    NbPoste_Probleme++;
                }
                if (unPoste.Etat == "En attente")
                {
                    NbPoste_Attente++;
                }
                if (unPoste.Etat == "Non requis")
                {
                    NbPoste_NonRequis++;
                }
                
            }
            NbPoste = NbPoste_Depart - NbPoste_NonRequis;
            NbPoste_Restant = NbPoste - NbPoste_Pret;
        }

        #region Interface INotifyPropertyChanged
        /// <summary>
        /// Handler spécifique à la liste de poste.
        /// </summary>
        /// <param name="sender">L'objet qui a provoqué la notification</param>
        /// <param name="e">La propriété qui a changée.</param>
        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Poste)
            {
                PosteCourant = (Poste)sender;
            }
            if (e.PropertyName == "Etat")
            {
                NotifyPropertyChanged("Etat", PosteCourant.AncienEtat, PosteCourant.Etat);
                if (PosteCourant.AncienEtat == "Problème")
                {
                    PosteCourant.Commentaire = null;
                }
            }
            if (e.PropertyName == "DernierModificateur")
            {
                NotifyPropertyChanged("DernierModificateur");
            }
            if (e.PropertyName == "Commentaire")
            {
                NotifyPropertyChanged("Commentaire");
            }
        }
        /// <summary>
        /// Cette fonction est appelé par les setter pour l'état des postes, elle avertie qu'un changement c'est produit.
        /// On garde une trace de l'ancienne valeur de la propriété pour optimiser certains calculs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nomProp">Le nom de la propriété qui a changée</param>
        /// <param name="ancienneValeur">L'ancienne valeur de la propriété.</param>
        /// <param name="nouvelleValeur">La nouvelle valeur de la propriété</param>
        protected void NotifyPropertyChanged<T>(string nomProp, T ancienneValeur, T nouvelleValeur)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedExtendedEventArgs<T>(nomProp, ancienneValeur, nouvelleValeur));
            if (nomProp == "Etat") // Si la propriété changé est l'état du poste.
            {
                // On appel la fonction calculer état en passant en paramètre l'ancienne valeur et la nouvelle.
                CalculerEtat(ancienneValeur.ToString(), nouvelleValeur.ToString()); // Il faut convertir en string puisque template à la base.
            }
        }
        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        #endregion
    }
}

