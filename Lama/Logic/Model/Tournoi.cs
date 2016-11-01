﻿using LamaBD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lama.Logic.Model
{
    public class Tournoi : INotifyPropertyChanged
    {
        #region Propriétés
        public string Etat { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Heure { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Tour> LstTours { get; set; }
        public ObservableCollection<Local> LstLocaux { get; set; }
        public ObservableCollection<Volontaire> LstVolontaires { get; set; }
        public ObservableCollection<Joueur> LstJoueurs { get; set; }
        public ObservableCollection<Equipe> LstEquipes { get; set; }
        public ObservableCollection<Prix> LstPrix { get; set; }
        public Tour TourActif { get; set; }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (value != _nom)
                {
                    _nom = value;
                    NotifyPropertyChanged("Nom");
                }
            }
        }

        private Equipe _vainqueur;
        public Equipe Vainqueur
        {
            get { return _vainqueur; }
            set
            {
                if (value != _vainqueur)
                {
                    _vainqueur = value;
                    NotifyPropertyChanged("Vainqueur");
                }
            }
        }
        #endregion

        #region Constructeurs
        public Tournoi()
        {
            // Infos de base du tournoi
            Nom = "Le tournoi";
            Date = DateTime.Now.Date;
            Heure = DateTime.Now.TimeOfDay;
            Description = null;

            LstLocaux = new ObservableCollection<Local>();
            LstVolontaires = new ObservableCollection<Volontaire>();
            LstJoueurs = new ObservableCollection<Joueur>();
            LstEquipes = new ObservableCollection<Equipe>();
            LstPrix = new ObservableCollection<Prix>();
            LstTours = new ObservableCollection<Tour>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string nomProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));
        }
        #endregion

        #region Méthodes
        public void GenererTour()
        {
            EliminerLesPerdants();

            if (GenerationTourValide())
            {
                TourActif = new Tour();

                List<Equipe> eq = new List<Equipe>(LstEquipes.Where(e => e.EstElimine != true));

                ObservableCollection<PartieEquipe> lstEquipesNonEliminees = new ObservableCollection<PartieEquipe>();

                foreach (var e in eq)
                    if (!e.EstElimine)
                        lstEquipesNonEliminees.Add(new PartieEquipe(e));

                if (lstEquipesNonEliminees.Count() >= 2)
                {
                    int index = 0;

                    for (int i = 0; i < lstEquipesNonEliminees.Count() / 2; i++)
                    {
                        Partie partie = new Partie(lstEquipesNonEliminees[index], lstEquipesNonEliminees[index + 1]);
                        TourActif.LstParties.Add(partie);
                        index += 2;
                    }

                    LstTours.Add(TourActif);
                }

                else if (lstEquipesNonEliminees.Count == 1)
                {
                    Vainqueur = lstEquipesNonEliminees.ElementAt(0).Equipe;
                }
            }

        }

        private void EliminerLesPerdants()
        {
            if (TourActif != null)
                foreach (var partie in TourActif.LstParties)
                    foreach (var equipe in partie.LstEquipes)
                    {
                        if (equipe.EstGagnant == true)
                        {
                            foreach (var e in partie.LstEquipes)
                                if (!equipe.Equals(e))
                                    e.Equipe.EstElimine = true;

                            partie.Gagnant = equipe.Equipe;
                        }
                        else
                        {
                            foreach (var e in partie.LstEquipes)
                                if (!equipe.Equals(e))
                                    e.Equipe.EstElimine = false;
                        }
                    }
        }

        private bool GenerationTourValide()
        {
            foreach (var tour in LstTours)
                foreach (var partie in tour.LstParties)
                    if (partie.Gagnant == null)
                        return false;
            return true;
        }

        public void MiseAJourStatistiques()
        {

            foreach (var equipe in LstEquipes)
            {
                foreach (var stat in equipe.LstStats.Stats)
                    stat.Value = 0;

                foreach (var tour in LstTours)
                    foreach (var partie in tour.LstParties)
                        foreach (var equipe2 in partie.LstEquipes)
                            if (equipe.Equals(equipe2.Equipe))
                                equipe.LstStats = equipe.LstStats + equipe2.LstStats;
            }

        }

        public static LamaBD.tournois getTournoiEntity(Tournoi t)
        {
            LamaBD.tournois entity = new LamaBD.tournois();

            using (var ctx = new Connexion420())
            {
                DateTime dateEvenement = t.Date.Add(t.Heure);
                entity.dateEvenement = dateEvenement;
                entity.dateCreation = DateTime.UtcNow;

                entity.description = t.Description;
                entity.nom = t.Nom;
                entity.enCours = true;

                var etat = ctx.etatstournois
                    .Where(x => x.nom == "Créé")
                    .SingleOrDefault();

                entity.idEtatTournoi = etat.idEtatTournoi;


                var createur = ctx.comptes
                    .Where(x => x.matricule == "1081849")
                    .SingleOrDefault();

                var jeu = ctx.jeux
                    .Where(x => x.nom == "League of Legends")
                    .SingleOrDefault();

                entity.idJeu = jeu.idJeu;

                entity.idCompte = createur.idCompte;

                if (t.LstLocaux.Count > 0)
                {
                    var locaux = ctx.locaux
                        .ToList();

                    foreach (var localModel in t.LstLocaux)
                    {
                        tournoislocaux tl = new tournoislocaux();
                        tl.tournois = entity;

                        var local = locaux.Where(x => x.numero == localModel.Numero).FirstOrDefault();

                        tl.idLocal = local.idLocal;

                        entity.tournoislocaux.Add(tl);
                    }
                }

                if (t.LstVolontaires.Count > 0)
                {
                    var comptes = ctx.comptes.ToList();

                    foreach (var compteModel in t.LstVolontaires)
                    {
                        comptestournois volontaireTournoi = new comptestournois();
                        volontaireTournoi.tournois = entity;

                        var volontaire = comptes.Where(x => x.matricule == compteModel.Matricule).FirstOrDefault();

                        volontaireTournoi.idCompte = volontaire.idCompte;

                        entity.comptestournois.Add(volontaireTournoi);
                    }
                }

                if (t.LstPrix.Count > 0)
                {
                    foreach (var prixModel in t.LstPrix)
                    {
                        prix entityPrix = new prix();
                        entityPrix.nom = prixModel.Nom;

                        prixtournois pt = new prixtournois();
                        pt.tournois = entity;
                        pt.prix = entityPrix;

                        entity.prixtournois.Add(pt);
                    }
                }

                //TODO: Extraire
                if (t.LstJoueurs.Count > 0)
                {
                    foreach (var joueur in t.LstJoueurs)
                    {
                        participants participant = new participants();

                        participant.matricule = joueur.Matricule;
                        participant.nom = joueur.Nom;
                        participant.prenom = joueur.Prenom;
                        //TODO: Trouver une solution pour prendre la date du serveur.
                        participant.dateCreation = DateTime.Now;


                    }
                }

                return entity;
            }
        }


        #endregion
    }
}