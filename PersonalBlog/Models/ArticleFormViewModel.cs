using System.ComponentModel;
using YamlDotNet.Serialization.Schemas;

namespace PersonalBlog.Models;

public class ArticleFormViewModel
{
    public Article Article { get; set; } = new Article();

    [DisplayName("Tags")]
    public string TagsString
    {
        get => Article.Tags != null ? string.Join(",", Article.Tags) : string.Empty;
        set => Article.Tags = value?.Split(',')
            .Where(t => !string.IsNullOrEmpty(t))
            .Select(t => t.Trim())
            .ToList();
    }

    public string? Message { get; set; }
}