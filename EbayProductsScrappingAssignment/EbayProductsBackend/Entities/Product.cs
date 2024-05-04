
public class Product
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ItemNumber { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public List<Image> Images { get; set; } = [];

    public void MapProduct(Product product)
    {
        Description = product.Description;
        ItemNumber = product.ItemNumber;
        Title = product.Title;
        Images = product.Images;
        Price = product.Price;
        VideoUrl = product.VideoUrl;
    }
}