using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services.Accounts;
using PersonalBlog.Services.Articles;

namespace PersonalBlog.Controllers;

public class AdminController : Controller
{
    private readonly IArticlesService _articlesService;
    private readonly IAccountsService _accountsService;

    public AdminController(IArticlesService articlesService, IAccountsService accountsService)
    {
        _articlesService = articlesService;
        _accountsService = accountsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (!_accountsService.IsLoggedIn)
            return RedirectToAction(nameof(Login));
        
        return View();
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel login)
    {
        if (!ModelState.IsValid) 
            return View(login);
        
        login.Failed = !_accountsService.Login(login.Username, login.Password);
        if (login.Failed)
        {
            return View(login);
        }
        
        return RedirectToAction(nameof(Index));
    }
}