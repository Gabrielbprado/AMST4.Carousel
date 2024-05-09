using System.ComponentModel.DataAnnotations.Schema;

namespace AMST4.Carousel.MVC.Models;

public class Product : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    [ForeignKey("Category")]
    public Guid Category_Id { get; set; }
    public virtual Category Category { get; set; }
}
