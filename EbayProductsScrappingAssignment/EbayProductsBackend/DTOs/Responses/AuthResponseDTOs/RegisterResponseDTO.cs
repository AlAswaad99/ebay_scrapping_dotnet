using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Responses.AuthResponseDTOs
{
    public class RegisterResponseDTO : BaseResponseDTO
    {
        public RegisterResponseDTO(int ResponseCode, string ResponseDescription)
        {
            this.ResponseCode = ResponseCode;
            this.ResponseDescription = ResponseDescription;
        }
    }
}