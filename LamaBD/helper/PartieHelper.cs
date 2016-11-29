﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class EquipeStatut
    {
        public string NomEquipe { get; set; }
        public bool? EstGagnante { get; set; }
    }
    public class PartieHelper
    {
        /// <summary>
        /// Retour async d'une liste contenant toutes les équipes participantes à une partie.
        /// </summary>
        /// <param name="idPartie">Id de la partie auquel les équipes participent.</param>
        /// <returns></returns>
        public async static Task<List<equipes>> SelectEquipePartieAsync(int idPartie)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.parties
                            join ep in ctx.equipesparties on p.idPartie equals ep.idPartie
                            join e in ctx.equipes on ep.idEquipe equals e.idEquipe
                            where p.idPartie == idPartie
                            select e;
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Retour async d'une liste contenant toutes les équipes participantes à une partie.
        /// </summary>
        /// <param name="idPartie">Id de la partie auquel les équipes participent.</param>
        /// <returns></returns>
        public async static Task<List<EquipeStatut>> SelectEquipePartieAsync(parties partie)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.parties
                            join ep in ctx.equipesparties on p.idPartie equals ep.idPartie
                            join e in ctx.equipes on ep.idEquipe equals e.idEquipe
                            where p.idPartie == partie.idPartie
                            select new EquipeStatut { NomEquipe = e.nom, EstGagnante = ep.estGagnante };
                return await query.ToListAsync();
            }
        }

        public static async Task<bool> EnregistreGagnant(int numPartie, string nomEquipeGagnante)
        {
            using (var ctx = new Connexion420())
            {
                var queryPartie = ctx.parties
                    .Where(x => x.numPartie == numPartie)
                    .FirstOrDefault();

                if (queryPartie != null)
                {
                    queryPartie.dateFin = DateTime.UtcNow;
                    queryPartie.estTermine = true;
                }


                var queryEquipePartie = ctx.equipesparties
                    .Where(x => x.idPartie == queryPartie.idPartie)
                    .Include(x => x.equipes).ToList();

                foreach (equipesparties ep in queryEquipePartie)
                {
                    if (ep.equipes.nom == nomEquipeGagnante)
                        ep.estGagnante = true;
                    else
                        ep.estGagnante = false;
                }

                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
