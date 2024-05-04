using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Responses.AuthResponseDTOs
{
    public class LoginResponseDTO : BaseResponseDTO
    {
        public string? Username { get; set; }
        public string? Token { get; set; }
        public int? ExpiresIn { get; set; }

        public LoginResponseDTO(int ResponseCode, string ResponseDescription, string? Username, string? Token, int? ExpiresIn) 
        {
            this.ResponseCode = ResponseCode;
            this.ResponseDescription = ResponseDescription;
            this.Username = Username;
            this.Token = Token;
            this.ExpiresIn = ExpiresIn;
        }
    }
}