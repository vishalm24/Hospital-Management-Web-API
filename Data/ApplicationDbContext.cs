using Hospital_Management.Model;
using Hospital_Management.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<DocDept> DocDept { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().HasOne(dc => dc.Department)
                .WithMany(d => d.Doctors)
                .HasForeignKey(dc => dc.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Doctor>().HasOne(dc =>dc.Admin)
                .WithMany(u => u.Users)
                .HasForeignKey(dc => dc.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Doctor>().HasOne(dc => dc.user)
                .WithOne(u => u.doctor)
                .HasForeignKey<Doctor>(dc => dc.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasOne(u => u.Admin)
                .WithMany(u => u.UserAdminIds)
                .HasForeignKey(u => u.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Appointment>().HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);
            modelBuilder.Entity<Appointment>().HasOne(a => a.Doctor)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.DoctorId);
            modelBuilder.Entity<Appointment>().HasOne(a => a.Receptionist)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.ReceptionistId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MedicalHistory>().HasOne(a => a.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(a => a.PatientId);
            modelBuilder.Entity<MedicalHistory>().HasOne(a => a.Appointment)
                .WithOne(m => m.MedicalHistory)
                .HasForeignKey<MedicalHistory>(a => a.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MedicalHistory>().HasOne(a => a.Doctor)
                .WithMany(dc => dc.MedicalHistories)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MedicalHistory>().HasOne(a => a.Department)
                .WithMany(d => d.MedicalHistories)
                .HasForeignKey(a => a.DepartmentId);
            modelBuilder.Entity<Leave>().HasOne(l => l.Doctor)
                .WithMany(dc => dc.Leaves)
                .HasForeignKey(l => l.DoctorId);
            modelBuilder.Entity<Leave>().HasOne(l => l.Admin)
                .WithMany(u => u.DoctorAdminIds)
                .HasForeignKey(l => l.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DoctorDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<LeaveGetDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<DocDept>().HasNoKey().ToView(null);
        }
    }
}
