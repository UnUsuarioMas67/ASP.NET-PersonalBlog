using PersonalBlog.Models;

namespace PersonalBlog.Services;

public interface IAccountService
{
    Account? Login(string username, string password);
}