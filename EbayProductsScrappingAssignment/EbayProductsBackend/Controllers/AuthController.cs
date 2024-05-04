using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Requests;
using EbayProductsBackend.DTOs.Requests.AuthDTOs;
using EbayProductsBackend.DTOs.Responses;
using EbayProductsBackend.DTOs.Responses.AuthResponseDTOs;
using EbayProductsBackend.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace EbayProductsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDTO>> Register(RegisterRequestDTO request)
        {
            var res = await _authService.Register(request);

            if (res.ResponseCode != 201) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            var res = await _authService.Login(request);

            if (res.ResponseCode != 200) return BadRequest(res);
            return Ok(res);
        }
    }
}