using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lama.Logic.Model;

namespace Lama.Logic.TypeTournoi
{
    public class Elimination : ITypeTournoi
    {
        public List<Tour> listTours;

        private bool estInitialiser = false;
        private int numTour = 0;
        private int numPartie = 0;
        private bool estSimpleElimination = true;
        private int maxNumberGames;
        private int numberOfBye;
        private static Random rng = new Random(Guid.NewGuid().GetHashCode());

        private List<Equipe> equipesParticipantes;
        private List<Equipe> equipesEliminees;
        private List<Equipe> equipesCompetiteurs;
        private List<Equipe> listBye;
        private Dictionary<Equipe, int> equipesVies;

        private List<Equipe> loserBracket;
        private List<Equipe> winnerBracket;


        public bool Initialiser(List<Equipe> equipes, string configString = "elimination=simple")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                dict = ParseConfig(configString);
            }
            catch (Exception e)
            {
                //Pas important de gérer. On veut juste ne pas planter si la string n'est pas correct.
                //Nous avons déjà des configs par défauts.
            }
            if (dict.ContainsKey("elimination"))
            {
                string typeElimination = dict["elimination"];
                if (typeElimination.Equals("simple"))
                {
                    estSimpleElimination = true;
                }
                else if (typeElimination.Equals("double"))
                {
                    loserBracket = new List<Equipe>();
                    winnerBracket = new List<Equipe>();
                    estSimpleElimination = false;
                }
                else
                    estSimpleElimination = true;
            }


            if (estSimpleElimination)
                maxNumberGames = equipes.Count();
            else
                maxNumberGames = (equipes.Count() * 2) - 1;
            listTours = new List<Tour>();
            equipesVies = new Dictionary<Equipe, int>();
            equipesParticipantes = new List<Equipe>(equipes.Count);
            equipesEliminees = new List<Equipe>(equipes.Count);
            equipesCompetiteurs = new List<Equipe>(equipes.Count);

            Elimination.Shuffle(equipes);

