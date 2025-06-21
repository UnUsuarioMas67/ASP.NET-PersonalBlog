using PersonalBlog.Models;

namespace PersonalBlog.Utils;

public static class ArticleFilterer
{
    public static List<Article> FilteredBy(this IEnumerable<Article> articles, string? searchString, string? category)
    {
        if (string.IsNullOrWhiteSpace(searchString) && string.IsNullOrWhiteSpace(category))
            return articles.ToList();

        var filteredArticles = articles.Where(a =>
        {
            var matchesTitle = a.Title.Contains(searchString ?? "", StringComparison.CurrentCultureIgnoreCase);
            var matchesTags =
                a.Tags?.Any(tag => searchString?.Contains(tag, StringComparison.CurrentCultureIgnoreCase) ?? true)
                ?? false;
            var matchesCategory = category == null || (a.Categories?.Contains(category) ?? false);

            return (matchesTitle || matchesTags) && matchesCategory;
        }).ToList();

        return filteredArticles;
    }
}