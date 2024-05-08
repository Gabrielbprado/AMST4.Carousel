using AMST4.Carousel.MVC.Data;
using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMST4.Carousel.MVC.Controllers;

public class ProductController : Controller
{
    private readonly DataContext _context;

    public ProductController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult ProductList()
    {
        var products = _context.Product.ToList();
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(Product product)
    {
        product.Id = new Guid();
        await _context.Product.AddAsync(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("ProductList");
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        return View();
    }

}
