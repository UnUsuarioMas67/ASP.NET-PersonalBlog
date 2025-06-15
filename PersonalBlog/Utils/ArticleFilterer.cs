using PersonalBlog.Models;

namespace PersonalBlog.Utils;

public static class ArticleFilterer
{
    public static List<Article> FilteredBy(this IEnumerable<Article> articles, string? searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return articles.ToList();

        return articles.Where(a =>
        {
            return a.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                   || a.Tags.Any(tag => searchString.Contains(tag, StringComparison.CurrentCultureIgnoreCase));
        }).ToList();
    }
}