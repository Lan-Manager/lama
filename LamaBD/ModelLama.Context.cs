﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LamaBD
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Connexion420 : DbContext
    {
        public Connexion420()
            : base("name=Connexion420")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<comptes> comptes { get; set; }
        public virtual DbSet<comptestournois> comptestournois { get; set; }
        public virtual DbSet<equipes> equipes { get; set; }
        public virtual DbSet<equipesparticipants> equipesparticipants { get; set; }
        public virtual DbSet<equipesparties> equipesparties { get; set; }
        public virtual DbSet<etatspostes> etatspostes { get; set; }
        public virtual DbSet<etatstournois> etatstournois { get; set; }
        public virtual DbSet<jeux> jeux { get; set; }
        public virtual DbSet<jeuxcomptes> jeuxcomptes { get; set; }
        public virtual DbSet<locaux> locaux { get; set; }
        public virtual DbSet<messages> messages { get; set; }
        public virtual DbSet<participants> participants { get; set; }
        public virtual DbSet<prix> prix { get; set; }
        public virtual DbSet<prixtournois> prixtournois { get; set; }
        public virtual DbSet<scoresequipesparties> scoresequipesparties { get; set; }
        public virtual DbSet<statistiques> statistiques { get; set; }
        public virtual DbSet<statistiquesjeux> statistiquesjeux { get; set; }
        public virtual DbSet<tours> tours { get; set; }
        public virtual DbSet<tournoislocaux> tournoislocaux { get; set; }
        public virtual DbSet<postes> postes { get; set; }
        public virtual DbSet<parties> parties { get; set; }
        public virtual DbSet<tournois> tournois { get; set; }
        public virtual DbSet<typestournois> typestournois { get; set; }
    
        public virtual int FIN_TOURNOI()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FIN_TOURNOI");
        }
    
        public virtual int DELETE_Compte(string account, ObjectParameter status)
        {
            var accountParameter = account != null ?
                new ObjectParameter("account", account) :
                new ObjectParameter("account", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DELETE_Compte", accountParameter, status);
        }
    }
}
