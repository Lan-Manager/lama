using Lama.Logic.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Lama.UI.Win;

namespace Lama.UI.UC.LocauxControls
{
    /// <summary>
    /// Logique d'interaction pour CarteLocauxUC.xaml
    /// </summary>
    public partial class CarteLocauxUC : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Local> LstLocaux { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }

        private Poste _posteCourant;
        public Poste PosteCourant
        {
            get
            {
                return _posteCourant;
            }
            set
            {
                if (value != _posteCourant)
                {
                    _posteCourant = value;
                    NotifyPropertyChanged("PosteCourant");
                }
            }
        }

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
            LocalSelectionne = LstLocaux[0];
            // On va chercher la liste de volontaires du tournoi.
            LstVolontaires = ParentWindow.TournoiEnCours.LstVolontaires;

            PosteCourant = ParentWindow.TournoiEnCours.LstLocaux[0].LstPoste[0];
            InitializeComponent();
        }
        /// <summary>
        /// Handler pour le click sur un des postes.
        /// </summary>
        /// <param name="sender">L'objet cliqué</param>
        /// <param name="e"></param>
        private void gPoste_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PosteCourant = ((Grid)sender).DataContext as Poste;
        }

        /// <summary>
        /// Handler pour le click du bouton d'ajout d'un commentaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutCommentaire_Click(object sender, RoutedEventArgs e)
        {
            AjouterCommentaire(PosteCourant);
        }

        /// <summary>
        /// Méthode affichant le dialogue pour entrer un commentaire.
        /// </summary>
        /// <param name="p">Le poste auquel on doit ajouter un commentaire.</param>
        public void AjouterCommentaire(Poste p)
        {
            MetroWindow commentaireWin = new CommentaireWin((MainWindow)Application.Current.MainWindow, p);
            commentaireWin.ShowDialog();
        }
    }
}
