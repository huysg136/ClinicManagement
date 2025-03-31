namespace FinalProject.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrescriptionDetails
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Prescription_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Medicine_ID { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(255)]
        public string Dosage { get; set; }

        public virtual Medicines Medicines { get; set; }

        public virtual Prescriptions Prescriptions { get; set; }
    }
}
