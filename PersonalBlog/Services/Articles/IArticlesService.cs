using PersonalBlog.Models;

namespace PersonalBlog.Services.Articles;

public interface IArticlesService
{
    Task<List<Article>> GetAllAsync();
    Task<Article?> GetByIdAsync(int id);
    Task CreateAsync(Article article);
    Task<bool> UpdateAsync(Article article);
    Task<bool> DeleteAsync(int id);
}