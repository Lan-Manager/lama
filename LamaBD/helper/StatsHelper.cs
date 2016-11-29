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
    }
}
