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
    
    public partial class equipesparties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public equipesparties()
        {
            this.scoresequipesparties = new HashSet<scoresequipesparties>();
        }
    
        public int idEquipePartie { get; set; }
        public int idPartie { get; set; }
        public int idEquipe { get; set; }
        public Nullable<bool> estGagnante { get; set; }
    
        public virtual equipes equipes { get; set; }
        public virtual parties parties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<scoresequipesparties> scoresequipesparties { get; set; }
    }
}
