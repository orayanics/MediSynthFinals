using MediSynthFinals.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediSynthFinals.Data
{
    public class MediDbContext : IdentityDbContext<UserCredentials>
    {
        // Constructor
        public MediDbContext(DbContextOptions<MediDbContext> options) : base(options) { }


        // Add service to Program.cs and context for database creation
        public DbSet<UserSchedule> DoctorSchedules { get; set; }
        public DbSet<PatientCredentials> PatientCredentials { get; set; }
        public DbSet<RecordDiagnosis> RecordDiagnosis { get; set; }
        public DbSet<RecordMedHistory> RecordMedHistory { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
    }
}
