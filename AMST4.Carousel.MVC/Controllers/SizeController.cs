using AMST4.Carousel.MVC.Data;
using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMST4.Carousel.MVC.Controllers;

public class SizeController : Controller
{
    private readonly DataContext _context;

    public SizeController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> SizeList()
    {
        return View(await _context.Size.ToListAsync());
    }

    public async Task<IActionResult> SizeDetails(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var size = await _context.Size
            .FirstOrDefaultAsync(m => m.Id == id);
        if (size == null)
        {
            return NotFound();
        }

        return View(size);
    }

    public IActionResult AddSize()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSize(Size size)
    {

        size.Id = Guid.NewGuid();
        _context.Add(size);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(SizeList));

    }

    public async Task<IActionResult> EditSize(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var size = await _context.Size.FindAsync(id);
        if (size == null)
        {
            return NotFound();
        }
        return View(size);
    }

    [HttpPost]
    public async Task<IActionResult> EditSize(Guid id, Size size)
    {
        if (id != size.Id)
        {
            return NotFound();
        }

        _context.Update(size);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(SizeList));

    }
    [HttpGet]

    public async Task<IActionResult> DeleteSize(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var size = await _context.Size
            .FirstOrDefaultAsync(m => m.Id == id);
        if (size == null)
        {
            return NotFound();
        }

        return View(size);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSizeConfirmed(Guid id)
    {
        var size = await _context.Size.FindAsync(id);
        if (size != null)
        {
            _context.Size.Remove(size);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(SizeList));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSizeWarning(Guid id)
    {
        var size = await _context.Size.FindAsync(id);
        if (size == null)
        {
            return NotFound();
        }

        var hasProducts = await _context.Product.AnyAsync(p => p.Size_Id == size.Id);
        if (hasProducts)
        {
            ViewBag.HasProducts = true;
            ViewBag.SizeDescription = size.Description;
            return View("DeleteSizeWarning", size);
        }
        else
        {
            await DeleteSizeConfirmed(id);
        }

        return RedirectToAction("SizeList");
    }

    private bool SizeExists(Guid id)
    {
        return _context.Size.Any(e => e.Id == id);
    }
}
