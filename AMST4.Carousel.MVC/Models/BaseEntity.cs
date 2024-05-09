using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AMST4.Carousel.MVC.Models;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedBy { get; set; } = DateTime.Now;
}
