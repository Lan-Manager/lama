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
    
    public partial class comptes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public comptes()
        {
            this.comptestournois = new HashSet<comptestournois>();
            this.messages = new HashSet<messages>();
            this.tournois = new HashSet<tournois>();
            this.tournoislocaux = new HashSet<tournoislocaux>();
            this.postes = new HashSet<postes>();
        }
    
        public int idCompte { get; set; }
        public string nomUtilisateur { get; set; }
        public string motDePasse { get; set; }
        public string courriel { get; set; }
        public string matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public bool estAdmin { get; set; }
        public System.DateTime dateCreation { get; set; }
        public System.DateTime lastUpdated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comptestournois> comptestournois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<messages> messages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tournois> tournois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tournoislocaux> tournoislocaux { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<postes> postes { get; set; }
    }
}
