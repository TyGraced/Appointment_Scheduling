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
    public class PatientService : IPatientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepositoryManager _repository;

        public PatientService(IHttpContextAccessor contextAccessor, IRepositoryManager repository,
            UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
            _userManager = userManager;
        }


        public async Task<ApiResponse<ProviderAvailability>> GetAvailabilityAsync(DateTime dateTime, Guid providerId, bool trackChanges)
        {
            var providerAvailability = await _repository.Availability.GetAvailabilityAsync(dateTime, providerId, trackChanges);

            if (GetAvailabilityAsync == null)
                throw new ProviderNotFound();

            return ApiResponse<ProviderAvailability>.Success(providerAvailability!);
        }

        public async Task<ApiResponse<string>> CancelAppointmentAsync(Guid providerId)
        {
            ApplicationUser user = await PatientCheck();

            await _repository.Appointment.CancelAppointmentAsync(user.Id, providerId);
            await _repository.SaveAsync();

            return ApiResponse<string>.Success("Appointment cancelled successfully");
        }

        public async Task<ApiResponse<string>> BookAppointmentAsync(BookAppointmentRequest request)
        {
            ApplicationUser user = await PatientCheck();

            var availability = await _repository.Availability
                .GetAvailabilityAsync(request.AppointmentStartTime, request.ProviderId, trackChanges:false);

            if (availability == null) throw new TimeSlotNotFound();

            var overlappingAppointment = await _repository.Appointment
                .GetOverlappingAppointmentAsync(request.ProviderId, request.AppointmentStartTime, request.AppointmentEndTime, trackChanges: false);

            if (overlappingAppointment != null) throw new TimeSlotBookedNotFound();

            var fullName = $"{user.FirstName} {user.LastName}";
            DateTime dateStartTime = request.AppointmentStartTime;
            DateTime dateEndTime = request.AppointmentEndTime;
            TimeSpan startTime = dateStartTime.TimeOfDay;
            TimeSpan endTime = dateEndTime.TimeOfDay;

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = user.Id,
                ProviderId = request.ProviderId,
                Date = request.AppointmentStartTime,
                Status = Core.Enums.AppointmentStatus.Pending,
                NIN = user.NIN,
                FullName = fullName,
                Reason = request.Reason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                StartTime = startTime,
                EndTime = endTime
            };

            await _repository.Appointment.BookAppointment(appointment);
            await _repository.SaveAsync();

            return ApiResponse<string>.Success("Appointment booked successfully");
        }

        private async Task<ApplicationUser> PatientCheck()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new UserUnauthorized();

            var isProvider = await _userManager.IsInRoleAsync(user, "PATIENT");

            if (!isProvider) throw new ProviderUnAuthorized();
            return user;
        }

    }
}
