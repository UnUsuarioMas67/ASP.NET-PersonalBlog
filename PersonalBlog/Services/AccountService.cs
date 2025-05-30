using PersonalBlog.Models;

namespace PersonalBlog.Services;

public class AccountService : IAccountService
{
    private readonly List<Account> _accounts = new List<Account>
    {
        new  Account() {Username = "unusuariomas67",  Password = "123456"},
        new  Account() {Username = "julioperez123",  Password = "123456"},
    };
    
    public Account? Login(string username, string password)
        => _accounts.FirstOrDefault(x => x.Username == username && x.Password == password);
}