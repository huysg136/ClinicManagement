namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MedicalRecords
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalRecords()
        {
            Prescriptions = new HashSet<Prescriptions>();
        }

        [Key]
        public int Record_ID { get; set; }

        public int Patient_ID { get; set; }

        public int Doctor_ID { get; set; }

        public int Diagnosis_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Record_Date { get; set; }

        public virtual DiagnosisTreatment DiagnosisTreatment { get; set; }

        public virtual Doctors Doctors { get; set; }

        public virtual Patients Patients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
