using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayProductsBackend.Services.ImagesService
{
    public interface IImageService
    {
        Task<List<Image>?> AddOrUpdateListOfImages(List<string> images, Guid Id);
    }
}