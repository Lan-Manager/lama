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
            LstLocal = new ObservableCollection<Local>();
            LstVolontaires = new ObservableCollection<Volontaire>();
            LocalSelectionne = new Local();

            #region Chargement des données en bd
            ChargerVolontaires();
            ChargerLocaux();
            ChargerPostes();
            ChargerVolontairesAssigne();
            ChargerDernierModificateur();
            #endregion

            InitializeComponent();

            foreach (Local l in LstLocal)
            {
                l.PropertyChanged += Local_PropertyChanged;
            }
            
            // On met l'index de l'item que l'on veut afficher par défaut.
            cboLocal.SelectedIndex = 0;
            CalculerEtat();
        }


        private void Local_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "NbPoste_Depart" || e.PropertyName == "NbPoste_Pret" || e.PropertyName == "NbPoste_Attente" || e.PropertyName == "NbPoste_Probleme" || e.PropertyName == "NbPoste_NonRequis" || e.PropertyName == "NbPoste_Restant" || e.PropertyName == "NbPoste")
            {
                CalculerEtat();
            }
            if (e.PropertyName == "VolontaireAssigne")
            {
                SauvegarderVolontaireAssigne();
            }
            if (e.PropertyName == "Etat")
            {
                SauvegarderEtatPoste(LocalSelectionne.PosteCourant);
            }
            if (e.PropertyName == "DernierModificateur")
            {
                SauvegarderVolontaireModification(LocalSelectionne.PosteCourant);
            }
        }

        private void SauvegarderVolontaireModification(Poste p)
        {
            var taskSave = PosteHelper.UpdateModificateurtAsync(p.Numero, LocalSelectionne.Numero, p.DernierModificateur.NomUtilisateur);
        }

        private void SauvegarderEtatPoste(Poste p)
        {
            var taskSave = PosteHelper.UpdateEtatAsync(p.Numero, LocalSelectionne.Numero, p.Etat);
        }

        protected void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        
        /// <summary>
        /// Fonction qui calcule les états et met a jour les propriétés pour l'affichage du sommaire global.
        /// </summary>
        private void CalculerEtat()
        {
            int nbPoste_Pret = 0;
            int nbPoste_Probleme = 0;
            int nbPoste_Attente = 0;
            int nbPoste_Restant = 0;
            int nbPoste_Requis = 0;

            foreach (Local l in LstLocal)
            {
                nbPoste_Pret += l.NbPoste_Pret;
                nbPoste_Probleme += l.NbPoste_Probleme;
                nbPoste_Attente += l.NbPoste_Attente;
                nbPoste_Restant += l.NbPoste_Restant;
                nbPoste_Requis += l.NbPoste;
            }
            NbPoste_Pret = nbPoste_Pret;
            NbPoste_Probleme = nbPoste_Probleme;
            NbPoste_EnAttente = nbPoste_Attente;
            NbPoste_Restant = nbPoste_Restant;
            NbPoste_Requis = nbPoste_Requis;
        }

        // Fonction qui charge les postes lié à la liste de locaux lié au tournoi.
        private void ChargerPostes()
        {
            foreach(Local l in LstLocal)
            {
                var task = PosteHelper.SelectAllByNumeroLocalAsync(l.Numero);
                task.Wait();
                List<postes> lPostes = task.Result;

                foreach (postes p in lPostes)
                {
                    // On va chercher l'état du poste courant.
                    var taskEtat = PosteHelper.SelectEtatAsync(p.idPoste);
                    taskEtat.Wait();
                    // On va chercher le volontaire ayant fait la modification.
                    string nomEtat = taskEtat.Result.nom;
                    l.LstPoste.Add(new Poste(p.numeroPoste, nomEtat)); // On ajoute le poste à la liste de poste.
                }
                l.NbPoste_Depart = lPostes.Count;
                l.CalculerEtat();
            }
        }
        // Fonction qui charge les locaux lié au tournoi.
        private void ChargerLocaux()
        {
            var taskLocaux = LocalHelper.SelectLocauxTournoiAsync(); // On va chercher la liste des locaux associer à ce tournoi.
            taskLocaux.Wait();
            List<locaux> lLocaux = taskLocaux.Result;
            foreach (locaux l in lLocaux)
            {
                LstLocal.Add(new Local(l.numero)); // On ajoute les locaux chercher en BD au tournoi.
            }
        }
        // Fonction qui charge les volontaires liés au tournois.
        private void ChargerVolontaires()
        {
            var taskVolontaire = CompteHelper.SelectAllComptesTournoi(false); // On veut la liste des volontaires donc on doit indiquer false pour estAdmin.
            taskVolontaire.Wait();
            List<comptes> lComptes = taskVolontaire.Result;

            foreach (comptes c in lComptes)
            {
                LstVolontaires.Add(new Volontaire(c.nom, c.prenom, c.matricule, c.courriel, c.nomUtilisateur)); // Ajout des volontaires à la liste.
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
        /// Fonction qui assigne un volontaire au local à partir des données de la bd.
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
        /// <summary>
        /// Méthode pour sauvegarder le volontaire assigné à un  local en BD.
        /// </summary>
        private void SauvegarderVolontaireAssigne()
        {
            var task = LocalHelper.UpdateAsync(LocalSelectionne.Numero, LocalSelectionne.VolontaireAssigne.NomUtilisateur);
        }
        
    }
}
