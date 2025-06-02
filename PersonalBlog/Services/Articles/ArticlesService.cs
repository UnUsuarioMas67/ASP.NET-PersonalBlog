using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Services.Articles;

public class ArticlesService : IArticlesService
{
    private readonly PersonalBlogContext _context;

    public ArticlesService(PersonalBlogContext context)
    {
        _context = context;
    }

    public async Task<List<Article>> GetAllAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task<Article?> GetByIdAsync(int id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task CreateAsync(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Article article)
    {
        if (!_context.Articles.Any(e => e.Id == article.Id)) return false;

        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var article = await GetByIdAsync(id);
        
        if (article == null) return false;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
        return true;
    }
}