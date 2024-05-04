using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Responses
{
    public class ListProductResponseDTO : BaseResponseDTO
    {
        public List<ProductDTO> Products { get; set; } = [];

        public ListProductResponseDTO(int ResponseCode, string ResponseDescription, List<Product> products)
        {
            this.ResponseCode = ResponseCode;
            this.ResponseDescription = ResponseDescription;

            Products = products.Select(p =>
            {
                var productDTO = new ProductDTO();
                productDTO.MapProduct(p);
                return productDTO;
            }).ToList();
        }
    }
}