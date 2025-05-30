using PersonalBlog.Models;

namespace PersonalBlog.Services.Accounts;

public interface IAccountsService
{
    Account? Login(string username, string password);
}