using AMST4.Carousel.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AMST4.Carousel.MVC.Data;

public class DataContext(DbContextOptions<DataContext> opts) : DbContext(opts)
{
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
}
