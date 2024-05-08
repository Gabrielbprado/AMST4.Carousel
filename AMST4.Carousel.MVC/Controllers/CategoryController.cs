using AMST4.Carousel.MVC.Data;
using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMST4.Carousel.MVC.Controllers;

public class CategoryController : Controller
{
    private readonly DataContext _context;

    public CategoryController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult CategoryList()
    {
        var categories = _context.Category.ToList();
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(Category category)
    {
        category.Id = new Guid();
        await _context.Category.AddAsync(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
         _context.Category.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public IActionResult EditCategory(Guid id)
    {

        
            Category? category = _context.Category.FirstOrDefault(x => x.Id == id);

            if (category == null) { return NotFound(); }
            Console.WriteLine(category.Description);
            Console.WriteLine(category.Id);

        return View(category);
        
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(Category category)
    {
        if(category == null)
        {
            return NotFound();
        }
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        if(id == Guid.Empty)
        {
            return NotFound();
        }
        var category = await _context.Category.FindAsync(id);
        return View(category);
    }
}
