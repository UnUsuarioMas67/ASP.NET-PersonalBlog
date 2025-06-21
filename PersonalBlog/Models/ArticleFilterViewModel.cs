using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalBlog.Models;

public class ArticleFilterViewModel
{
    public string? SearchString { get; set; }
    public string? Category { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>();
    public SelectList Categories { get; set; } = new SelectList(Article.AvailableCategories);
}