using Appointment_Scheduling.Core.Enums;
using Appointment_Scheduling.Core.Exceptions;
using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Infrastructure.Repository.Implementations
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        #region Admin

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

        public async Task<IEnumerable<Appointment>> GetCancelledAppointmentsAsync(bool trackChanges)
        {
            var cancelledAppointments = await FindAll(trackChanges)
                .Where(c => c.Status == AppointmentStatus.Cancelled)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
            return cancelledAppointments;
        }

        public async Task<IEnumerable<Appointment>> GetCompletedAppointmentsAsync(bool trackChanges)
        {
            var completedAppointments = await FindAll(trackChanges)
                .Where(c => c.Status == AppointmentStatus.Completed)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
            return completedAppointments;
        }

        public async Task<IEnumerable<Appointment>> GetConfirmedAppointmentsAsync(bool trackChanges)
        {
            var confirmedAppointments = await FindAll(trackChanges)
                .Where(c => c.Status == AppointmentStatus.Confirmed)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
            return confirmedAppointments;
        }

        public async Task<IEnumerable<Appointment>> GetPendingAppointmentsAsync(bool trackChanges)
        {
            var pendingAppointments = await FindAll(trackChanges)
                .Where(c => c.Status == AppointmentStatus.Pending)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
            return pendingAppointments;
        }

        #endregion

        #region Patient

        public async Task BookAppointment(Appointment appointment)
        {
            appointment.Date = appointment.Date.Date;

            if (appointment.EndTime <= appointment.StartTime.Add(TimeSpan.FromMinutes(30)))
                throw new ArgumentException("EndTime must be at least 30 minutes after StartTime.");

            Create(appointment);
        }

        public async Task CancelAppointmentAsync(Guid patientId, Guid providerId)
        {
            var existingAppointment = await FindByCondition(a => a.PatientId.Equals(patientId)
                && a.ProviderId.Equals(providerId), trackChanges: true)
                .SingleOrDefaultAsync();

            if (existingAppointment == null)
            {
                throw new AppointmentNotFound(patientId);
            }

            var patientFullName = $"{existingAppointment.Patient!.FirstName} {existingAppointment.Patient.LastName}";

            existingAppointment.Status = AppointmentStatus.Cancelled;
            existingAppointment.UpdatedAt = DateTime.UtcNow;
            existingAppointment.UpdatedBy = patientFullName;

            Update(existingAppointment);
        }

        #endregion

        #region Provider

        public async Task<Appointment?> GetAppointmentAsync(Guid patientId, Guid providerId, bool trackChanges) =>
            await FindByCondition(a => a.PatientId.Equals(patientId) && a.ProviderId.Equals(providerId), trackChanges)
                    .SingleOrDefaultAsync();

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync(Guid providerId, bool trackChanges) =>
            await FindAll(trackChanges).Where(a => a.ProviderId.Equals(providerId))
                    .OrderByDescending(a => a.Date)
                    .ToListAsync();

        public async Task UpdateAppointmentStatusAsync(Guid patientId, Guid providerId, AppointmentStatus status)
        {
            var existingAppointment = await FindByCondition(a => a.PatientId.Equals(patientId)
                && a.ProviderId.Equals(providerId), trackChanges: true)
                .SingleOrDefaultAsync();

            if (existingAppointment == null)
            {
                throw new AppointmentNotFound(patientId);
            }

            var providerFullName = $"{existingAppointment.Provider!.FirstName} {existingAppointment.Provider.LastName}";

            existingAppointment.Status = status;
            existingAppointment.UpdatedAt = DateTime.UtcNow;
            existingAppointment.UpdatedBy = providerFullName;

            Update(existingAppointment);
        }

        #endregion

    }
}
