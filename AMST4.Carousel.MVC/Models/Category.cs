namespace AMST4.Carousel.MVC.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
