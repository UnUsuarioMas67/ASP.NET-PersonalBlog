using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services;
using PersonalBlog.Services.Articles;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticlesService _articlesService;

    public HomeController(ILogger<HomeController> logger, IArticlesService articlesService)
    {
        _logger = logger;
        _articlesService = articlesService;
    }

    public async Task<IActionResult> Index()
    {
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}