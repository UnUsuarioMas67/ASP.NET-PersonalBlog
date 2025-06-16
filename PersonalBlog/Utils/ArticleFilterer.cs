using PersonalBlog.Models;

namespace PersonalBlog.Utils;

public static class ArticleFilterer
{
    public static List<Article> FilteredBy(this IEnumerable<Article> articles, string? searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return articles.ToList();

        var filteredArticles = articles.Where(a =>
        {
            var matchesTitle = a.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase);
            var matchesTags = 
                a.Tags?.Any(tag => searchString.Contains(tag, StringComparison.CurrentCultureIgnoreCase)) ?? false;
            return matchesTitle || matchesTags;
        }).ToList();
        
        return filteredArticles;
    }
}