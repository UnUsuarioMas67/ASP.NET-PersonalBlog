namespace PersonalBlog.Models;

public class ArticleFormViewModel
{
    public Article Article { get; set; } = new Article();
    public string? Message { get; set; }
}