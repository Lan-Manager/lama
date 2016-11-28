using Lama.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lama.Logic.TypeTournoi
{
    public interface ITypeTournoi
    {
        /// <summary>
        /// Méthode pour savoir si oui ou non l'objet a été initialisé.
        /// </summary>
        /// <returns>Vrai si initialisé, sinon false.</returns>
        bool EstInitialiser();
        /// <summary>
        /// Initialise l'objet ITypeTournoi avec une liste d'équipes et une string de configuration.
        /// Appeler une autre méthode avant appeler cette fonction lancer une exception.
        /// </summary>
        /// <param name="equipes">Liste d'équipes qui participent au tournoi.</param>
        /// <param name="configString">String de configuration propre à chaque objet implémentant l'interface.</param>
        /// <returns>True si succès, false s'il y a une erreur.</returns>
        bool Initialiser(List<Equipe> equipes, string configString);
        /// <summary>
        /// Génère le prochain tour (ou le premier tour) du tournoi.
        /// </summary>
        /// <param name="equipesPerdantes">Liste d'équipe ayant été éliminées au tour précédent. Envoyez une liste vide pour le premier tour.</param>
        /// <returns>Retourne une liste des parties pour le tour actuel.</returns>
        Tour GenererTour(List<Equipe> equipesPerdantes);
        /// <summary>
        /// Méthode permettant d'obtenir une liste des équipes ayant été éliminé au cours du tournoi.
        /// </summary>
        /// <returns>Liste d'équipes ayant été éléminé depuis le début du tournoi.</returns>
        List<Equipe> ObtenirEliminer();
        /// <summary>
        /// Méthode permettant d'obtenir une liste des équipes toujours en compétition au tournoi (donc, les équipes qui ne sont pas éliminées).
        /// </summary>
        /// <returns>Liste d'équipes qui sont toujours en compétition dans le tournoi.</returns>
        List<Equipe> ObtenirCompetiteurs();
        /// <summary>
        /// Méthode permettant d'obtenir une liste des équipes ayant participé au tournoi.
        /// </summary>
        /// <returns>Liste d'équipes ayant participé au tournoi (éliminé ou non).</returns>
        List<Equipe> ObtenirParticipants();
        /// <summary>
        /// Méthode permettant d'obtenir le numéro du tour actuel du tournoi.
        /// </summary>
        /// <returns>Retourne le numéro du tour auquel le tournoi est rendu.</returns>
        int ObtenirNumTourCourant();
        /// <summary>
        /// Méthode permettant d'obtenir le numéro de la dernière partie générée depuis le début du tournoi.
        /// </summary>
        /// <returns>Retourne le numéro de la dernière partie du tournoi générée.</returns>
        int ObtenirNumDernierePartie();
        /// <summary>
        /// Méthode pour obtenir l'équipe ayant reçut un bye au tour actuel.
        /// </summary>
        /// <returns>Retourne l'équipe ayant reçut un bye.</returns>
        List<Equipe> ObtenirBye();
        /// <summary>
        /// Méthode pour obtenir la liste des tours du tournoi.
        /// </summary>
        /// <returns>Liste de tours de tout le tournoi.</returns>
        List<Tour> ObtenirTours();




    }
}
