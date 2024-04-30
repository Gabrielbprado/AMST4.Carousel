using AMST4.Carousel.MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace AMST4.Carousel.MVC.Controllers;

public class CategoryController : Controller
{
    private readonly DataContext _context;

    public CategoryController(DataContext context)
    {
        _context = context;
    }

    public IActionResult CategoryList()
    {
        var categories = _context.Category.ToList();
        return View(categories);
    }
}
