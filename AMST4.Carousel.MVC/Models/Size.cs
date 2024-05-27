namespace AMST4.Carousel.MVC.Models;

public class Size : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
