namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiagnosisTreatment")]
    public partial class DiagnosisTreatment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiagnosisTreatment()
        {
            MedicalRecords = new HashSet<MedicalRecords>();
        }

        [Key]
        public int Diagnosis_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Diagnosis_Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Treatment_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalRecords> MedicalRecords { get; set; }
    }
}
