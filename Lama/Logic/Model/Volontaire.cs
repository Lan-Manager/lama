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


        // Liste de local auquel le volontaire est assigné.
        public List<Local> LstLocal { get; set; }

        public Volontaire(string nom, string prenom, string matricule, string courriel)
        {
            Prenom = prenom;
            Nom = nom;
            Matricule = matricule;
            Courriel = courriel;
            LstLocal = new List<Local>();
        }

        /// <summary>
        /// Fonction pour assigner un local à un volontaire.
        /// </summary>
        /// <param name="nouveauLocal">Le local que l'on souhaite ajouter à la liste des locaux du volontaire.</param>
        public void AssignerLocal(Local nouveauLocal)
        {
            foreach (Local unLocal in LstLocal)
            {
                if (nouveauLocal.Nom == unLocal.Nom) // si le local est déja dans la liste.
                {
                    return;
                }
            }
            // S'il n'est pas déjà dans la liste, on l'ajoute.
            LstLocal.Add(nouveauLocal);
        }

        /// <summary>
        /// Fonction pour retirer l'assignation d'un local. Utile si l'on change le volontaire assigné à un local.
        /// </summary>
        /// <param name="local">Le local que l'on souhaite retirer.</param>
        public void RetirerAssignationLocal(Local local)
        {
            foreach (Local unLocal in LstLocal)
            {
                if (unLocal.Nom == local.Nom)
                {
                    LstLocal.Remove(local);
                }
            }
        }
    }
}