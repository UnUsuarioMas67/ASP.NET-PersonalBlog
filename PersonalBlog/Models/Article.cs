using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalBlog.Models;

public class Article
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(int.MaxValue)]
    public string Content { get; set; } = string.Empty;
    
    [DisplayName("Publish Date")]
    [DataType(DataType.Date)]
    [Column(TypeName = "date")]
    public DateTime PublishDate { get; set; }
}