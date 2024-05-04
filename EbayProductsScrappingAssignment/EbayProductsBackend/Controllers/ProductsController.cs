using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EbayProductsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = new List<Product> {
                new() {
                    Id=1,
                    Title="Mukera",
                    Description= "Mukera Description"
                }
            };

            return Ok(products);
        }
    }
}