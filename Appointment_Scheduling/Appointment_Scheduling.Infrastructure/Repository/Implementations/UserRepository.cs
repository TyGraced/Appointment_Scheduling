using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Appointment_Scheduling.Infrastructure.Repository.Implementations
{
    public class UserRepository :RepositoryBase<ApplicationUser>, IUserRepository
    {
        private readonly IRepositoryBase<IdentityUserRole<Guid>> _userRoleRepository;
        private readonly IRepositoryBase<IdentityRole> _roleRepository;
        public UserRepository(ApplicationDbContext context,
                                 IRepositoryBase<IdentityUserRole<Guid>> userRoleRepository,
                                 IRepositoryBase<IdentityRole> roleRepository)
               : base(context)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetProvidersAsync(bool trackChanges)
        {
            var roleName = "Provider";

            // Get the RoleId for the "Provider" role
            var providerRole = await _roleRepository
                .FindByCondition(r => r.Name == roleName, false)
                .FirstOrDefaultAsync();

            if (providerRole == null)
                return Enumerable.Empty<ApplicationUser>();

            // Get UserIds for users in the "Provider" role and materialize the list
            var providerUserIds = await _userRoleRepository
                .FindByCondition(ur => ur.RoleId.ToString() == providerRole.Id, false)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Retrieve the ApplicationUsers who match these UserIds
            return await FindByCondition(
                user => providerUserIds.Contains(user.Id), 
                trackChanges
            )
            .OrderByDescending(user => user.FirstName) 
            .ToListAsync();
        }


        public async Task<IEnumerable<ApplicationUser>> GetPatientsAsync(bool trackChanges)
        {
            var roleName = "Patient";

            // Get the RoleId for the "Patient" role
            var patientRole = await _roleRepository
                .FindByCondition(r => r.Name == roleName, false)
                .FirstOrDefaultAsync();

            if (patientRole == null)
                return Enumerable.Empty<ApplicationUser>();

            // Get UserIds for users in the "Patient" role and materialize the list
            var patientUserIds = await _userRoleRepository
                .FindByCondition(ur => ur.RoleId.ToString() == patientRole.Id, false)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Retrieve the ApplicationUsers who match these UserIds
            return await FindByCondition(
                user => patientUserIds.Contains(user.Id),
                trackChanges
            )
            .OrderByDescending(user => user.FirstName)
            .ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(u => u.Id == userId, trackChanges)
                .SingleOrDefaultAsync();
    }
}
