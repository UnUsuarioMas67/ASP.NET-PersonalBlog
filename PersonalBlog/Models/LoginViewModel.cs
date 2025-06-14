using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models;

public class LoginViewModel
{
    public User User { get; set; } = new User();
    public string? Message { get; set; }
}