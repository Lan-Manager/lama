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
using Lama.Logic.Tools;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Lama.UI.UC.LocauxControls
{
    /// <summary>
    /// Logique d'interaction pour CarteLocauxUC.xaml
    /// </summary>
    public partial class CarteLocauxUC : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Local> LstLocaux { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }

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
            LstVolontaires = ParentWindow.TournoiEnCours.LstVolontaires;
            
            InitializeComponent();
            cboLocal.SelectedIndex = 0;
        }

        /// <summary>
        /// Méthode affichant le dialogue pour entrer un commentaire.
        /// </summary>
        /// <param name="p">Le poste auquel on doit ajouter un commentaire.</param>
        public async void Afficher_Menu(Poste p)
        {
            // On va chercher la fenêtre parent (MainWindow dans ce cas-ci) avec la référence du contrôle (this).
            MetroWindow parent = Window.GetWindow(this) as MetroWindow;
            var metroWindow = parent;

            // On donne le thème général de l'application à la fenêtre de saisi
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;

            // On extrait le résultat.
            string result = await metroWindow.ShowInputAsync("Commentaire", p.Commentaire, metroWindow.MetroDialogOptions);

            // Si l'utilisateur n'a pas fait cancel.
            if (result != null)
            {
                p.Commentaire = result;
            }
        }

        private void gPoste_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Poste p = ((Grid)sender).DataContext as Poste;
            CustomDialog cm = new MenuCarte();
            cm.ShowModalDialogExternally();
        }
    }
}
