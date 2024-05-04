using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Requests.ScrapperServiceRequestDTO;
using EbayProductsBackend.DTOs.Responses.ScrapperServiceResponseDTOs;
using EbayProductsBackend.Services.ScrapperService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbayProductsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ScrapperController : ControllerBase
    {
        private readonly IScrapperService _scrapperService;

        public ScrapperController(IScrapperService scrapperService)
        {
            _scrapperService = scrapperService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ScrapeProductListResponseDTO>> GetProducts (ScrapeProductListRequestDTO request)
        {
            try
            {
                var data = await _scrapperService.ScrapeProductsList(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving external data: {ex.Message}");
            }
        }

        [HttpPost("product-details")]
        public async Task<ActionResult<ScrapeProductListResponseDTO>> GetProductDetails(ScrapeProductListRequestDTO request)
        {
            try
            {
                var data = await _scrapperService.ScrapeAllProductsDetails();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving external data: {ex.Message}");
            }
        }

        [HttpPost("product-details/{id}")]
        public async Task<ActionResult<ScrapeProductListResponseDTO>> GetProductDetailsByItemNumber(ScrapeProductDetailRequestDTO request)
        {
            try
            {
                var data = await _scrapperService.ScrapeProductDetail(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving external data: {ex.Message}");
            }
        }
    }
}