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

    [HttpGet]
    public async Task<IActionResult> ProductDetails(Guid id)
    {
        Product product = await _context.Product.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
        return View(product);
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        var hasCategories = _context.Category.Any();
        ViewBag.HasCategories = hasCategories;
        ViewBag.CategoryList = new SelectList(_context.Category, "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct(Product product, IFormFile image)
    {


        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        var UrlImage = Path.Combine("images", "Product", fileName);


        product.Id = Guid.NewGuid();
        product.ImageUrl = UrlImage;
        _context.Add(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ProductList));



    }
    [HttpGet]
    public async Task<IActionResult> EditProduct(Guid id)
    {
        Product product = await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
        ViewBag.CategoryList = new SelectList(_context.Category, "Id", "Name");
        return View(product);
    }
    [HttpPost]

    public async Task<IActionResult> EditProduct(Guid id, Product product, IFormFile image)
    {
        if (id != product.Id)
        {
            return NotFound();
        }



        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }
        var UrlImage = Path.Combine("images", "Product", fileName);

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", product.ImageUrl);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }

        product.ImageUrl = UrlImage;


        _context.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ProductList));


    }





    [HttpPost]

    public async Task<IActionResult> DeleteProduct(Product product)
    {


        if (product == null)
        {
            return NotFound();
        }
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ProductList));
    }

    [HttpGet]


    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _context.Product.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id); 
        return View(product);
    }


    [HttpGet]
    public async Task<IActionResult> ProductListByCategory(Guid? categoryId)
    {
        if (categoryId == null)
        {
            return View(new List<Product>());
        }

        var products = await _context.Product
                                    .Include(p => p.Category)
                                    .Where(p => p.Category_Id == categoryId)
                                    .ToListAsync();

        ViewBag.CategoryList = new SelectList(_context.Category, "Id", "Name");
        ViewBag.SelectedCategory = categoryId;

        return View(products);
    }

   

}
