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

    public List<string>? Tags { get; set; }
    
    [YamlIgnore, DisplayName("Tags")]
    public string TagsString
    {
        get => Tags != null ? string.Join(",", Tags) : string.Empty;
        set => Tags = value?.Split(',')
            .Where(t => !string.IsNullOrEmpty(t))
            .Select(t => t.Trim())
            .ToList();
    } 
}