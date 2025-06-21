using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services.Articles;
using PersonalBlog.Utils;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticlesService _articlesService;
    private const string NotFoundView = "NotFound";

    public HomeController(ILogger<HomeController> logger, IArticlesService articlesService)
    {
        _logger = logger;
        _articlesService = articlesService;
    }

    public async Task<IActionResult> Index(string? searchString, string? category)
    {
        var articles = await _articlesService.GetAllAsync();
        
        var viewModel = new ArticleFilterViewModel
        {
            Articles = articles.FilteredBy(searchString, category),
        };
        
        return View(viewModel);
    }

    public async Task<IActionResult> Article(string? id)
    {
        if (id == null)
            return View(NotFoundView);
        
        var article = await _articlesService.GetByIdAsync(id);
        if (article == null)
            return View(NotFoundView);
        return View(article);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}