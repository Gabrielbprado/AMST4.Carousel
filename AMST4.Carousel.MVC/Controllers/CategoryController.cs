﻿using AMST4.Carousel.MVC.Data;
using AMST4.Carousel.MVC.Helper;
using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet]
    public IActionResult CategoryDetails(Guid id)
    {
        var category = _context.Category.FirstOrDefault(x => x.Id == id);
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(Category category, IFormFile image)
    {
        if (image != null)
        {
            var folderName = "Category";
            category.ImageUrl = await ImageHelper.SaveImageAsync(image, folderName);
        }

        category.Id = Guid.NewGuid();
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
    public async Task<IActionResult> EditCategory(Category category, IFormFile image)
    {
        if (image != null)
        {
            var folderName = "Category";
            category.ImageUrl = await ImageHelper.SaveImageAsync(image, folderName);
        }

        _context.Category.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public IActionResult EditCategory(Guid id)
    {
        var category = _context.Category.FirstOrDefault(x => x.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategoryWarning(Guid id)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var hasProducts = await _context.Product.AnyAsync(p => p.Category_Id == category.Id);
        if (hasProducts)
        {
            ViewBag.HasProducts = true;
            ViewBag.CategoryName = category.Description;
            return View("DeleteCategoryWarning", category);
        } else
        {
            await DeleteCategoryConfirmed(id);
        }

        return RedirectToAction("CategoryList");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        if (id == Guid.Empty)
        {
            return NotFound();
        }

        var category = await _context.Category.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategoryConfirmed(Guid id)
    {
        var category = await _context.Category.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Product.RemoveRange(category.Products);

        if (!string.IsNullOrEmpty(category.ImageUrl))
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.ImageUrl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("CategoryList");
    }
}