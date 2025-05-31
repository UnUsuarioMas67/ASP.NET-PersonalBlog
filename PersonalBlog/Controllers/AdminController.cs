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
        if (login.Failed) return View(login);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!_accountsService.IsLoggedIn)
            return RedirectToAction(nameof(Login));
        
        ViewBag.Username = _accountsService.LoggedInAccount!.Username;
        return View(await _articlesService.GetAllAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Article(int? id)
    {
        if (!_accountsService.IsLoggedIn)
            return RedirectToAction(nameof(Login));
        ViewBag.Username = _accountsService.LoggedInAccount!.Username;
        
        if (id == null)
            return NotFound();
        
        var article = await _articlesService.GetByIdAsync(id.Value);
        if (article == null)
            return NotFound();
        
        return View(article);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!_accountsService.IsLoggedIn)
            return RedirectToAction(nameof(Login));
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Content,PublishDate")] Article article)
    {
        if (!_accountsService.IsLoggedIn)
            return RedirectToAction(nameof(Login));

        if (!ModelState.IsValid) return View(article);
        
        article.PublishDate = DateTime.Now;
        await _articlesService.CreateAsync(article);

        return RedirectToAction(nameof(Index));
    }
}