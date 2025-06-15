using PersonalBlog.Models;

namespace PersonalBlog.Utils;

public static class ArticleFilterer
{
    public static List<Article> FilteredBy(this IEnumerable<Article> articles, string? searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return articles.ToList();
        
        return articles.Where(a => a.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
    }
}