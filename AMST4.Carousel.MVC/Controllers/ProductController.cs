using AMST4.Carousel.MVC.Data;
using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AMST4.Carousel.MVC.Controllers;

public class ProductController : Controller
{
    private readonly DataContext _context;

    public ProductController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ProductList()
    {
        List<Product> products = await _context.Product.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    [HttpPost]
   
   
    public async Task<IActionResult> AddProduct(Product product)
    {
        
       var category = await _context.Category.FindAsync(product.Category_Id);
       product.Id = Guid.NewGuid();
       _context.Add(product);
       await _context.SaveChangesAsync();
       return RedirectToAction(nameof(ProductList)); 
    }


    [HttpGet]
    public IActionResult AddProduct()
    {
        ViewBag.CategoryList = new SelectList(_context.Category, "Id", "Description");
        return View();
    }

}
