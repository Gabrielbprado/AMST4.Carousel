using System.ComponentModel.DataAnnotations.Schema;

namespace AMST4.Carousel.MVC.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    [ForeignKey("Category")]
    public Guid Category_Id { get; set; }
    public virtual Category Category { get; set; } 
    [ForeignKey("Size")]
    public Guid Size_Id { get; set; }
    public virtual Size Size { get; set; }
    public decimal Price { get; set; }
    public double Stock { get; set; }
    

}
