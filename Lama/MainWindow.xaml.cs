﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using Lama.UI.UC;
using Lama.UI.Win;
using Lama.Logic.Model;
using System.ComponentModel;
using LamaBD.helper;
using LamaBD;
using System.Collections.ObjectModel;

namespace Lama
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Tournoi _tournoiEnCours;

        public Tournoi TournoiEnCours
        {
            get
            {
                return _tournoiEnCours;
            }

            set
            {
                if (_tournoiEnCours != value)
                {
                    _tournoiEnCours = value;
                    NotifyPropertyChanged("TournoiEnCours");
                }
            }
        }

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
            if (nomProp == "EstConnecte" || nomProp == "Utilisateur" || nomProp == "TournoiEnCours")
            {
                AfficherElement();
            }
            
        }

        #region code BD
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
            var task = LocalHelper.SelectAllAsync();
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
        #endregion

        // Fonction qui affiche/cache ou modifie le texte de certain élément selon l'état de l'utilisateur.
        private void AfficherElement()
        {

            if (Utilisateur == null || Utilisateur.EstConnecte == false)
            {
                tabLocaux.Visibility = Visibility.Hidden;
                tabContenant.SelectedItem = tabTournoi;
            }
            else
            {
                if (Utilisateur.EstAdmin)
                {
                    btnCreerTournoi.Visibility = Visibility.Visible;
                }
                if (TournoiEnCours.LstLocaux.Count() > 0)
                {
                    tabLocaux.Content = new LocauxUC();
                    tabLocaux.Visibility = Visibility.Visible;
                }
                else
                {
                    tabLocaux.Visibility = Visibility.Hidden;
                }
            }
        }

        public MainWindow()
        {

            InitializeComponent();

            Utilisateur = new Utilisateur();
            Utilisateur.PropertyChanged += PropertyChanged;

            this.DataContext = this;

            TournoiEnCours = ChargerTournoi();
        }

        private void Authentification_Click(object sender, RoutedEventArgs e)
        {
            // Si l'utilisateur n'est pas identifié, on affiche le formulaire d'authentification.
            if (Utilisateur == null || Utilisateur.EstConnecte == false)
            {
                AuthentificationWin FenetreAuthentification = new AuthentificationWin(this);
                FenetreAuthentification.ShowDialog();
                if (FenetreAuthentification.Utilisateur != null)
                {
                    Utilisateur = FenetreAuthentification.Utilisateur;
                }
            }
            // Si l'utilisateur est déjà identifié, on le désauthentifie en remettant le statut d'utilisateur.
            else
            {
                Utilisateur = new Utilisateur();
                btnCreerTournoi.Visibility = Visibility.Hidden;
            }
        }

        private void btnCreerTournoi_Click(object sender, RoutedEventArgs e)
        {
            CreerTournoiWindow creerTournoiWindow = new CreerTournoiWindow();
            creerTournoiWindow.ShowDialog();

            TournoiEnCours = creerTournoiWindow.temp;
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
