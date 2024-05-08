namespace AMST4.Carousel.MVC.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedBy { get; set; } = DateTime.Now;
}
