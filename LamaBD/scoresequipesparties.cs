//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class scoresequipesparties
    {
        public int idScoreEquipePartie { get; set; }
        public int idStatistiqueJeu { get; set; }
        public int idEquipePartie { get; set; }
        public decimal valeur { get; set; }
    
        public virtual equipesparties equipesparties { get; set; }
        public virtual statistiquesjeux statistiquesjeux { get; set; }
    }
}
