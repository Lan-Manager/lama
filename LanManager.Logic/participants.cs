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
    
    public partial class participants
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public participants()
        {
            this.equipesparticipants = new HashSet<equipesparticipants>();
            this.jeuxcomptes = new HashSet<jeuxcomptes>();
        }
    
        public int idParticipant { get; set; }
        public string matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public System.DateTime dateCreation { get; set; }
        public System.DateTime lastUpdated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipesparticipants> equipesparticipants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jeuxcomptes> jeuxcomptes { get; set; }
    }
}
