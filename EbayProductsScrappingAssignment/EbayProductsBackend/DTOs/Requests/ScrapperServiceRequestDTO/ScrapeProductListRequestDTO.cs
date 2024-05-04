using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Requests.ScrapperServiceRequestDTO
{
    public class ScrapeProductListRequestDTO
    {
        public string Url { get; set; } = string.Empty;
    }
}