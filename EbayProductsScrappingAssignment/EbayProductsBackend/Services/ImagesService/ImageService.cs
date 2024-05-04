using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EbayProductsBackend.Data;

namespace EbayProductsBackend.Services.ImagesService
{
    public class ImageService : IImageService
    {
        private readonly DataContext _context;

        public ImageService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Image>?> AddOrUpdateListOfImages(List<string> images, Guid Id)
        {
            try
            {
                Debug.Print("inhere:");

                (List<string> exists, List<string> added, List<string> removed) = FilterImageUrls(images, Id);

                // PrGuid existing image URLs
                Debug.Print("Existing image URLs:");
                foreach (var url in exists)
                {
                    Debug.Print(url);
                }

                // PrGuid added image URLs
                Debug.Print("\nAdded image URLs:");
                foreach (var url in added)
                {
                    Debug.Print(url);
                }

                // PrGuid removed image URLs
                Debug.Print("\nRemoved image URLs:");
                foreach (var url in removed)
                {
                    Debug.Print(url);
                }


                if (added != null && added.Count > 0)
                    await AddImages(added, Id);

                if (removed != null && removed.Count > 0)
                    RemoveImages(removed, Id);

                return [.. _context.Images.Where(i => i.ProductId == Id)];

            }
            catch (System.Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return null;
            }


        }

        protected async Task AddImages(List<string> imageUrlList, Guid Id)
        {
            if (imageUrlList != null && imageUrlList.Count > 0)
            {

                // foreach (var item in imageUrlList)
                // {
                //     Image img = new()
                //     {
                //         ProductId = Id,
                //         Url = item
                //     };
                //     _context.Images.Add(img);
                // }
                var imagesList = imageUrlList.Select(i => new Image { ProductId = Id, Url = i }).ToList();
                await _context.Images.AddRangeAsync(imagesList);
                await _context.SaveChangesAsync();
                // return [.. _context.Images.Where(i => i.ProductId == Id)];

            }
            // else return [];
        }

        protected async void RemoveImages(List<string> imageUrlList, Guid Id)
        {

            if (imageUrlList != null && imageUrlList.Count > 0)
            {
                var imagesToRemove = _context.Images.Where(i => imageUrlList.Contains(i.Url) && i.ProductId == Id).ToList();

                // var imagesList = imageUrlList.Select(i => new Image { ProductId = Id, Url = i }).ToList();
                _context.Images.RemoveRange(imagesToRemove);
                await _context.SaveChangesAsync();
            }
        }
        protected (List<string> exists, List<string> added, List<string> removed) FilterImageUrls(List<string> imageUrlList, Guid Id)
        {
            var existingImageUrls = _context.Images.Where(i => i.ProductId == Id).Select(i => i.Url).ToList();

            var existingUrlsInList = imageUrlList.Intersect(existingImageUrls).ToList();

            var urlsToAdd = imageUrlList.Except(existingImageUrls).ToList();

            var urlsToRemove = existingImageUrls.Except(imageUrlList).ToList();

            Debug.Print("returning:");


            return (exists: existingUrlsInList, added: urlsToAdd, removed: urlsToRemove);
        }
    }
}