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

namespace Lama.UI.UC.LocauxControls
{
    /// <summary>
    /// Logique d'interaction pour CarteLocauxUC.xaml
    /// </summary>
    public partial class CarteLocauxUC : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Local> LstLocaux { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires_DernierModificateur { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }

        public CarteLocauxUC()
        {
            // On va chercher la liste de locaux du tournoi.
            MainWindow ParentWindow = (MainWindow)Application.Current.MainWindow;
            LstLocaux = ParentWindow.TournoiEnCours.LstLocaux;
            
            // On va chercher la liste de volontaires du tournoi.
            LstVolontaires_DernierModificateur = ParentWindow.TournoiEnCours.LstVolontaires;
            
            InitializeComponent();
            cboLocal.SelectedIndex = 0;
        }
    }
}
