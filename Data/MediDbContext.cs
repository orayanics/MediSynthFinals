using MediSynthFinals.Models;
using Microsoft.EntityFrameworkCore;

namespace MediSynthFinals.Data
{
    public class MediDbContext : DbContext
    {
        // Constructor
        public MediDbContext(DbContextOptions<MediDbContext> options) : base(options) { }


        // Add service to Program.cs and context for database creation
        public DbSet<AdminCredentials> AdminCredentials { get; set; }
        public DbSet<DoctorCredentials> DoctorCredentials { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set;}
        public DbSet<PatientCredentials> PatientCredentials { get; set; }
        public DbSet<RecordDiagnosis> RecordDiagnosis { get; set; }
        public DbSet<RecordMedHistory> RecordMedHistory { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<DoctorCredentials>().HasData(
        //        new DoctorCredentials()
        //        {
        //            email = "oraya@gmail.com"
        //        },
        //        );
        //}
    }
}
