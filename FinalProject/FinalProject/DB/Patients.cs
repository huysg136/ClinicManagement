namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patients()
        {
            Bills = new HashSet<Bills>();
            MedicalRecords = new HashSet<MedicalRecords>();
        }

        [Key]
        public int Patient_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Patient_Full_Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Patient_DOB { get; set; }

        [StringLength(10)]
        public string Patient_Gender { get; set; }

        [Required]
        [StringLength(15)]
        public string Patient_Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string Patient_Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bills> Bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalRecords> MedicalRecords { get; set; }
    }
}
