using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.DTOs;
using Appointment_Scheduling.Core.Exceptions;
using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Appointment_Scheduling.Application.Services.Implementations
{
    public class ProviderService : IProviderService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepositoryManager _repository;

        public ProviderService(IHttpContextAccessor contextAccessor, IRepositoryManager repository,
            UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> SetAvailabilityAsync(SetAvailabilityRequest request)
        {
            ApplicationUser user = await ProviderCheck();

            foreach (var availability in request.DailyAvailability!)
            {
                if (availability.StartTime >= availability.EndTime)
                    throw new TimeMismatchException();

                DateTime dateStartTime = availability.StartTime;
                DateTime dateEndTime = availability.EndTime;
                TimeSpan startTime = dateStartTime.TimeOfDay;
                TimeSpan endTime = dateEndTime.TimeOfDay;

                var providerAvailability = new ProviderAvailability
                {
                    Id = Guid.NewGuid(),
                    ProviderId = user.Id,
                    DayOfWeek = availability.DayOfWeek,
                    StartTime = startTime,
                    EndTime = endTime
                };

                await _repository.Availability.SetAvailabilityAsync(providerAvailability);
                await _repository.SaveAsync();
            }

            return ApiResponse<string>.Success("Provider availability set successfully");

        }

        public async Task<ApiResponse<Appointment>> GetAppointmentAsync(Guid patientId, bool trackChanges)
        {
            ApplicationUser user = await ProviderCheck();

            var appointment = await _repository.Appointment.GetAppointmentAsync(user.Id, patientId, trackChanges);

            if (appointment== null)
                throw new AppointmentNotFound(patientId); 

            return ApiResponse<Appointment>.Success(appointment);
        }

        public async Task<ApiResponse<IEnumerable<Appointment>>> GetAppointmentsAsync(bool trackChanges)
        {
            ApplicationUser user = await ProviderCheck();

            var appointments = await _repository.Appointment.GetAppointmentsAsync(user.Id, trackChanges);

            if (!appointments.Any())
                throw new AppointmentsNotFound();

            return ApiResponse<IEnumerable<Appointment>>.Success(appointments);
        }

        public async Task<ApiResponse<string>> UpdateAppointmentStatus(UpdateProviderAppointmentStatusRequest request)
        {
            ApplicationUser user = await ProviderCheck();

            await _repository.Appointment.UpdateAppointmentStatusAsync(user.Id, request.PatientId, request.Status);
            await _repository.SaveAsync();

            return ApiResponse<string>.Success("Appointment status updated successfully");
        }

        private async Task<ApplicationUser> ProviderCheck()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new UserUnauthorized();

            var isProvider = await _userManager.IsInRoleAsync(user, "PROVIDER");

            if (!isProvider) throw new ProviderUnAuthorized();
            return user;
        }

    }
}
