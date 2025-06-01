using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using PersonalBlog.Filters;
using PersonalBlog.Models;
using PersonalBlog.Services.Accounts;
using PersonalBlog.Services.Articles;

namespace PersonalBlog.Controllers;

[ServiceFilter(typeof(EnsureLoggedIn))]
public class AdminController : Controller
{
    private readonly IArticlesService _articlesService;
    private readonly IAccountsService _accountsService;

    public AdminController(IArticlesService articlesService, IAccountsService accountsService)
    {
        _articlesService = articlesService;
        _accountsService = accountsService;
    }

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

    public async Task<IActionResult> Index()
    {
        ViewBag.Username = _accountsService.LoggedInAccount!.Username;
        return View(await _articlesService.GetAllAsync());
    }

    public async Task<IActionResult> Article(int? id)
    {
        if (id == null)
            return NotFound();

        var article = await _articlesService.GetByIdAsync(id.Value);
        if (article == null)
            return NotFound();

        return View(article);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Content")] Article article)
    {
        if (!ModelState.IsValid) return View(article);

        article.PublishDate = DateTime.Now;
        await _articlesService.CreateAsync(article);

        return RedirectToAction(nameof(Index));
    }
}