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

        private bool estInitialiser = false;
        private uint numTour = 0;
        private bool estSimpleElimination = false;

        private List<Equipe> equipesParticipantes;
        private List<Equipe> equipesEliminees;
        private List<Equipe> equipesCompetiteurs;
        private Equipe equipeBye;


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
                    estSimpleElimination = false;
                }
            }


            equipesParticipantes = new List<Equipe>(equipes.Count);
            equipesEliminees = new List<Equipe>(equipes.Count);
            equipesCompetiteurs = new List<Equipe>(equipes.Count);

            foreach (Equipe equipe in equipes)
            {
                equipesCompetiteurs.Add(equipe);
                equipesParticipantes.Add(equipe);
            }
            estInitialiser = true;
            return EstInitialiser();
        }

        public List<Partie> GenererTour(List<Equipe> equipesPerdantes)
        {
            VerificationInitialisation();
            throw new NotImplementedException();
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

        public uint ObtenirNumTourCourant()
        {
            VerificationInitialisation();
            return numTour;
        }

        public Equipe ObtenirBye()
        {
            VerificationInitialisation();
            return equipeBye;
        }

        private void VerificationInitialisation()
        {
            if (!estInitialiser)
                throw new InvalidOperationException("SimpleElimination n'est pas initialisé.");
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
    }
}
