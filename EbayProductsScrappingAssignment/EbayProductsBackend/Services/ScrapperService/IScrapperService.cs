using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Requests.ScrapperServiceRequestDTO;
using EbayProductsBackend.DTOs.Responses.ScrapperServiceResponseDTOs;

namespace EbayProductsBackend.Services.ScrapperService
{
    public interface IScrapperService
    {
        Task<ScrapeProductListResponseDTO> ScrapeProductsList(ScrapeProductListRequestDTO request);
        Task<ScrapeProductListResponseDTO> ScrapeAllProductsDetails();
        Task<ScrapeProductListResponseDTO> ScrapeProductDetail(ScrapeProductDetailRequestDTO ItemNumber);



    }
}