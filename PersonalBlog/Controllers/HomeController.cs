using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticleService _articleService;

    public HomeController(ILogger<HomeController> logger, IArticleService articleService)
    {
        _logger = logger;
        _articleService = articleService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _articleService.GetAllAsync());
    }

    public async Task<IActionResult> Article(int? id)
    {
        if (id == null)
            return NotFound();
        
        var article = await _articleService.GetByIdAsync(id.Value);
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