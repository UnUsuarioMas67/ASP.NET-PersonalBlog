using PersonalBlog.Models;
using YamlDotNet.Core;

namespace PersonalBlog.Services.Articles;

public class MarkdownArticlesService : IArticlesService
{
    private readonly string _articlesFolder = "articles";
    
    public async Task<List<Article>> GetAllAsync()
    {
        if (!Directory.Exists(_articlesFolder))
            return new List<Article>();
        
        var files = Directory.EnumerateFiles(_articlesFolder, "*.md").ToList();
        var tasks = files.Select(file => File.ReadAllTextAsync(file)).ToList();
        var markdownList = await Task.WhenAll(tasks);
        
        var articles = new List<Article>();
        for (int i = 0; i < markdownList.Length; i++)
        {
            var article = Article.FromMarkdown(markdownList[i]);
            article.Id = Path.GetFileNameWithoutExtension(files[i]);
            articles.Add(article);
        }
        
        return articles;
    }

    public async Task<Article?> GetByIdAsync(string id)
    {
        try
        {
            var markdown = await File.ReadAllTextAsync($"{_articlesFolder}/{id}.md");
            return Article.FromMarkdown(markdown);
        }
        catch (Exception e)
        {
            if (e is FileNotFoundException or DirectoryNotFoundException or YamlException)
                return null;
            
            throw;
        }
    }

    public async Task CreateAsync(Article article)
    {
        if (IdExists(article.Id))
            throw new InvalidOperationException($"Id '{article.Id}' already exists");
        
        var markdown = article.ToMarkdown();
        Directory.CreateDirectory(_articlesFolder);
        await File.WriteAllTextAsync($"{_articlesFolder}/{article.Id}.md", markdown);
    }

    public async Task<bool> UpdateAsync(Article article)
    {
        var file = Path.Combine(_articlesFolder, article.Id + ".md");
        if (!File.Exists(file))
            return false;
        
        article.LastModified = DateTime.Now;
        var markdown = article.ToMarkdown();
        await File.WriteAllTextAsync(file, markdown);
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var file = Path.Combine(_articlesFolder, id + ".md");
        if (!File.Exists(file))
            return false;
        
        File.Delete(file);
        return await Task.FromResult(true);
    }

    private bool IdExists(string id)
    {
        return Directory.EnumerateFiles(_articlesFolder, "*.md")
            .Select(Path.GetFileNameWithoutExtension)
            .Contains(id);
    }
}