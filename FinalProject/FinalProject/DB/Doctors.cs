namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Doctors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctors()
        {
            MedicalRecords = new HashSet<MedicalRecords>();
        }

        [Key]
        public int Doctor_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Doctor_Full_Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Doctor_Phone { get; set; }

        public int Department_ID { get; set; }

        public virtual Departments Departments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalRecords> MedicalRecords { get; set; }
    }
}
