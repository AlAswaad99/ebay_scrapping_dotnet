using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.Data;
using EbayProductsBackend.DTOs.Request;
using EbayProductsBackend.DTOs.Requests;
using EbayProductsBackend.DTOs.Responses;
using EbayProductsBackend.Services.ImagesService;
using Microsoft.EntityFrameworkCore;

namespace EbayProductsBackend.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IImageService _imageService;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public ProductService(DataContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public async Task<SingleProductResponseDTO> AddProduct(AddProductRequestDTO product)
        {
            try
            {
                if (product is null) return new SingleProductResponseDTO(400, "Invalid Product Entry", null);
                Product newProduct = new()
                {
                    Description = product.Description,
                    ItemNumber = product.ItemNumber,
                    Title = product.Title,
                    Price = product.Price,
                    VideoUrl = product.VideoUrl,
                    Images = []
                };
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                var images = await _imageService.AddOrUpdateListOfImages(product.Images, newProduct.Id);
                if (images != null && images.Count > 0)
                {
                    newProduct.Images = images;
                    await _context.SaveChangesAsync();
                }
                return new SingleProductResponseDTO(201, "Successfully Added Product", newProduct);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new SingleProductResponseDTO(500, ex.Message, null);
            }
        }

        public async Task<SingleProductResponseDTO> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product is null)
                    return new SingleProductResponseDTO(404, "Product Not Found", null); ;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                await _context.Entry(product)
                                .Collection(p => p.Images)
                                .LoadAsync();
                return new SingleProductResponseDTO(200, "Successfully Deleted Product", product);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new SingleProductResponseDTO(500, ex.Message, null);
            }
        }

        public async Task<ListProductResponseDTO> GetProducts(int page, int pageSize)
        {
            try
            {
                var skip = (page - 1) * pageSize;
                var products = await _context.Products.Skip(skip).Take(pageSize).Include(p => p.Images).ToListAsync();
                Debug.Print(products.ToString());

                return new ListProductResponseDTO(200, "Successfully Retreived Products", products);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new ListProductResponseDTO(500, ex.Message, []);
            }
        }

        public async Task<SingleProductResponseDTO> GetSingleProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product is null)
                    return new SingleProductResponseDTO(404, "Product Not Found", null); ;

                await _context.Entry(product)
                .Collection(p => p.Images)
                .LoadAsync();
                return new SingleProductResponseDTO(200, "Successfully Retreived Product", product);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new SingleProductResponseDTO(500, ex.Message, null);
            }
        }

        public async Task<SingleProductResponseDTO> UpdateProduct(Guid id, UpdateProductRequestDTO product)
        {
            try
            {
                if (product is null) return new SingleProductResponseDTO(400, "Invalid Product Entry", null);

                var oldProduct = await _context.Products.FindAsync(id);
                if (oldProduct is null)
                    return new SingleProductResponseDTO(400, "Product Not Found", null);

                // Product newProduct = new()
                // {
                //     Description = product.Description,
                //     Title = product.Title,
                //     Price = product.Price,
                //     VideoUrl = product.VideoUrl,
                //     Images = []
                // };
                // oldProduct.MapProduct(product);
                oldProduct.Title = product.Title;
                oldProduct.Description = product.Description;
                oldProduct.Price = product.Price;
                oldProduct.VideoUrl = product.VideoUrl;
                oldProduct.ItemNumber = product.ItemNumber;

                var images = await _imageService.AddOrUpdateListOfImages(product.Images, id);
                if (images != null) oldProduct.Images = images;
                // await _context.SaveChangesAsync();

                await _context.SaveChangesAsync();

                await _context.Entry(oldProduct)
                                .Collection(p => p.Images)
                                .LoadAsync();
                return new SingleProductResponseDTO(200, "Successfully Updated Product", oldProduct);
            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
                return new SingleProductResponseDTO(500, ex.Message, null);
            }
        }
    }
}