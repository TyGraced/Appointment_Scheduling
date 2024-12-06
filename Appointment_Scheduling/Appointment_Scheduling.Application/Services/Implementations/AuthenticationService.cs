using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.DTOs;
using Appointment_Scheduling.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Appointment_Scheduling.Application.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }


        public async Task<ApiResponse<string>> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                DateOfBirth = request.DateOfBirth,
                NIN = request.NIN,
                ProviderName = request.ProviderName
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                Console.WriteLine($"User registration failed: {errors}");
            return ApiResponse<string>.Failure($"User registration failed: {errors}");
            }

            if (request.IsPatient)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Patient"); 
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    Console.WriteLine($"Failed to assign Patient role: {errors}");
                    return ApiResponse<string>.Failure($"Failed to assign Patient role.");
                }
            }
            if (request.IsProvider)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Provider"); 
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    Console.WriteLine($"Failed to assign Provider role: {errors}");
                    return ApiResponse<string>.Failure($"Failed to assign Provider role");
                }
            }

            var emailMessage = $"Your Appointment Scheduling profile has been successfully created.";
            await _emailService.SendEmailAsync(request.Email, "Welcome To Appointment Scheduling", emailMessage);
            return ApiResponse<string>.Success("Registration Successful");
        }


        public async Task<ApiResponse<AuthenticationResult>> LoginAsync(LoginRequest request)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return ApiResponse<AuthenticationResult>.Failure("Invalid credentials");

            // Check password
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return ApiResponse<AuthenticationResult>.Failure("Invalid credentials.");

            // Generate Jwt Token
            var token = await _tokenService.GenerateTokenAsync(user);

            var authResult = new AuthenticationResult
            {
                Token = token,
                Email = user.Email,
                UserId = user.Id.ToString()
            };

            DateTime currentDate = DateTime.UtcNow.AddHours(1);
            var date = currentDate.ToString("D");
            var time = currentDate.ToString("T");
            var emailMessage = $"You have successfully logged into your Appointment Scheduling profile on {date} at {time}.";
            await _emailService.SendEmailAsync(request.Email, "Login Alert", emailMessage);

            // Return the token in the response
            return ApiResponse<AuthenticationResult>.Success(authResult, "Login Successful");
        }


        public async Task<ApiResponse<string>> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return ApiResponse<string>.Success("Logged out successfully.");
        }

    }
}
