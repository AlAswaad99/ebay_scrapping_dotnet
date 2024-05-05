using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Requests.ScrapperServiceRequestDTO;
using EbayProductsBackend.DTOs.Responses.ScrapperServiceResponseDTOs;

namespace EbayProductsBackend.Services.ScrapperService
{
    public class ScrapperService : IScrapperService
    {

        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;


        public ScrapperService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("ScrapperBaseUrl"));
        }

        public async Task<ScrapeProductListResponseDTO> ScrapeAllProductsDetails()
        {

            // Prepare request content using JSON payload
            var requestContent = new StringContent("", Encoding.UTF8, "application/json");

            Debug.Print(requestContent.ToString());


            // Make a POST request to the external microservice endpoint
            var response = await _httpClient.PostAsync("scrape-all-products-detail", requestContent);

            // Check if the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the response content to ExternalResponse object
            var externalResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ScrapeProductListResponseDTO>(responseContent);

            return externalResponse;
        }

        public async Task<ScrapeProductListResponseDTO> ScrapeProductDetail(ScrapeProductDetailRequestDTO request)
        {
            // Serialize the request DTO to JSON
            var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            Debug.Print(jsonRequest.ToString());

            // Prepare request content using JSON payload
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            Debug.Print(requestContent.ToString());


            // Make a POST request to the external microservice endpoint
            var response = await _httpClient.PostAsync("scrape-product-detail", requestContent);

            // Check if the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the response content to ExternalResponse object
            var externalResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ScrapeProductListResponseDTO>(responseContent);

            return externalResponse;
        }

        public async Task<ScrapeProductListResponseDTO> ScrapeProductsList(ScrapeProductListRequestDTO request)
        {
            // Serialize the request DTO to JSON
            var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(request);


            // Prepare request content using JSON payload
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            Debug.Print(requestContent.ToString());


            // Make a POST request to the external microservice endpoint
            var response = await _httpClient.PostAsync("scrape-products-list", requestContent);

            // Check if the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the response content to ExternalResponse object
            var externalResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ScrapeProductListResponseDTO>(responseContent);

            return externalResponse;
        }
    }
}