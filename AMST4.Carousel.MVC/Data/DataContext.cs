using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMST4.Carousel.MVC.Data;

public class DataContext(DbContextOptions<DataContext> opts) : IdentityDbContext<User>(opts)
{
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<User> User { get; set; }
}
