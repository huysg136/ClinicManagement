using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FinalProject.DB
{
    public partial class DbClinic : DbContext
    {
        public DbClinic()
            : base("name=DbClinic")
        {
        }

        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DiagnosisTreatment> DiagnosisTreatment { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<MedicalRecords> MedicalRecords { get; set; }
        public virtual DbSet<Medicines> Medicines { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<PrescriptionDetails> PrescriptionDetails { get; set; }
        public virtual DbSet<Prescriptions> Prescriptions { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bills>()
                .Property(e => e.Total_Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Bills>()
                .HasMany(e => e.Services)
                .WithMany(e => e.Bills)
                .Map(m => m.ToTable("BillDetails").MapLeftKey("Bill_ID").MapRightKey("Service_ID"));

            modelBuilder.Entity<Doctors>()
                .Property(e => e.Doctor_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Medicines>()
                .Property(e => e.Medicine_Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Patients>()
                .Property(e => e.Patient_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Service_Price)
                .HasPrecision(10, 2);
        }
    }
}
