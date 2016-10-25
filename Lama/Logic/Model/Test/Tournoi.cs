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
            Tour tour = new Tour();

            ObservableCollection<Partie> parties = new ObservableCollection<Partie>();
            ObservableCollection<Equipe> equipes = new ObservableCollection<Equipe>();

            foreach (Equipe equipe in Equipes)
                if (!equipe.EstElimine)
                    hjhj;

            foreach (Equipe equipe in Equipes)
                if (!equipe.EstElimine)
                    equipes.Add(equipe);

            for (int i = 0; i < Equipes.Count() / 2; i += 2)
            {
                Partie partie = new Partie(Equipes[i], Equipes[i + 1]);
                tour.Parties.Add(partie);
            }
            Tours.Add(tour);
        }
    }
}
