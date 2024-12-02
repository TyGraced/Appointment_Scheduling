using Appointment_Scheduling.Core.DTOs;
using Appointment_Scheduling.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Scheduling.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<IEnumerable<Appointment>>> GetAllAppointmentsAsync(bool trackChanges);
        Task<ApiResponse<IEnumerable<Appointment>>> GetPendingAppointmentsAsync(bool trackChanges);
        Task<ApiResponse<IEnumerable<Appointment>>> GetCancelledAppointmentsAsync(bool trackChanges);
        Task<ApiResponse<IEnumerable<Appointment>>> GetConfirmedAppointmentsAsync(bool trackChanges);
        Task<ApiResponse<IEnumerable<Appointment>>> GetCompletedAppointmentsAsync(bool trackChanges);
    }
}
