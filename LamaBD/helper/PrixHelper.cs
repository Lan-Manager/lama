using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class PrixHelper
    {
        public static async Task<List<prix>> SelectAllPrixTournoiAsync()
        {
            using (var ctx = new Connexion420())
            {
                var task = TournoiHelper.SelectLast();
                int idTournoi = task.Result.idTournoi;

                var query = ctx.prix
                    .Include(x => x.prixtournois)
                    .Include(x => x.equipes)
                    .Where(x => x.prixtournois.Any(y => y.idTournoi == idTournoi))
                    .OrderBy(x => x.nom);

                return await query.ToListAsync();
            }
        }

        public static async Task<bool> AssigneGagnant(string nomPrix, string nomEquipe)
        {
            using (var ctx = new Connexion420())
            {
                var queryEquipe = ctx.equipes
                    .Where(x => x.nom == nomEquipe)
                    .FirstOrDefault();

                var query = ctx.prix
                    .Where(x => x.nom == nomPrix)
                    .FirstOrDefault();

                query.idEquipe = queryEquipe.idEquipe;

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
