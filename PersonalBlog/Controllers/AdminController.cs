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

    public async Task<IActionResult> Article(string? id)
    {
        if (id == null)
            return NotFound();

        var article = await _articlesService.GetByIdAsync(id);
        if (article == null)
            return NotFound();

        return View(article);
    }

    public IActionResult Create()
    {
        return View(new CreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Article")] CreateViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            viewModel.Article.PublishDate = DateTime.Now;
            await _articlesService.CreateAsync(viewModel.Article);
        }
        catch (InvalidOperationException e)
        {
            viewModel.IdInUse = true;
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();
        
        var article = await _articlesService.GetByIdAsync(id);
        if (article == null)
            return NotFound();
        
        return View(article);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Content,PublishDate")] Article article)
    {
        if (id != article.Id) return NotFound();
        if (!ModelState.IsValid) return View(article);
        
        var result = await _articlesService.UpdateAsync(article);
        if (!result) return NotFound();
        
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();
        
        var article = await _articlesService.GetByIdAsync(id);
        if (article == null) return NotFound();
        
        return View(article);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var result = await _articlesService.DeleteAsync(id);
        if (!result) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}