using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PersonalBlog.Models;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Article
{
    [Required, StringLength(150), YamlIgnore]
    public string Id { get; set; } = string.Empty;

    [Required, StringLength(150)] 
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(int.MaxValue), YamlIgnore]
    public string Content { get; set; } = string.Empty;
    
    [StringLength(200)] 
    public string? Summary { get; set; }

    [DataType(DataType.Date), DisplayName("Publish Date")]
    public DateTime PublishDate { get; set; }

    [DataType(DataType.Date), DisplayName("Last Modified")]
    public DateTime? LastModified { get; set; }

    [YamlIgnore, DisplayName("Tags")] 
    public string TagsString { get; set; } = string.Empty;

    public List<string> Tags
    {
        get => !string.IsNullOrWhiteSpace(TagsString)
            ? TagsString.Split(',')
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList()
            : new List<string>();
        set => TagsString = string.Join(",", value);
    }
}