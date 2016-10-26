using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.Model.Test
{
    public class Tournoi
    {
        public string Nom { get; set; }
        public string Etat { get { return "Tournoi en cours"; } }
        public Equipe Vainqueur { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Prix> lstPrix { get; set; }
        public Statistiques lstStatistiques { get; set; }
        public ObservableCollection<Equipe> Equipes { get; set; }
        public ObservableCollection<Joueur> Joueurs { get; set; }
        public Tour TourActif { get; set; }

        public Tournoi(string nom)
        {
            Nom = nom;
            Tours = new ObservableCollection<Tour>() { };
            lstPrix = new ObservableCollection<Prix> { new Prix("25$ carte cadeau à la cafétéria"), new Prix() };
            lstStatistiques = new Statistiques();

            Equipes = new ObservableCollection<Equipe>();
            Equipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Max"), new Joueur("Francis"), new Joueur("Tristan"), new Joueur("Antoine"), new Joueur("l'autre dude") }));
            Equipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Ko"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            Equipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Ko123"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            Equipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("K1daso"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            Equipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("Kasdo"), new Joueur("jo"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));
            Equipes.Add(new Equipe(new ObservableCollection<Joueur>() { new Joueur("asdasdasdKo"), new Joueur("joasd"), new Joueur("yoo"), new Joueur("arrr"), new Joueur("l'autre dude2") }));

            GenererTour();
        }

        public void GenererTour()
        {
            EliminerLesPerdants();

            if (GenerationTourValide())
            {
                TourActif = new Tour();

                ObservableCollection<Equipe> equipes = new ObservableCollection<Equipe>(Equipes.Where(e => e.EstElimine != true));

                if (equipes.Count() >= 2)
                {
                    int index = 0;

                    for (int i = 0; i < equipes.Count() / 2; i++)
                    {
                        Partie partie = new Partie(equipes[index], equipes[index + 1]);
                        TourActif.Parties.Add(partie);
                        index += 2;
                    }

                    Tours.Add(TourActif);
                }

                else
                    Vainqueur = equipes.ElementAt(0);
            }

        }

        private void EliminerLesPerdants()
        {
            if (TourActif != null)
            {
                foreach (Partie partie in TourActif.Parties)
                {
                    foreach (Equipe equipe in partie.Equipes)
                    {
                        if (equipe.EstGagnant == true)
                        {
                            foreach (Equipe e in partie.Equipes)
                                if (!equipe.Equals(e))
                                {
                                    e.EstElimine = true;
                                }

                            partie.Gagnant = equipe;
                        }
                        else
                        {
                            foreach (Equipe e in partie.Equipes)
                                if (!equipe.Equals(e))
                                {
                                    e.EstElimine = false;
                                }
                        }

                        equipe.EstGagnant = null;
                    }
                }
            }
        }

        private bool GenerationTourValide()
        {
            foreach (var tour in Tours)
                foreach (var partie in tour.Parties)
                    if (partie.Gagnant == null)
                        return false;
            return true;
        }

    }
}