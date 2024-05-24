using AMST4.Carousel.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMST4.Carousel.MVC.Controllers;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    public readonly SignInManager<User> _signInManager;
    public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
    {   
        _userManager = userManager;
        _signInManager = signInManager;

    }

    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        var userExists = await IsEmailInUse(user.Email);
        if (userExists)
        {
            ViewBag.ErrorMessage = "Este email Já esta em Uso";
            return View(user);
        }
        var userNameExists = await IsUserNameInUse(user.UserName);
        if (!userNameExists)
        {
            ViewBag.ErrorMessage = "Este Nome de Usuário Já esta em Uso";
            return View(user);
        }

        user.Id = Guid.NewGuid().ToString();
        var result = await _userManager.CreateAsync(user, user.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("ProductList", "Product");
        }

        ViewBag.ErrorMessage = "Aconteceu um Erro ao Tentar Criar sua Conta";
        return View(user);
    }


    [HttpGet]
    public IActionResult LoginUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(User user)
    {
        var UserExit = await IsEmailInUse(user.Email);
        if (UserExit)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("ProductList", "Product");
            }
        } else
        {
            ViewBag.ErrorMessage = "Não encontramos uma conta associada a este endereço de e-mail";
            return View(user);
        }
        return RedirectToAction("LoginUser");
    }
 

    [HttpGet]
    public async Task<IActionResult> UserDetails(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> UserEdit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UserEdit(User user)
    {
        var userEdit = await _userManager.FindByIdAsync(user.Id);
        userEdit.Adress = user.Adress;
        userEdit.City = user.City;
        userEdit.State = user.State;
        await _userManager.UpdateAsync(userEdit);
        return RedirectToAction("UserList");
    }

    private async Task<bool> IsEmailInUse(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }

    private async Task<bool> IsUserNameInUse(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user != null;
    }

}
