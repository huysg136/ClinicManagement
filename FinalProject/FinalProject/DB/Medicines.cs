namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Medicines
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicines()
        {
            PrescriptionDetails = new HashSet<PrescriptionDetails>();
        }

        [Key]
        public int Medicine_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Medicine_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Medicine_Unit { get; set; }

        public decimal Medicine_Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrescriptionDetails> PrescriptionDetails { get; set; }
    }
}
