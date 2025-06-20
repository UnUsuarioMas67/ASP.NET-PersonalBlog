using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonalBlog.Models;
using PersonalBlog.Services.Articles;
using PersonalBlog.Utils;

namespace PersonalBlog.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly IArticlesService _articlesService;
    private const string NotFoundView = "NotFound";

    public AdminController(IArticlesService articlesService)
    {
        _articlesService = articlesService;
    }

    public async Task<IActionResult> Index(string? searchString)
    {
        var articles = await _articlesService.GetAllAsync();

        var viewModel = new ArticleFilterViewModel
        {
            SearchString = searchString,
            Articles = articles.FilteredBy(searchString),
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Article(string? id)
    {
        if (id == null)
            return View(NotFoundView);

        var article = await _articlesService.GetByIdAsync(id);
        return article == null ? View(NotFoundView) : View(article);
    }

    public IActionResult Create()
    {
        return View(new ArticleFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Article,TagsString")] ArticleFormViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var categories = Request.Form["Categories"];
        if (categories.IsNullOrEmpty())
        {
            viewModel.Message = "You must pick at least one category";
            return View(viewModel);
        }
        
        try
        {
            var article = viewModel.Article;
            article.PublishDate = DateTime.Now;
            article.Categories = categories.ToList()!;
            
            await _articlesService.CreateAsync(viewModel.Article);
        }
        catch (InvalidOperationException e)
        {
            viewModel.Message = "Filename already in use";
            return View(viewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return View(NotFoundView);

        var article = await _articlesService.GetByIdAsync(id);
        if (article == null)
            return View(NotFoundView);

        var viewModel = new ArticleFormViewModel
        {
            Article = article,
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id,
        [Bind("Article,TagsString")] ArticleFormViewModel viewModel)
    {
        var article = viewModel.Article;
        if (id != article.Id) return View(NotFoundView);
        if (!ModelState.IsValid) return View(viewModel);
        
        var categories = Request.Form["Categories"];
        if (categories.IsNullOrEmpty())
        {
            viewModel.Message = "You must pick at least one category";
            return View(viewModel);
        }
        article.Categories = categories.ToList()!;

        var result = await _articlesService.UpdateAsync(article);
        if (!result) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return View(NotFoundView);

        var article = await _articlesService.GetByIdAsync(id);
        if (article == null) return View(NotFoundView);

        return View(article);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var result = await _articlesService.DeleteAsync(id);
        return result ? RedirectToAction(nameof(Index)) : View(NotFoundView);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Login");
    }
}