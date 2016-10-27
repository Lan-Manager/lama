using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model
{
    public class Tournoi
    {
        #region Propriétés
        public string Nom { get; set; }
        public string Etat { get; set; }
        public Equipe Vainqueur { get; set; }
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

            //LstEquipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Max"), new Joueur("Francis"), new Joueur("Tristan"), new Joueur("Antoine"), new Joueur("l'autre dude") }));
            //LstEquipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Ko"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            //LstEquipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Ko123"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            //LstEquipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("K1daso"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            //LstEquipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Kasdo"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            //LstEquipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("asdasdasdKo"), new Joueur("joasd"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));

            GenererTour();
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

                else
                {
                   // Vainqueur = lstEquipesNonEliminees.ElementAt(0).Equipe;
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

                        equipe.EstGagnant = null;
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
    }
    #endregion
}