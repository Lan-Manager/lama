//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LamaBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class parties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parties()
        {
            this.equipesparties = new HashSet<equipesparties>();
        }
    
        public int idPartie { get; set; }
        public int idTour { get; set; }
        public int numPartie { get; set; }
        public bool estTermine { get; set; }
        public Nullable<System.TimeSpan> dureeJeu { get; set; }
        public Nullable<System.DateTime> dateFin { get; set; }
        public System.DateTime lastUpdated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipesparties> equipesparties { get; set; }
        public virtual tours tours { get; set; }
    }
}
