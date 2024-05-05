using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.DTOs.Request;
using EbayProductsBackend.DTOs.Requests;
using EbayProductsBackend.DTOs.Responses;
using EbayProductsBackend.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbayProductsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ListProductResponseDTO>> GetAllProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProducts(page, pageSize);

            if (products.ResponseCode != 200) return BadRequest(products);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SingleProductResponseDTO>> GetProduct(Guid id)
        {
            var product = await _productService.GetSingleProduct(id);

            if (product.ResponseCode != 200) return BadRequest(product);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<SingleProductResponseDTO>> AddProduct(AddProductRequestDTO product)
        {
            var newProduct = await _productService.AddProduct(product);

            if (newProduct.ResponseCode != 201) return BadRequest(newProduct);
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SingleProductResponseDTO>> UpdateProduct(Guid id, UpdateProductRequestDTO product)
        {
            var updatedProduct = await _productService.UpdateProduct(id, product);

            if (updatedProduct.ResponseCode != 200) return BadRequest(updatedProduct);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SingleProductResponseDTO>> DeleteProduct(Guid id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);

            if (deletedProduct.ResponseCode != 200) return BadRequest(deletedProduct);
            return Ok(deletedProduct);
        }
    }
}