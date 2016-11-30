using Lama.Logic.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System;
using System.Windows;
using LamaBD.helper;
using LamaBD;
using System.Collections.Generic;
using Lama.UI.Model;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Lama.UI.UC.LocauxControls
{
    /// <summary>
    /// Logique d'interaction pour Locaux.xaml
    /// </summary>
    public partial class LocauxUC : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Local> LstLocaux { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires_Assignable { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires_DernierModificateur { get; set; }

        #region Propriétés de LocauxUC
        private int _nbPostePret;
        public int NbPoste_Pret
        {
            get
            {
                if (_nbPostePret < 1)
                {
                    return 0;
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
                if (_nbPosteProbleme < 1)
                {
                    return 0;
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
        private int _nbPosteRestant;
        public int NbPoste_Restant
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
        private int _nbRequis;
        public int NbPoste_Requis
        {
            get
            {
                if (_nbRequis < 1)
                {
                    return 0;
                }
                return _nbRequis;
            }
            set
            {
                if (value != _nbRequis)
                {
                    _nbRequis = value;
                    NotifyPropertyChanged("NbPoste_Requis");
                }
            }
        }
        #endregion

        // Le local sélectionné dans la liste.
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
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        // La fenêtre parent (MainWindow)
        public MainWindow ParentWindow { get; set; }
        public LocauxUC()
        {
            LstLocaux = new ObservableCollection<Local>();
            LstVolontaires_Assignable = new ObservableCollection<Volontaire>();
            LstVolontaires_Assignable.Add(new Volontaire()); // On ajoute une entrée vide à la liste.
            LstVolontaires_DernierModificateur = new ObservableCollection<Volontaire>();

            MainWindow ParentWindow = (MainWindow)Application.Current.MainWindow;
            LstLocaux = ParentWindow.TournoiEnCours.LstLocaux;
            LstVolontaires_DernierModificateur = ParentWindow.TournoiEnCours.LstVolontaires;
            // On ajoute la liste de volontaire à la liste contenant une entrée vide.
            foreach (var v in ParentWindow.TournoiEnCours.LstVolontaires)
            {
                LstVolontaires_Assignable.Add(v);
            }

            // On subscribe les events de propriétés changeantes et on calcul les états de départ.
            foreach (Local l in LstLocaux)
            {
                l.PropertyChanged += Local_PropertyChanged;
                l.CalculerEtatDepart(); // À la fin du chargement on calcule les états initiaux.

            }
            // S'il y a une liste de joueurs associés au tournoi.
            if (ParentWindow.TournoiEnCours.LstJoueurs != null)
            {
                int nbJoueur = ParentWindow.TournoiEnCours.LstJoueurs.Count;
                DistributionJoueurLocaux(nbJoueur);
            }
            CalculerEtat();

            InitializeComponent();
            this.IsEnabled = false;

            // On met l'index de l'item que l'on veut afficher par défaut.
            cboLocal.SelectedIndex = 0;


            #region Chargement des données en bd
            Task task = new Task(new Action(ChargementDonnes));
            task.ContinueWith(wat =>
            {
                Completed();

            }, TaskScheduler.FromCurrentSynchronizationContext());
            task.Start();

            #endregion


        }

        /// <summary>
        /// Méthode qui distribue le nombre de joueurs entre les locaux disponibles.
        /// </summary>
        /// <param name="nbJoueur">Le nombre de joueurs participant au tournoi.</param>
        private void DistributionJoueurLocaux(int nbJoueur)
        {
            double nb = LstLocaux.Count;

            if (LstLocaux.Count % 2 != 0 && LstLocaux.Count > 1)
            {
                nb -= 1;
            }

            double nbJoueurParLocal = nbJoueur / nb;
            int nbEquipe = nbJoueur / 5;

            if (LstLocaux.Count != nbEquipe) // Il n'y a pas de local pour chaque équipe
            {
                for (int i = 0; i < nb; i++)
                {
                    LocalSelectionne = LstLocaux[i];
                    for (int j = 0; j < nbJoueurParLocal;) // Un maximum de 20 postes peut être utilisé.
                    {
                        if (LstLocaux[i].LstPoste[j].Etat == "Non requis")
                        {
                            LocalSelectionne.LstPoste[j].Etat = "En attente";
                        }
                        if (j < 20)
                            j++;
                        else
                            return;
                    }
                }
            }
            else // Il y a un local pour chaque équipe
            {
                foreach (var l in LstLocaux)
                {
                    LocalSelectionne = l;
                    for (int i = 0; i < 5;) // Une équipe par local donc 5 postes requis par local.
                    {
                        if (LocalSelectionne.LstPoste[i].Etat == "Non requis")
                        {
                            LocalSelectionne.LstPoste[i].Etat = "En attente";
                        }
                        if (i < 20)
                            i++;
                        else
                            break;
                    }
                }
            }
        }



        private void Completed()
        {
            //Todo
            this.IsEnabled = true;
        }

        private void ChargementDonnes()
        {
            ChargerVolontairesAssigne();
            ChargerDernierModificateur();

        }
        /// <summary>
        /// Handler pour le click du bouton d'ajout d'un commentaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutCommentaire_Click(object sender, RoutedEventArgs e)
        {
            Poste p = ((Button)sender).DataContext as Poste;
            AjouterCommentaire(p);
        }

        /// <summary>
        /// Méthode affichant le dialogue pour entrer un commentaire.
        /// </summary>
        /// <param name="p">Le poste auquel on doit ajouter un commentaire.</param>
        public async void AjouterCommentaire(Poste p)
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
        /// <summary>
        /// Handler spécifique au locaux.
        /// </summary>
        /// <param name="sender">L'objet qui a provoqué la notification</param>
        /// <param name="e">La propriété qui a changée.</param>
        private void Local_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VolontaireAssigne")
            {
                SauvegarderVolontaireAssigne();
            }
            if (e.PropertyName == "Etat" || e.PropertyName == "Commentaire")
            {
                CalculerEtatChangement(LocalSelectionne.PosteCourant.AncienEtat, LocalSelectionne.PosteCourant.Etat);
                SauvegarderEtatPoste(LocalSelectionne.PosteCourant);
            }
            if (e.PropertyName == "DernierModificateur")
            {
                SauvegarderVolontaireModification(LocalSelectionne.PosteCourant);
            }
        }


        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }

        /// <summary>
        /// Fonction qui calcul les nouveaux états après un changement.
        /// </summary>
        /// <param name="ancienEtat">L'ancien état du poste.</param>
        /// <param name="nouvelEtat">Le nouvel état du poste.</param>
        private void CalculerEtatChangement(string ancienEtat, string nouvelEtat)
        {
            switch (ancienEtat)
            {
                case "Prêt":
                    NbPoste_Pret--;
                    break;
                case "Problème":
                    NbPoste_Probleme--;
                    break;
                case "En attente":
                    NbPoste_EnAttente--;
                    break;
            }
            switch (nouvelEtat)
            {
                case "Prêt":
                    NbPoste_Pret++;
                    break;
                case "Problème":
                    NbPoste_Probleme++;
                    break;
                case "En attente":
                    NbPoste_EnAttente++;
                    break;
            }

            NbPoste_Restant = NbPoste_Requis - NbPoste_Pret;
        }
        /// <summary>
        /// Fonction qui calcule les états de départ et met a jour les propriétés pour l'affichage du sommaire global.
        /// </summary>
        private void CalculerEtat()
        {
            foreach (Local l in LstLocaux)
            {
                NbPoste_Pret += l.NbPoste_Pret;
                NbPoste_Probleme += l.NbPoste_Probleme;
                NbPoste_EnAttente += l.NbPoste_Attente;
                NbPoste_Restant += l.NbPoste_Restant;
                NbPoste_Requis += l.NbPoste;
            }
        }

        #region Action lié à la base de données.
        #region Chargement des données
        //// Fonction qui charge les postes lié à la liste de locaux lié au tournoi.
        //private void ChargerPostes(Local local, ICollection<postes> postes)
        //{
        //    foreach (postes p in postes)
        //    {
        //        // On va chercher le volontaire ayant fait la modification.
        //        local.LstPoste.Add(new Poste(p.numeroPoste, p.etatspostes.nom, p.commentaire)); // On ajoute le poste à la liste de poste.
        //    }
        //    local.NbPoste_Depart = postes.Count;
        //}

        //// Fonction qui charge les locaux lié au tournoi.
        //private void ChargerLocaux()
        //{
        //    foreach (Local l in LstLocaux)
        //    {
        //        l.PropertyChanged += Local_PropertyChanged;
        //    }
        //    App.Current.Dispatcher.Invoke((Action)delegate
        //    {
        //        // On met l'index de l'item que l'on veut afficher par défaut.
        //        cboLocal.SelectedIndex = 0;
        //    });

        //}

        //// Fonction qui charge les volontaires liés au tournois.
        //private void ChargerVolontaires()
        //{
        //    var taskVolontaire = CompteHelper.SelectAllComptesTournoi(false); // On veut la liste des volontaires donc on doit indiquer false pour estAdmin.
        //    taskVolontaire.Wait();
        //    List<comptes> lComptes = taskVolontaire.Result;

        //    App.Current.Dispatcher.Invoke((Action)delegate
        //    {
        //        LstVolontaires_Assignable.Add(new Volontaire()); // On ajoute les locaux chercher en BD au tournoi.
        //    });

        //    foreach (comptes c in lComptes)
        //    {
        //        App.Current.Dispatcher.Invoke((Action)delegate
        //        {
        //            LstVolontaires_Assignable.Add(new Volontaire(c.nom, c.prenom, c.matricule, c.courriel, c.nomUtilisateur)); // Ajout des volontaires à la liste.
        //            LstVolontaires_DernierModificateur.Add(new Volontaire(c.nom, c.prenom, c.matricule, c.courriel, c.nomUtilisateur)); // Ajout des volontaires à la liste.
        //        });

        //    }
        //}
        /// <summary>
        /// Fonction qui assigne un volontaire au local à partir des données de la bd.
        /// </summary>
        private void ChargerVolontairesAssigne()
        {
            foreach (var l in LstLocaux)
            {
                var taskVolAss = CompteHelper.SelectByLocalAssigne(l.Numero);
                taskVolAss.Wait();
                string c = taskVolAss.Result;
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    foreach (var v in LstVolontaires_Assignable)
                    {

                        if (v.NomUtilisateur == c)
                        {
                            l.VolontaireAssigne = v;
                        }

                    }
                });
            }
        }
        /// <summary>
        /// Fonction qui assigne un volontaire au à la propriété représentant la dernière modification de l'état d'un poste.
        /// </summary>
        private void ChargerDernierModificateur()
        {
            foreach (var l in LstLocaux)
            {
                foreach (var p in l.LstPoste)
                {
                    var taskVolMod = CompteHelper.SelectByModificationEtat(p.Numero, l.Numero);
                    taskVolMod.Wait();
                    string c = taskVolMod.Result;

                    foreach (var v in LstVolontaires_DernierModificateur)
                    {
                        if (v.NomUtilisateur == c)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                p.DernierModificateur = v;
                            });
                        }
                    }

                }
            }
        }
        #endregion
        #region Sauvegarde des données
        /// <summary>
        /// Fonction appelé pour sauvegarder le nom du volontaire qui a fait la modification de l'état du poste.
        /// </summary>
        /// <param name="p">Le poste qui a été ciblé par la modification</param>
        private void SauvegarderVolontaireModification(Poste p)
        {
            if (p != null)
            {
                var taskSave = PosteHelper.UpdateModificateurtAsync(p.Numero, LocalSelectionne.Numero, p.DernierModificateur.NomUtilisateur);
            }
        }

        /// <summary>
        /// Fonction appelé pour sauvegarder l'état d'un poste en BD.
        /// </summary>
        /// <param name="p">Le poste qu'y doit être modifié en BD.</param>
        private void SauvegarderEtatPoste(Poste p)
        {
            var taskSave = PosteHelper.UpdateEtatAsync(p.Numero, LocalSelectionne.Numero, p.Etat, p.Commentaire);
        }
        /// <summary>
        /// Méthode pour sauvegarder le volontaire assigné à un  local en BD.
        /// </summary>
        private void SauvegarderVolontaireAssigne()
        {
            var task = LocalHelper.UpdateAsync(LocalSelectionne.Numero, LocalSelectionne.VolontaireAssigne.NomUtilisateur);
        }
        #endregion

        #endregion


    }
}
