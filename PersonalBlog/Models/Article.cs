using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;

namespace PersonalBlog.Models;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Article
{
    public static readonly string[] AvailableCategories =
    [
        "Personal", "Tech", "Health", "Food", "Entertainment", "Hobbies", "Gaming", "Home", "Wellness",
        "Self-Improvement", "Education", "Productivity", "Lifestyle", "Travel", "Other"
    ];

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
    public List<string>? Categories { get; set; }
}