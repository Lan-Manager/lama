﻿using Lama.Logic.Model;
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

namespace Lama.UI.UC
{
    /// <summary>
    /// Logique d'interaction pour Locaux.xaml
    /// </summary>
    public partial class LocauxUC : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Local> LstLocal { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }

        #region Propriétés de LocauxUC
        private int _nbPostePret;
        public int NbPoste_Pret
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

        public LocauxUC(ObservableCollection<Local> lstLocal)
        {
            LstLocal = new ObservableCollection<Local>();
            LstVolontaires = new ObservableCollection<Volontaire>();
            LocalSelectionne = new Local();
            InitializeComponent();
            this.IsEnabled = false;

            //LstLocal = lstLocal;

            #region Chargement des données en bd
            Task task = new Task(new Action(ChargementDonnes));
            task.ContinueWith(wat =>
            {
                foreach (var l in LstLocal)
                {
                    l.CalculerEtatDepart(); // À la fin du chargement on calcule les états initiaux.
                }
                CalculerEtat(); // On calcule les états lors de l'initialisation de la page.
                Completed();

            }, TaskScheduler.FromCurrentSynchronizationContext());
            task.Start();
            #endregion
           
        }

        private void Completed()
        {
            //Todo
            this.IsEnabled = true;
        }

        private void ChargementDonnes()
        {
            ChargerVolontaires();
            ChargerLocaux();
            ChargerVolontairesAssigne();
            ChargerDernierModificateur();

            foreach (Local l in LstLocal)
            {
                l.PropertyChanged += Local_PropertyChanged;
            }

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                // On met l'index de l'item que l'on veut afficher par défaut.
                cboLocal.SelectedIndex = 0;
            });
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
            if (e.PropertyName == "Etat")
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
            foreach (Local l in LstLocal)
            {
                NbPoste_Pret += l.NbPoste_Pret;
                NbPoste_Probleme += l.NbPoste_Probleme;
                NbPoste_EnAttente += l.NbPoste_Attente;
                NbPoste_Restant += l.NbPoste_Restant;
                NbPoste_Requis += l.NbPoste;
            }

            // Si le nombre de poste restant est a 0 tout les locaux sont prêts a recevoir les joueurs.
            if (NbPoste_Pret == NbPoste_Requis)
            {
                // TODO : Modifier l'état du tournoi pour indiquer que les postes sont prêts. [Si Merge Conflict, cette ligne est la plus a jour]
            }
        }

        #region Action lié à la base de données.
        #region Chargement des données
        // Fonction qui charge les postes lié à la liste de locaux lié au tournoi.
        private void ChargerPostes(Local local, ICollection<postes> postes)
        {
            foreach (postes p in postes)
            {
                // On va chercher le volontaire ayant fait la modification.
                local.LstPoste.Add(new Poste(p.numeroPoste, p.etatspostes.nom)); // On ajoute le poste à la liste de poste.
            }
            local.NbPoste_Depart = postes.Count;
        }

        // Fonction qui charge les locaux lié au tournoi.
        private void ChargerLocaux()
        {
            var taskLocaux = LocalHelper.SelectLocauxTournoiAsync(); // On va chercher la liste des locaux associer à ce tournoi.
            taskLocaux.Wait();
            List<locaux> lLocaux = taskLocaux.Result;
            foreach (locaux l in lLocaux)
            {
                Local modelLocal = new Local(l.numero);

                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    LstLocal.Add(modelLocal); // On ajoute les locaux chercher en BD au tournoi.
                });
                ChargerPostes(modelLocal, l.postes);
            }
        }

        // Fonction qui charge les volontaires liés au tournois.
        private void ChargerVolontaires()
        {
            var taskVolontaire = CompteHelper.SelectAllComptesTournoi(false); // On veut la liste des volontaires donc on doit indiquer false pour estAdmin.
            taskVolontaire.Wait();
            List<comptes> lComptes = taskVolontaire.Result;

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                LstVolontaires.Add(new Volontaire()); // On ajoute les locaux chercher en BD au tournoi.
            });

            foreach (comptes c in lComptes)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    LstVolontaires.Add(new Volontaire(c.nom, c.prenom, c.matricule, c.courriel, c.nomUtilisateur)); // Ajout des volontaires à la liste.
                });

            }
        }
        /// <summary>
        /// Fonction qui assigne un volontaire au local à partir des données de la bd.
        /// </summary>
        private void ChargerVolontairesAssigne()
        {
            foreach (var l in LstLocal)
            {
                var taskVolAss = CompteHelper.SelectByLocalAssigne(l.Numero);
                taskVolAss.Wait();
                string c = taskVolAss.Result;
                foreach (var v in LstVolontaires)
                {
                    if (v.NomUtilisateur == c)
                    {
                        l.VolontaireAssigne = v;
                    }
                }
            }
        }
        /// <summary>
        /// Fonction qui assigne un volontaire au à la propriété représentant la dernière modification de l'état d'un poste.
        /// </summary>
        private void ChargerDernierModificateur()
        {
            foreach (var l in LstLocal)
            {
                foreach (var p in l.LstPoste)
                {
                    var taskVolMod = CompteHelper.SelectByModificationEtat(p.Numero, l.Numero);
                    taskVolMod.Wait();
                    string c = taskVolMod.Result;
                    foreach (var v in LstVolontaires)
                    {
                        if (v.NomUtilisateur == c)
                        {
                            p.DernierModificateur = v;
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
            var taskSave = PosteHelper.UpdateModificateurtAsync(p.Numero, LocalSelectionne.Numero, p.DernierModificateur.NomUtilisateur);
        }

        /// <summary>
        /// Fonction appelé pour sauvegarder l'état d'un poste en BD.
        /// </summary>
        /// <param name="p">Le poste qu'y doit être modifié en BD.</param>
        private void SauvegarderEtatPoste(Poste p)
        {
            var taskSave = PosteHelper.UpdateEtatAsync(p.Numero, LocalSelectionne.Numero, p.Etat);
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
