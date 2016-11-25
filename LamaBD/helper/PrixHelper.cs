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
    }
}
