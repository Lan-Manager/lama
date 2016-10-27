using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using Lama.UI.UC;
using Lama.UI.Win;
using Lama.Logic.Model;
using System.ComponentModel;
using Lama.UI.UC.TournoiControls;

namespace Lama
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Tournoi TournoiEnCours { get; set; }

        private Utilisateur _utilisateur;
        public Utilisateur Utilisateur
        {
            get
            {
                return _utilisateur;
            }
            set
            {
                if (_utilisateur != value)
                {
                    _utilisateur = value;
                    NotifyPropertyChanged("Utilisateur");
                }
            }
        }

        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
            if (nomProp == "EstConnecte" || nomProp == "Utilisateur")
            {
                AfficherElement();
            }
        }

        // Fonction qui affiche/cache ou modifie le texte de certain élément selon l'état de l'utilisateur.
        private void AfficherElement()
        {
            if (Utilisateur == null || Utilisateur.EstConnecte == false)
            {
                tabLocaux.Visibility = Visibility.Hidden;
                tabContenant.SelectedItem = tabTournoi;
                hplAuthentification.Content = "S'authentifier";
            }
            else
            {
                tabLocaux.Content = new LocauxUC();
                tabLocaux.Visibility = Visibility.Visible;
                hplAuthentification.Content = "Se désauthentifier";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Utilisateur = new Utilisateur();
            Utilisateur.PropertyChanged += PropertyChanged;
            TournoiEnCours = new Tournoi();
            this.DataContext = this;
        }

        private void Authentification_Click(object sender, RoutedEventArgs e)
        {
            // Si l'utilisateur n'est pas identifié, on affiche le formulaire d'authentification.
            if (Utilisateur == null || Utilisateur.EstConnecte == false)
            {
                AuthentificationWin FenetreAuthentification = new AuthentificationWin();
                FenetreAuthentification.ShowDialog();
                Utilisateur = FenetreAuthentification.Utilisateur;
            }
            // Si l'utilisateur est déjà identifié, on le désauthentifie en remettant le statut d'utilisateur.
            else
            {
                Utilisateur = new Utilisateur();
            }
        }
    }
}
