using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.Data;
using EbayProductsBackend.DTOs.Requests;
using EbayProductsBackend.DTOs.Requests.AuthDTOs;
using EbayProductsBackend.DTOs.Responses;
using EbayProductsBackend.DTOs.Responses.AuthResponseDTOs;
using EbayProductsBackend.Utils;
using Microsoft.EntityFrameworkCore;

namespace EbayProductsBackend.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

                // Check if user exists and password matches
                if (user != null && AuthUtils.VerifyPassword(model.Password, user.Password))
                {
                    // Generate JWT token
                    var token = AuthUtils.GenerateJwtToken(user, _configuration);
                    return new LoginResponseDTO(200, "User Login Successful", model.Username, token, 3600);
                }

                return  new LoginResponseDTO(400, "Invalid Credentials", null, null, 0);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new LoginResponseDTO(500, ex.Message, null, null, 0);

            }

        }

        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO model)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                    return new RegisterResponseDTO(409, "Username already exists");

                // Hash the password (you can use any secure hashing algorithm)
                var hashedPassword = AuthUtils.HashPassword(model.Password);

                // Create new user
                var user = new User
                {
                    Username = model.Username,
                    Password = hashedPassword
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new RegisterResponseDTO(201, "User Successfully Created");
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new RegisterResponseDTO(500, ex.Message);

            }

        }
    }
}