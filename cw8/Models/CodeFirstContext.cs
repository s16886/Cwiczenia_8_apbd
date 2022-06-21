using Microsoft.EntityFrameworkCore;
using System;

namespace cw8.Models //Add-Migration MigrationName       Update-Database
{
    public class CodeFirstContext : DbContext
    {
        public CodeFirstContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(e => e.IdDoctor).HasName("Doctor_pk");
                e.ToTable("Doctor");
                e.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
                e.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                e.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                e.Property(e => e.Email).IsRequired().HasMaxLength(100);
                
                e.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "JanMail@gmail.com" },
                    new Doctor { IdDoctor = 2, FirstName = "Tom", LastName = "Scot", Email = "TomMail@gmail.com" }
                    );
            });

            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(e => e.IdMedicament).HasName("Medicament_pk");
                e.ToTable("Medicament");
                e.Property(e => e.IdMedicament).ValueGeneratedOnAdd();
                e.Property(e => e.Name).IsRequired().HasMaxLength(100);
                e.Property(e => e.Description).IsRequired().HasMaxLength(100);
                e.Property(e => e.Type).IsRequired().HasMaxLength(100);

                e.HasData(
                    new Medicament { IdMedicament = 1, Name = "Xylolit", Description = "Soft medicament", Type = "n-1" },
                    new Medicament { IdMedicament = 2, Name = "Ibuprom", Description = "Hard medicament", Type = "sigma-beta" }
                    );
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(e => e.IdPatient).HasName("Patient_pk");
                e.ToTable("Patient");
                e.Property(e => e.IdPatient).ValueGeneratedOnAdd();
                e.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                e.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                e.Property(e => e.Birthdate).IsRequired();

                e.HasData(
                    new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = DateTime.Parse("1999-01-01") },
                    new Patient { IdPatient = 2, FirstName = "Tom", LastName = "Scot", Birthdate = DateTime.Parse("1988-03-06") }
                    );
            });

            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(e => e.IdPrescription).HasName("Prescription_pk");
                e.ToTable("Prescription");
                e.Property(e => e.IdPrescription).ValueGeneratedOnAdd();
                e.Property(e => e.Date).IsRequired();
                e.Property(e => e.DueDate).IsRequired();

                e.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Patient_Prescription");
                e.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Doctor_Prescription");

                e.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2024-01-01"), IdPatient =  1, IdDoctor = 1 },
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-09-08"), DueDate = DateTime.Parse("2026-09-08"), IdPatient = 2, IdDoctor = 2 }
                    );
            });
            modelBuilder.Entity<Prescription_Medicament>(e =>
            {
                e.HasKey(e => new { e.IdPrescription, e.IdMedicament }).HasName("Prescription_Medicament_pk");
                e.ToTable("Prescription_Medicament");
                e.Property(e => e.Dose);
                e.Property(e => e.Details).IsRequired().HasMaxLength(100);

                e.HasOne(e => e.Medicament).WithMany(e => e.Prescriptions_Medicaments).HasForeignKey(e => e.IdMedicament).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Medicament_Prescription_Medicament"); ;
                e.HasOne(e => e.Prescription).WithMany(e => e.Prescriptions_Medicaments).HasForeignKey(e => e.IdPrescription).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Prescription_Medicament"); ;

                e.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 45, Details = "1 pill in the morning" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 20, Details = "2 pills a day" }
                    );
            });
        }
    }
}
