using System.Collections.Generic;

namespace Lama.Logic.Model
{
    public class Volontaire
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomUtilisateur { get; set; }
        public string Matricule { get; set; }
        public string Courriel { get; set; }



        public Volontaire(string nom, string prenom, string matricule, string courriel)
        {
            Prenom = prenom;
            Nom = nom;
            Matricule = matricule;
            Courriel = courriel;
        }
    }
}