using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Request
{
    public class AddProductRequestDTO
    {
        public required string Title { get; set; }
        public required string Price { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ItemNumber { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public List<string> Images { get; set; } = [];
    }
}