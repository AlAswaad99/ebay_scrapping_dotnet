using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Requests;
using EbayProductsBackend.DTOs.Requests.AuthDTOs;
using EbayProductsBackend.DTOs.Responses;
using EbayProductsBackend.DTOs.Responses.AuthResponseDTOs;

namespace EbayProductsBackend.Services.AuthService
{
    public interface IAuthService
    {
        Task<RegisterResponseDTO> Register(RegisterRequestDTO model);
        Task<LoginResponseDTO> Login(LoginRequestDTO model);
    }
}