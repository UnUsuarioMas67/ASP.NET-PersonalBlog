using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models;

public class User
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    [StringLength(int.MaxValue)]
    public string Password { get; set; } = string.Empty;
}