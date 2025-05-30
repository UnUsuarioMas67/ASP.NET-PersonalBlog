using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models;

public class Account
{
    /*[Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = String.Empty;*/
    
    [Required]
    public string Username { get; set; } = String.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
}