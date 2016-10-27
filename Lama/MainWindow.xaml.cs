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
using LamaBD.helper;
using LamaBD;
using System.Collections.ObjectModel;

namespace Lama
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Utilisateur Utilisateur { get; set; }
        public Tournoi LeTournoi { get; set; }

        public MainWindow()
        {
            Utilisateur = new Utilisateur();
            LeTournoi = ChargerTournoi();
            InitializeComponent();
        }

        private Tournoi ChargerTournoi()
        {
            var task = TournoiHelper.SelectLast();
            task.Wait();

            tournois t = task.Result;

            Tournoi T = new Tournoi();

            if (t == null)
            {
                return T;
            }


            // Tournoi
            T.Nom = t.nom;
            T.Date = t.dateEvenement.Date;
            T.Heure = t.dateEvenement.TimeOfDay;
            T.Description = t.description;

            // Locaux
            T.LstLocaux = ChargerLocaux();

            // Volontaires
            T.LstVolontaires = ChargerVolontaires();

            // Participants
            T.LstJoueurs = null; // ils sont dans Equipes

            // Équipes
            T.LstEquipes = ChargerEquipes();

            // Prix
            T.LstPrix = null;

            return T;
        }

        private ObservableCollection<Local> ChargerLocaux()
        {
            var task = LocalHelper.SelectLocauxTournoiAsync();
            task.Wait();

            List<locaux> data = task.Result;

            ObservableCollection<Local> lstTemp = new ObservableCollection<Local>();

            foreach (var local in data)
            {
                lstTemp.Add(new Local(local.numero, local.postes.Count));
            }

            return lstTemp;
        }

        private ObservableCollection<Volontaire> ChargerVolontaires()
        {
            var task = CompteHelper.SelectAllComptesTournoi(false);
            task.Wait();

            List<comptes> data = task.Result;

            ObservableCollection<Volontaire> lstTemp = new ObservableCollection<Volontaire>();

            foreach (var volontaire in data)
            {
                lstTemp.Add(new Volontaire(volontaire.nom, volontaire.prenom, volontaire.matricule, volontaire.courriel));
            }

            return lstTemp;
        }

        private ObservableCollection<Equipe> ChargerEquipes()
        {
            var task = EquipeHelper.SelectAll();
            task.Wait();

            List<equipes> data = task.Result;

            ObservableCollection<Equipe> lstTemp = new ObservableCollection<Equipe>();

            foreach (var equipe in data)
            {
                lstTemp.Add(new Equipe(equipe.nom));
            }

            return lstTemp;
        }

        private void Authentification_Click(object sender, RoutedEventArgs e)
        {
            AuthentificationWin FenetreAuthentification = new AuthentificationWin(Utilisateur);
            FenetreAuthentification.ShowDialog();

            Utilisateur = FenetreAuthentification.Utilisateur;

        }

        private void btnCreerTournoi_Click(object sender, RoutedEventArgs e)
        {
            CreerTournoiWindow creerTournoiWindow = new CreerTournoiWindow();
            creerTournoiWindow.ShowDialog();

            LeTournoi = creerTournoiWindow.LeTournoi;
        }
    }
}
