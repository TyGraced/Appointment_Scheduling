using Appointment_Scheduling.Core.Enums;
using Appointment_Scheduling.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Appointment_Scheduling.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<ProviderAvailability> ProviderAvailability { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var converter = new ValueConverter<AppointmentStatus, string>(
                v => v.ToString(),
                v => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), v));

            builder.Entity<Appointment>()
                .Property(e => e.Status)
                .HasConversion(converter);

            builder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Appointment>()
                .HasOne(a => a.Provider)
                .WithMany()
                .HasForeignKey(a => a.ProviderId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Appointment>(entity =>
            {
                entity.Property(a => a.Date)
                      .HasColumnType("date")
                      .IsRequired();

                entity.Property(a => a.StartTime)
                      .HasColumnType("time")
                      .IsRequired();

                entity.Property(a => a.EndTime)
                      .HasColumnType("time")
                      .IsRequired();
            });
        }

    }
}
