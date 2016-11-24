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
        public string NomComplet
        {
            get
            {
                return $"{Prenom} {Nom}";
            }
        }

        // Constructeur vide pour volontaire
        public Volontaire()
        {

        }
        // Constructeur sans matricule pour administrateur.
        public Volontaire(string nom, string prenom, string courriel, string nomUtilisateur)
        {
            Prenom = prenom;
            Nom = nom;
            Courriel = courriel;
            NomUtilisateur = nomUtilisateur;

        }
        public Volontaire(string nom, string prenom, string matricule, string courriel, string nomUtilisateur)
        {
            Prenom = prenom;
            Nom = nom;
            Matricule = matricule;
            Courriel = courriel;
            NomUtilisateur = nomUtilisateur;
        }
    }
}