using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Responses.ScrapperServiceResponseDTOs
{
    public class ScrapeProductListResponseDTO
    {
        public int ResponseCode { get; set; }
        public string ResponseDescription { get; set; } = string.Empty;
    }
}