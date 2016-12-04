using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class StatsHelper
    {

        public static async Task<List<statistiquesjeux>> SelectStatsJeuAsync(string nomJeu)
        {
            using (var ctx = new Connexion420())
            {
                var stats = ctx.statistiquesjeux
                    .Include(x => x.jeux)
                    .Where(x => x.jeux.nom == nomJeu)
                    .Include(x => x.statistiques);

                return await stats.ToListAsync();
            }
        }

        public static async Task<List<scoresequipesparties>> SelectScoresJeuAsync()
        {
            using (var ctx = new Connexion420())
            {
                var scores = ctx.scoresequipesparties
                    .Include(x => x.statistiquesjeux.statistiques);

                return await scores.ToListAsync();
            }
        }

        public static async Task<bool> MiseAjourStats(int numeroPartie, decimal valeur, string nomEquipe, string nomStat)
        {
            using (var ctx = new Connexion420())
            {
                var queryPartie = ctx.equipesparties
                    .Include(x => x.parties)
                    .Include(x => x.equipes)
                    .Where(x => x.parties.numPartie == numeroPartie && x.equipes.nom == nomEquipe)
                    .FirstOrDefault();

                var queryStats = ctx.statistiquesjeux
                    .Include(x => x.statistiques)
                    .Where(x => x.statistiques.nom == nomStat)
                    .FirstOrDefault();

                var query = ctx.scoresequipesparties
                    .Where(x => x.idEquipePartie == queryPartie.idEquipePartie && x.idStatistiqueJeu == queryStats.idStatistiqueJeu)
                    .FirstOrDefault();

                query.valeur = valeur;

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
