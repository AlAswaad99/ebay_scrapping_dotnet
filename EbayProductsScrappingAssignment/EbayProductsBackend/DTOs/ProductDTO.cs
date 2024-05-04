using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public List<ImageDTO> Images { get; set; } = [];

        public void MapProduct(Product product)
        {
            Description = product.Description;
            Title = product.Title;
            Images = product.Images.Select(i => new ImageDTO(i.Id, i.Url)).ToList();
            Price = product.Price;
            VideoUrl = product.VideoUrl;
            Id = product.Id;
        }
    }
}