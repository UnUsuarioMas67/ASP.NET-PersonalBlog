using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Models;
using PersonalBlog.Services.Articles;
using PersonalBlog.Utils;

namespace PersonalBlog.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly IArticlesService _articlesService;

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
            return NotFound();

        var article = await _articlesService.GetByIdAsync(id);
        if (article == null)
            return NotFound();

        return View(article);
    }

    public IActionResult Create()
    {
        return View(new ArticleFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Article")] ArticleFormViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            viewModel.Article.PublishDate = DateTime.Now;
            await _articlesService.CreateAsync(viewModel.Article);
        }
        catch (InvalidOperationException e)
        {
            viewModel.Message = "Id already in use";
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

        var viewModel = new ArticleFormViewModel
        {
            Article = article,
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id,
        [Bind("Article")]
        ArticleFormViewModel viewModel)
    {
        var article = viewModel.Article;
        
        if (id != article.Id) return NotFound();
        if (!ModelState.IsValid) return View(viewModel);

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

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Login");
    }
}