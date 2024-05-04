public class Image
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public required string Url { get; set; }
    public required Product Product { get; set; }
}
