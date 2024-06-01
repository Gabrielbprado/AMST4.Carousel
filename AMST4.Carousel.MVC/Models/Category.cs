namespace AMST4.Carousel.MVC.Models;

public class Category : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
