using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PersonalBlogContext _context;

    public HomeController(ILogger<HomeController> logger, PersonalBlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Articles.ToListAsync());
    }

    public async Task<IActionResult> Article(int? id)
    {
        if (id == null)
            return NotFound();
        
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
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