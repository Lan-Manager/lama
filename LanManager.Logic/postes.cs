//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LanManager.Logic
{
    using System;
    using System.Collections.Generic;
    
    public partial class postes
    {
        public int idPoste { get; set; }
        public int idLocal { get; set; }
        public int idEtatPoste { get; set; }
        public Nullable<int> idCompte { get; set; }
        public int numeroPoste { get; set; }
        public System.DateTime lastUpdated { get; set; }
    
        public virtual comptes comptes { get; set; }
        public virtual etatspostes etatspostes { get; set; }
        public virtual locaux locaux { get; set; }
    }
}
