using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs
{
    public class ImageDTO
    {
        public Guid ImageId { get; set; }
        public string Url { get; set; } = string.Empty;

        public ImageDTO(Guid Id, string url)
        {
            ImageId = Id;
            Url = url;
        }
    }
}