using Microsoft.AspNetCore.Identity;

namespace Appointment_Scheduling.Infrastructure.Data.SeedData
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            var roles = new[] { "Admin", "Patient", "Provider" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
                }
            }
        }
    }
}
