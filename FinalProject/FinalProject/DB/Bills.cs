namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bills
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bills()
        {
            Services = new HashSet<Services>();
        }

        [Key]
        public int Bill_ID { get; set; }

        public int Patient_ID { get; set; }

        public decimal Total_Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime Bill_Date { get; set; }

        public virtual Patients Patients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Services> Services { get; set; }
    }
}
