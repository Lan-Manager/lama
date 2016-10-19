using Lama.Logic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lama.UI.UC
{
    /// <summary>
    /// Logique d'interaction pour Locaux.xaml
    /// </summary>
    public partial class LocauxUC : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Local> LstLocal { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }

        private int _nbPostePret;
        public int NbPoste_Pret
        {
            get
            {
                foreach (Local unLocal in LstLocal)
                {
                    foreach (Poste unPoste in unLocal.LstPoste)
                    {
                        if (unPoste.Etat == "Prêt")
                        {
                            _nbPostePret++;
                        }
                    }
                }
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
        private int _nbPosteEnAttente;
        public int NbPoste_EnAttente
        {
            get
            {
                foreach (Local unLocal in LstLocal)
                {
                    foreach (Poste unPoste in unLocal.LstPoste)
                    {
                        if (unPoste.Etat == "En attente")
                        {
                            _nbPosteEnAttente++;
                        }
                    }
                }
                return _nbPosteEnAttente;
            }
            set
            {
                if (value != _nbPosteEnAttente)
                {
                    _nbPosteEnAttente = value;
                    NotifyPropertyChanged("NbPoste_EnAttente");
                }
            }
        }
        private int _nbPosteProbleme;
        public int NbPoste_Probleme
        {
            get
            {
                foreach (Local unLocal in LstLocal)
                {
                    foreach (Poste unPoste in unLocal.LstPoste)
                    {
                        if (unPoste.Etat == "Problème")
                        {
                            _nbPosteProbleme++;
                        }
                    }
                }
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
        private int _nbPosteRequis;
        public int NbPoste_Requis
        {
            get
            {
                foreach (Local unLocal in LstLocal)
                {
                    _nbPosteRequis += unLocal.NbPoste_Requis;
                }
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
        private Local _localSelectionne;
        public Local LocalSelectionne
        {
            get
            {
                return _localSelectionne;
            }
            set
            {
                // Si il n'y a pas de changement on ne fait pas d'assignation et on ne lance pas le PropertyChanged.
                if (value != _localSelectionne)
                {
                    _localSelectionne = value;
                    NotifyPropertyChanged("LocalSelectionne");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        //public ObservableCollection<string> LstEtatPossible { get; set; }

        #region Helpers
        /// <summary>
        /// Fonction qui charge une liste des volontaires associés au tournoi à partir de la BD.
        /// </summary>
        private void GetVolontaires()
        {
            LstVolontaires = new ObservableCollection<Volontaire>();
            LstVolontaires.Add(new Volontaire("Jemini", "Tom", "1353049", "abc@gmail.com"));
            LstVolontaires.Add(new Volontaire("Sanchez", "Rick", "1033549", "popsa@msn.com"));
            LstVolontaires.Add(new Volontaire("Jeans", "Mario", "1253334", "tonyhawkembarque@aol.com"));
        }
        /// <summary>
        /// Fonction qui charge une liste de locaux associés au tournoi à partir de la BD.
        /// </summary>
        private void GetLocaux()
        {
            LstLocal = new ObservableCollection<Local>();
            Local L1 = new Local("D125", 5);
            Local L2 = new Local("A320", 4);
            Local L3 = new Local("C200", 1);
            LstLocal.Add(L1);
            LstLocal.Add(L2);
            LstLocal.Add(L3);
        }
        /// <summary>
        /// Fonction qui charge une liste de postes pour chaque locaux.
        /// </summary>
        private void GetPoste()
        {
            Poste P1 = new Poste(1, "Problème");
            Poste P2 = new Poste(2, "Prêt");
            Poste P3 = new Poste(3, "Prêt");
            Poste P4 = new Poste(4, "En attente");
            Poste P5 = new Poste(5, "Problème");

            Poste P6 = new Poste(28, "Problème");
            Poste P7 = new Poste(10, "En attente");
            Poste P8 = new Poste(13, "Prêt");
            Poste P9 = new Poste(7, "En attente");
            Poste P10 = new Poste(6, "Problème");

            
            LstLocal[0].LstPoste.Add(P1);
            LstLocal[0].LstPoste.Add(P2);
            LstLocal[0].LstPoste.Add(P3);
            LstLocal[0].LstPoste.Add(P4);
            LstLocal[0].LstPoste.Add(P5);

            LstLocal[1].LstPoste.Add(P6);
            LstLocal[1].LstPoste.Add(P7);
            LstLocal[1].LstPoste.Add(P8);
            LstLocal[1].LstPoste.Add(P9);
            LstLocal[2].LstPoste.Add(P10);

            foreach(Local l in LstLocal)
            {
                l.CalculerEtat();
            }
        }
        #endregion
        public LocauxUC()
        {
            LocalSelectionne = new Local();
            GetVolontaires();
            GetLocaux();
            GetPoste();
            InitializeComponent();
            // LocalSelectionne.CalculerEtat();
            // On met l'index de l'item que l'on veut afficher par défaut.
            cboLocal.SelectedIndex = 0;
            
        }
        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        } 
    }
}
