namespace PersonalBlog.Models;

public class ArticleFilterViewModel
{
    public string? SearchString { get; set; }
    public List<Article> Articles { get; set; }
}