using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class TourHelper
    {
        /// <summary>
        /// Retour async d'une liste contenant toutes les parties d'un tour.
        /// </summary>
        /// <param name="tour">Objet tour auquel appartient les parties.</param>
        /// <returns></returns>
        public async static Task<List<parties>> SelectAllPartieAsync(tours tour)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.parties
                            where p.idTour == tour.idTour
                            orderby p.numPartie ascending
                            select p;
                return await query.ToListAsync();
            }
        }

        /// <summary>
        /// Retour async d'une liste contenant toutes les tours du tournoi.
        /// </summary>
        /// <returns></returns>
        public async static Task<List<tours>> SelectToursAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = ctx.tours
                    .Include(x => x.parties.Select(y => y.equipesparties.Select(z => z.equipes)))
                    .Include(x => x.parties.Select(y => y.equipesparties.Select(z => z.scoresequipesparties.Select(q => q.statistiquesjeux))))
                    .OrderBy(x => x.numTour);


                return await query.ToListAsync();
            }
        }

        public static async Task<bool> InsertAsync(tours obj)
        {
            using (var ctx = new Connexion420())
            {
                ctx.tours.Add(obj);

                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (Exception exct)
                {
                    Console.WriteLine(exct.Message);
                    return false;
                }

                return true;
            }
        }

    }
}
