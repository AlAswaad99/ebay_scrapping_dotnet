using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Requests
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}