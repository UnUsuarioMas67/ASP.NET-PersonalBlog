using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Models;

namespace PersonalBlog.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context =
               new PersonalBlogContext(serviceProvider.GetRequiredService<DbContextOptions<PersonalBlogContext>>()))
        {
            if (context.Articles.Any())
                return;

            var articles = LoadArticles();
            if (articles == null)
                return;

            context.Articles.AddRange(articles);
            context.SaveChanges();
        }
    }

    private static List<Article>? LoadArticles()
    {
        try
        {
            var json = File.ReadAllText("mock_articles.json");
            var articles = JsonSerializer.Deserialize<List<Article>>(json,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return articles;
        }
        catch (IOException e)
        {
            return null;
        }
    }
}