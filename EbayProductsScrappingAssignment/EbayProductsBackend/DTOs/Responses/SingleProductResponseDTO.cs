using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs.Responses
{
    public class SingleProductResponseDTO : BaseResponseDTO
    {
        public ProductDTO? Product { get; set; }

        public SingleProductResponseDTO(int ResponseCode, string ResponseDescription, Product? product)
        {
            this.ResponseCode = ResponseCode;
            this.ResponseDescription = ResponseDescription;
            if (product != null)
            {
                var productDTO = new ProductDTO();
                productDTO.MapProduct(product);
                Product = productDTO;
            }

        }
    }
}