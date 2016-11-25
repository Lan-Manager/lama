using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class CompteHelper
    {

        /// <summary>
        /// Fonction qui retourne le compte du volontaire assigné au local.
        /// </summary>
        /// <param name="numeroLocal">Le numéro du local assigné.</param>
        /// <returns></returns>
        public static async Task<string> SelectByLocalAssigne(string numeroLocal)
        {
            using (var ctx = new Connexion420())
            {
                var query = from tl in ctx.tournoislocaux
                            join l in ctx.locaux on tl.idLocal equals l.idLocal
                            join c in ctx.comptes on tl.idCompte equals c.idCompte
                            where l.numero == numeroLocal
                            select c.nomUtilisateur;
                string compte = await query.SingleOrDefaultAsync();
                return compte;
            }
        }
        /// <summary>
        /// Fonction qui retourne le compte du volontaire assigné à la dernière modification.
        /// </summary>
        /// <param name="numeroLocal">Le numéro du local assigné.</param>
        /// <returns></returns>
        public static async Task<string> SelectByModificationEtat(int numeroPoste, string numeroLocal)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.postes
                            join l in ctx.locaux on p.idLocal equals l.idLocal
                            join c in ctx.comptes on p.idCompte equals c.idCompte
                            where p.numeroPoste == numeroPoste && l.numero == numeroLocal
                            select c.nomUtilisateur;
                string compte = await query.SingleOrDefaultAsync();
                return compte;
            }
        }



        public async static Task<List<comptes>> SelectAllAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            orderby c.nomUtilisateur ascending
                            select c;
                return await query.ToListAsync();
            }
        }

        public async static Task<comptes> SelectCompte(string nomUtilisateur)
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            where c.nomUtilisateur == nomUtilisateur
                            select c;
                comptes obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }

        public static async Task<comptes> SelectByIDAsync(string id)
        {
            int num;

            bool success = int.TryParse(id, out num);
            if (!success)
                return null;

            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            where c.idCompte == num
                            select c;
                comptes obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }

        /// <summary>
        /// Retourne de façon async une liste de compte selon le critère de sélection s'il est administrateur ou non.
        /// Par défaut retourne tout les administrateur.
        /// </summary>
        /// <param name="estAdmin">Vrai par défaut, mettez à false pour retourner les non admins.</param>
        /// <returns></returns>
        public static async Task<List<comptes>> SelectAllAdminAsync(bool estAdmin = true)
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            where c.estAdmin == estAdmin
                            orderby c.nomUtilisateur ascending
                            select c;
                return await query.ToListAsync();
            }
        }

        public static async Task<List<comptes>> SelectAllComptesTournoi(bool estAdmin = false)
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            join ct in ctx.comptestournois on c.idCompte equals ct.idCompte
                            join t in ctx.tournois on ct.idTournoi equals t.idTournoi
                            where c.estAdmin == estAdmin && t.enCours == true
                            orderby c.nomUtilisateur ascending
                            select c;
                return await query.ToListAsync();
            }
        }
        public static async Task<bool> InsertAsync(comptes obj)
        {
            using (var ctx = new Connexion420())
            {
                ctx.comptes.Add(obj);

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

        public static async Task<bool> UpdateAsync(comptes obj)
        {
            using (var ctx = new Connexion420())
            {
                ctx.comptes.Attach(obj);
                ctx.Entry(obj).State = EntityState.Modified;
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

        public static bool Delete(string nomUtilisateur)
        {
            using (var ctx = new Connexion420())
            {

                var userAccountParameter = new MySqlParameter("@account", nomUtilisateur);
                var errorCodeParameter = new MySqlParameter("@errorCode", "@errorCode");
                errorCodeParameter.Direction = System.Data.ParameterDirection.Output;

                var details = ctx.Database.ExecuteSqlCommand("CALL DELETE_COMPTE(@account, @errorCode)", userAccountParameter, errorCodeParameter);
                ctx.Database.ExecuteSqlCommand("SELECT @errorCode");

                /*
                                if (!success)
                                    throw new Exception("Retour incorrect du résultat de la procédure");

                                if (data == 0)
                                    return true;
                                else if (data == 1)
                                {
                                    throw new CompteCreateurTournoiException("Le compte est le créateur d'un tournoi.");
                                }
                                else
                                    return false;
                                    */
                return true;
            }
        }

        public class CompteCreateurTournoiException : Exception
        {
            public CompteCreateurTournoiException() { }

            public CompteCreateurTournoiException(string message)
                : base(message)
            {

            }

            public CompteCreateurTournoiException(string message, Exception innerException)
                : base(message, innerException)
            {

            }
        }
    }
}
