using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Request;
using EbayProductsBackend.DTOs.Requests;
using EbayProductsBackend.DTOs.Responses;

namespace EbayProductsBackend.Services.ProductService
{
    public interface IProductService
    {
        Task<ListProductResponseDTO> GetProducts();
        Task<SingleProductResponseDTO> GetSingleProduct(Guid id);
        Task<SingleProductResponseDTO> AddProduct(AddProductRequestDTO product);
        Task<SingleProductResponseDTO> UpdateProduct(Guid id, UpdateProductRequestDTO product);
        Task<SingleProductResponseDTO> DeleteProduct(Guid id);
    }
}