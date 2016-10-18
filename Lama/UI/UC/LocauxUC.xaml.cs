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
                if (value == _localSelectionne)
                {
                    return;
                }
                _localSelectionne = value;
                NotifyPropertyChanged("LocalSelectionne");
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
        }
        #endregion
        public LocauxUC()
        {
            LocalSelectionne = new Local();
            GetVolontaires();
            GetLocaux();
            GetPoste();
            InitializeComponent();

            // On met l'index de l'item que l'on veut afficher par défaut.
            cboLocal.SelectedIndex = 0;
            
        }
        protected void NotifyPropertyChanged(string nomProp)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nomProp));
            }

        }
        /// <summary>
        /// Fonction qui affiche le sommaire global de l'état des postes pour le tournoi.
        /// </summary>
        private void AfficherSommaireGlobal()
        {
            int nbPret = 0;
            int nbAttente = 0;
            int nbRequis = 0;
            int nbProbleme = 0;
            int nbPoste_Total = 0;
            foreach (Local unLocal in LstLocal)
            {
                foreach (Poste unPoste in unLocal.LstPoste)
                {
                    nbPoste_Total++;
                    if (unPoste.Etat.Equals("Prêt"))
                    {
                        nbPret++;
                    }
                    else if (unPoste.Etat.Equals("En attente"))
                    {
                        nbAttente++;
                    }
                    else if (unPoste.Etat.Equals("Problème"))
                    {
                        nbProbleme++;
                    }
                }
            }
            // Le nombre de local requi est égal au nombre de poste du local moins le nombre de poste prêt a être utilisé.
            nbRequis = nbPoste_Total - nbPret;
            // On change le contenu de l'affichage.
            lblAttente_Global.Content = nbAttente;
            lblPret_Global.Content = nbPret;
            lblProblematique_Global.Content = nbProbleme;
            lblRequis_Global.Content = nbRequis;
        }
    }
}
