
public class Product
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Price { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public List<Image>? Images { get; set; }
}