using System.Collections.Generic;
using System.Text;

namespace Lama.Logic.Model
{
    public class Volontaire : Utilisateur
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomUtilisateur { get; set; }
        public string Matricule { get; set; }
        public string Courriel { get; set; }
        

        public Volontaire(string nom, string prenom, string matricule, string courriel)
        {
            StringBuilder sb = new StringBuilder();
            Prenom = prenom;
            Nom = nom;
            Matricule = matricule;
            Courriel = courriel;
            sb.Append(Nom).Append(" ").Append(Prenom);
            NomComplet = sb.ToString();
        }
    }
}