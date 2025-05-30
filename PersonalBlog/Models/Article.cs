using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [DisplayName("Publish Date")]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; }
}