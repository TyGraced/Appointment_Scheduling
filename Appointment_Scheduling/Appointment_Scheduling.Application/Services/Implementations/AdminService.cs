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
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepositoryManager _repository;

        public AdminService(IHttpContextAccessor contextAccessor, IRepositoryManager repository,
            UserManager<ApplicationUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
            _userManager = userManager;
        }


        public async Task<ApiResponse<IEnumerable<Appointment>>> GetAllAppointmentsAsync(bool trackChanges)
        {
            ApplicationUser user = await AdminCheck();

            var appointments = await _repository.Appointment.GetAllAppointmentsAsync(trackChanges);

            if (!appointments.Any())
                throw new AppointmentsNotFound();

            return ApiResponse<IEnumerable<Appointment>>.Success(appointments);
        }

        public async Task<ApiResponse<IEnumerable<Appointment>>> GetPendingAppointmentsAsync(bool trackChanges)
        {
            ApplicationUser user = await AdminCheck();

            var appointments = await _repository.Appointment.GetPendingAppointmentsAsync(trackChanges);

            if (!appointments.Any())
                throw new AppointmentsNotFound();

            return ApiResponse<IEnumerable<Appointment>>.Success(appointments);
        }

        public async Task<ApiResponse<IEnumerable<Appointment>>> GetCancelledAppointmentsAsync(bool trackChanges)
        {
            ApplicationUser user = await AdminCheck();

            var appointments = await _repository.Appointment.GetCancelledAppointmentsAsync(trackChanges);

            if (!appointments.Any())
                throw new AppointmentsNotFound();

            return ApiResponse<IEnumerable<Appointment>>.Success(appointments);
        }

        public async Task<ApiResponse<IEnumerable<Appointment>>> GetConfirmedAppointmentsAsync(bool trackChanges)
        {
            ApplicationUser user = await AdminCheck();

            var appointments = await _repository.Appointment.GetConfirmedAppointmentsAsync(trackChanges);

            if (!appointments.Any())
                throw new AppointmentsNotFound();

            return ApiResponse<IEnumerable<Appointment>>.Success(appointments);
        }

        public async Task<ApiResponse<IEnumerable<Appointment>>> GetCompletedAppointmentsAsync(bool trackChanges)
        {
            ApplicationUser user = await AdminCheck();

            var appointments = await _repository.Appointment.GetCompletedAppointmentsAsync(trackChanges);

            if (!appointments.Any())
                throw new AppointmentsNotFound();

            return ApiResponse<IEnumerable<Appointment>>.Success(appointments);
        }


        private async Task<ApplicationUser> AdminCheck()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new UserUnauthorized();

            var isAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");

            if (!isAdmin) throw new AdminUnauthorized();
            return user;
        }

    }
}
