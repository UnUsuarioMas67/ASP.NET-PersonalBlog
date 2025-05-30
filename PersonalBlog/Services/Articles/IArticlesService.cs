using PersonalBlog.Models;

namespace PersonalBlog.Services.Articles;

public interface IArticlesService
{
    Task<List<Article>> GetAllAsync();
    Task<Article?> GetByIdAsync(int id);
    Task CreateAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(int id);
}