            foreach (Equipe equipe in equipes)
            {
                if (estSimpleElimination)
                    equipesVies.Add(equipe, 1);
                else
                    equipesVies.Add(equipe, 2);
                equipesCompetiteurs.Add(equipe);
                equipesParticipantes.Add(equipe);
            }
            numberOfBye = nlpo2(UInt32.Parse(equipes.Count.ToString())) - equipes.Count();
            listBye = new List<Equipe>(numberOfBye);
            estInitialiser = true;
            return EstInitialiser();
        }

        public Tour GenererTour(List<Equipe> equipesPerdantes)
        {
            VerificationInitialisation();

            numTour++;
            Tour tour;
            if (equipesPerdantes == null)
            {
                listBye = equipesCompetiteurs.ToList(); ;
                int nbrEquipePremierTour = equipesParticipantes.Count - numberOfBye;
                if (estSimpleElimination)
                    tour = CreerTour(equipesCompetiteurs, numTour, nbrEquipePremierTour);
                else
                    tour = CreerTourDoubleElimination(equipesCompetiteurs, numTour, nbrEquipePremierTour);
            }
            else
            {
                AjustementEquipe(equipesPerdantes);
                if (estSimpleElimination)
                    tour = CreerTour(equipesCompetiteurs, numTour);
                else
                    tour = CreerTourDoubleElimination(equipesCompetiteurs, numTour);
            }
            listTours.Add(tour);
            return tour;
        }

        public bool EstInitialiser()
        {
            return estInitialiser;
        }

        public List<Equipe> ObtenirEliminer()
        {
            VerificationInitialisation();
            return equipesEliminees;
        }

        public List<Equipe> ObtenirCompetiteurs()
        {
            VerificationInitialisation();
            return equipesCompetiteurs;
        }

        public List<Equipe> ObtenirParticipants()
        {
            VerificationInitialisation();
            return equipesParticipantes;
        }

        public int ObtenirNumTourCourant()
        {
            VerificationInitialisation();
            return numTour;
        }

        public int ObtenirNumDernierePartie()
        {
            VerificationInitialisation();
            return numPartie;
        }

        public List<Equipe> ObtenirBye()
        {
            VerificationInitialisation();
            return listBye;
        }

        private void VerificationInitialisation()
        {
            if (!estInitialiser)
                throw new InvalidOperationException("Objet Elimination n'est pas initialisé.");
        }


        public List<Tour> ObtenirTours()
        {
            VerificationInitialisation();
            return listTours;
        }

        private Dictionary<string, string> ParseConfig(string configurationString)
        {
            Dictionary<string, string> dict =
                configurationString.Split('?')[1]
                    .Split('&')
                    .Select(x => x.Split('='))
                    .ToDictionary(y => y[0], y => y[1]);
            return dict;
        }


        //https://stackoverflow.com/questions/600293/how-to-check-if-a-number-is-a-power-of-2
        private bool IsPowerOfTwo(ulong x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }


        //https://stackoverflow.com/questions/273313/randomize-a-listt
        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //https://stackoverflow.com/questions/5525122/c-sharp-math-question-smallest-power-of-2-bigger-than-x
        private int nlpo2(uint x)
        {
            x--; // comment out to always take the next biggest power of two, even if x is already a power of two
            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return (int)(x + 1);
        }

        private Tour CreerTour(List<Equipe> equipes, int numtour, int nbrEquipePremierTour)
        {
            Tour tour = new Tour(numTour);
            int index = 0;
            for (int i = 0; i < equipes.Count() / 2; i++)
            {
                if (nbrEquipePremierTour == 0)
                    break;
                else
                    nbrEquipePremierTour -= 2;
                PartieEquipe pe1 = new PartieEquipe(equipes[index]);
                PartieEquipe pe2 = new PartieEquipe(equipes[index + 1]);
                listBye.Remove(pe1.Equipe);
                listBye.Remove(pe2.Equipe);
                Partie partie = new Partie(pe1, pe2, ++numPartie);

                index += 2;
                tour.LstParties.Add(partie);
            }
            return tour;
        }

        private Tour CreerTour(List<Equipe> equipes, int numtour)
        {
            Tour tour = new Tour(numTour);
            int index = 0;
            for (int i = 0; i < equipes.Count() / 2; i++)
            {
                PartieEquipe pe1 = new PartieEquipe(equipes[index]);
                PartieEquipe pe2 = new PartieEquipe(equipes[index + 1]);
                Partie partie = new Partie(pe1, pe2, ++numPartie);

                index += 2;
                tour.LstParties.Add(partie);
            }
            return tour;
        }


        private Tour CreerTourDoubleElimination(List<Equipe> equipes, int numtour, int nbrEquipePremierTour)
        {
            winnerBracket = equipes.ToList();
            Tour tour = this.CreerTour(equipes, numtour, nbrEquipePremierTour);
            return tour;
        }

        private Tour CreerTourDoubleElimination(List<Equipe> equipes, int numtour)
        {
            Tour tour = new Tour(numTour);
            int index = 0;
            for (int i = 0; i < equipes.Count() / 2; i++)
            {
                PartieEquipe pe1 = new PartieEquipe(equipes[index]);
                PartieEquipe pe2 = new PartieEquipe(equipes[index + 1]);
                Partie partie = new Partie(pe1, pe2, ++numPartie);

                index += 2;
                tour.LstParties.Add(partie);
            }
            return tour;
        }

        private void AjustementEquipe(List<Equipe> equipesPerdantes)
        {
            //List<Equipe> equipesGagnante = equipesCompetiteurs.Where(x => !equipesPerdantes.Any(y => x == y)).ToList();
            equipesCompetiteurs.Clear();

            foreach (Equipe e in equipesPerdantes)
            {
                equipesVies[e] -= 1;
                if (equipesVies[e] == 0)
                    equipesEliminees.Add(e);
                else
                    equipesCompetiteurs.Add(e);
            }
        }
    }
}
