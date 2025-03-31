namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prescriptions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prescriptions()
        {
            PrescriptionDetails = new HashSet<PrescriptionDetails>();
        }

        [Key]
        public int Prescription_ID { get; set; }

        public int Record_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Prescription_Date_Issued { get; set; }

        public virtual MedicalRecords MedicalRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrescriptionDetails> PrescriptionDetails { get; set; }
    }
}
